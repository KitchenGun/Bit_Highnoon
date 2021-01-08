/************************************************************************************

Copyright (c) Facebook Technologies, LLC and its affiliates. All rights reserved.  

See SampleFramework license.txt for license terms.  Unless required by applicable law 
or agreed to in writing, the sample code is provided �AS IS� WITHOUT WARRANTIES OR 
CONDITIONS OF ANY KIND, either express or implied.  See the license for specific 
language governing permissions and limitations under the license.

************************************************************************************/

using UnityEngine;

namespace OVRTouchSample
{
    // Animating controller that updates with the tracked controller.
    public class TouchController : MonoBehaviour
    {
        private GameManager GM;
        #region 기존 오큘러스 컨트롤러 스크립트 변수
        [SerializeField]
        private OVRInput.Controller m_controller = OVRInput.Controller.None;
        [SerializeField]
        private Animator m_animator = null;

        private bool m_restoreOnInputAcquired = false;
        #endregion

        #region Grab&Drop
        private string side; //컨트롤러 방향
        private bool isHandOnColider;//손이 이벤트 오브젝트와 충돌하는가?

        public GameObject GunSample; //무장하고 있는 총 샘플
        [SerializeField]
        private GameObject Belt; //밸트
        #endregion

        #region Fire 가능 확인 변수
        private bool FireState;
        private int Bullet;
        #endregion

        #region AudioSource
        private AudioSource HandAudio;
        private AudioClip Pistol_Garb;
        private AudioClip Pistol_DropOnHolster;
        #endregion

        private void Start()
        {
            GM = GameObject.Find("GameManager").GetComponent<GameManager>();
            HandAudio = this.gameObject.GetComponent<AudioSource>();//오디오 소스 선택
            side=this.gameObject.tag;//현재 컨트롤러 오른쪽 왼쪽 확인용
            isHandOnColider = false;
            this.gameObject.transform.Find("gun_hand").gameObject.SetActive(false);//현재 컨트롤러 총든손 총 내리도록 만들기
        }
       
        private void Update()
        {
            #region 기존 컨트롤러 애니메이션 (사용안함)
            //m_animator.SetFloat("Button 1", OVRInput.Get(OVRInput.Button.One, m_controller) ? 1.0f : 0.0f);
            //m_animator.SetFloat("Button 2", OVRInput.Get(OVRInput.Button.Two, m_controller) ? 1.0f : 0.0f);
            //m_animator.SetFloat("Joy X", OVRInput.Get(OVRInput.Axis2D.PrimaryThumbstick, m_controller).x);
            //m_animator.SetFloat("Joy Y", OVRInput.Get(OVRInput.Axis2D.PrimaryThumbstick, m_controller).y);
            //m_animator.SetFloat("Grip", OVRInput.Get(OVRInput.Axis1D.PrimaryHandTrigger, m_controller));
            //m_animator.SetFloat("Trigger", OVRInput.Get(OVRInput.Axis1D.PrimaryIndexTrigger, m_controller));
            #endregion

            #region 컨트롤러 연결 확인
            OVRManager.InputFocusAcquired += OnInputFocusAcquired;
            OVRManager.InputFocusLost += OnInputFocusLost;
            #endregion

            #region 총 들고있는지 확인
            DropCheck();
            #endregion

        }

        #region 컨트롤러 연결 확인
        private void OnInputFocusLost()
        {
            if (gameObject.activeInHierarchy)
            {
                gameObject.SetActive(false);
                m_restoreOnInputAcquired = true;
            }
        }

        private void OnInputFocusAcquired()
        {
            if (m_restoreOnInputAcquired)
            {
                gameObject.SetActive(true);
                m_restoreOnInputAcquired = false;
            }
        }
        #endregion

        #region 충돌처리
        private void OnCollisionStay(Collision collision)//벨트에서 총을 뽑을때
        {
            if (collision.transform.gameObject.name == "BeltGun")
            {
                if (Belt.GetComponent<Belt>().isSet(collision.gameObject.tag))
                {
                    //총을 들고있지 않을 경우
                    if (this.gameObject.transform.Find("OculusTouchForQuest2").GetComponentInChildren<SkinnedMeshRenderer>().enabled == true)
                    {
                        if (side == "Left")
                        {
                            if (OVRInput.Get(OVRInput.Axis1D.PrimaryHandTrigger, OVRInput.Controller.Touch) > 0.1)
                            {//그랩 할 경우
                             //벨트에서 총 제거
                                if (Belt.GetComponent<Belt>().isSet(collision.gameObject.tag))
                                {
                                    Belt.GetComponent<Belt>().GrabGun(collision.transform.gameObject.tag);
                                    getGunInfo(collision.transform.gameObject);
                                    //컨트롤러->총으로 모델링 교체
                                    HandtoGun();
                                    //사운드 효과
                                    HandAudio.clip =  GM.GetComponent<GameManager>().LoadAudioClip("gripup");
                                    HandAudio.Play();
                                }
                            }
                        }
                        else if (side == "Right")
                        {
                            if (OVRInput.Get(OVRInput.Axis1D.SecondaryHandTrigger, OVRInput.Controller.Touch) > 0.1)
                            {//그랩 할 경우
                             //벨트에서 총 제거
                                if (Belt.GetComponent<Belt>().isSet(collision.gameObject.tag))
                                {
                                    Belt.GetComponent<Belt>().GrabGun(collision.transform.gameObject.tag);
                                    getGunInfo(collision.transform.gameObject);
                                    //컨트롤러->총으로 모델링 교체
                                    HandtoGun();
                                    //사운드 효과
                                    HandAudio.clip = GM.GetComponent<GameManager>().LoadAudioClip("gripup");
                                    HandAudio.Play();
                                }
                            }
                        }
                    }

                }
               //else if (!GameObject.Find("Belt").GetComponent<Belt>().isSet(collision.gameObject.tag))
               //{
               //    //총을 들고있을 경우
               //    if (this.gameObject.transform.Find("OculusTouchForQuest2").GetComponentInChildren<SkinnedMeshRenderer>().enabled == false)
               //    {
               //         if (side == "Left")
               //         {
               //             if (OVRInput.Get(OVRInput.Axis1D.PrimaryHandTrigger, OVRInput.Controller.Touch) <= 0.1)
               //             {//드랍 할 경우
               //                 Belt.GetComponent<Belt>().DropGun(collision.gameObject.tag);
               //                 setGunInfo(collision.transform.gameObject);
               //                 //컨트롤러로 교체
               //                 GuntoHand();
               //             }
               //         }
               //         else if (side == "Right")
               //         {
               //             if (OVRInput.Get(OVRInput.Axis1D.SecondaryHandTrigger, OVRInput.Controller.Touch) <= 0.1)
               //             {//드랍 할 경우
               //                 Belt.GetComponent<Belt>().DropGun(collision.gameObject.tag);
               //                 setGunInfo(collision.transform.gameObject);
               //                 //총 컨트롤러로 교체
               //                 GuntoHand();
               //             }
               //         }
               //    }
               //}
            }
        }

        private void OnTriggerStay(Collider other)
        {
            switch (other.gameObject.name)
            {
                case "BeltGunPos":
                    if (!Belt.GetComponent<Belt>().isSet(other.gameObject.tag))
                    {
                        if (this.gameObject.transform.Find("OculusTouchForQuest2").GetComponentInChildren<SkinnedMeshRenderer>().enabled == false)
                        {//총을 들고있는 상태
                            if (side == "Left")
                            {
                                if (OVRInput.Get(OVRInput.Axis1D.PrimaryHandTrigger, OVRInput.Controller.Touch) <= 0.1)
                                {
                                    //밸트 위에서 드랍 할 경우 밸트에 총 생성
                                    Belt.GetComponent<Belt>().DropGun(other.gameObject.tag);
                                    setGunInfo(other.gameObject.transform.Find("BeltGun").gameObject);
                                    //컨트롤러로 교체
                                    GuntoHand();
                                    //사운드 효과
                                    HandAudio.clip = GM.GetComponent<GameManager>().LoadAudioClip("gripdown");
                                    HandAudio.Play();
                                }
                            }
                            else if (side == "Right")
                            {
                                if (OVRInput.Get(OVRInput.Axis1D.SecondaryHandTrigger, OVRInput.Controller.Touch) <= 0.1)
                                {
                                    //드랍 할 경우 밸트에 총 생성
                                    Belt.GetComponent<Belt>().DropGun(other.gameObject.tag);
                                    setGunInfo(other.gameObject.transform.Find("BeltGun").gameObject);
                                    //컨트롤러로 교체
                                    GuntoHand();
                                    //사운드 효과
                                    HandAudio.clip = GM.GetComponent<GameManager>().LoadAudioClip("gripdown");
                                    HandAudio.Play();
                                }
                            }
                        }
                    }
                    break;
                case "Gun":
                    if (!isHandOnColider)
                    {
                        if (other.gameObject.tag == "DropObj")
                        {
                            if (this.gameObject.transform.Find("OculusTouchForQuest2").GetComponentInChildren<SkinnedMeshRenderer>().enabled == true)
                            {//컨트롤러 상태인경우
                                if (side == "Left")
                                {
                                    if (OVRInput.Get(OVRInput.Axis1D.PrimaryHandTrigger, OVRInput.Controller.Touch) > 0.9)
                                    {
                                        //총에 정보에 접근 
                                        getGunInfo(other.gameObject);
                                        //컨트롤러->총으로 모델링 교체
                                        HandtoGun();
                                        Destroy(other.gameObject);
                                    }
                                }
                                else if (side == "Right")
                                {
                                    if (OVRInput.Get(OVRInput.Axis1D.SecondaryHandTrigger, OVRInput.Controller.Touch) > 0.9)
                                    {
                                        //총에 정보에 접근 
                                        getGunInfo(other.gameObject);
                                        //컨트롤러->총으로 모델링 교체
                                        HandtoGun();
                                        Destroy(other.gameObject);
                                    }
                                }
                            }
                        }
                    }
                    break;
                case "Gun(Clone)":
                    if (!isHandOnColider)
                    {
                        if (other.gameObject.tag == "DropObj")
                        {
                            if (this.gameObject.transform.Find("OculusTouchForQuest2").GetComponentInChildren<SkinnedMeshRenderer>().enabled == true)
                            {//컨트롤러 상태인경우
                                if (side == "Left")
                                {
                                    if (OVRInput.Get(OVRInput.Axis1D.PrimaryHandTrigger, OVRInput.Controller.Touch) > 0.9)
                                    {
                                        getGunInfo(other.gameObject);
                                        HandtoGun();
                                        Destroy(other.gameObject);
                                    }
                                }
                                else if (side == "Right")
                                {
                                    if (OVRInput.Get(OVRInput.Axis1D.SecondaryHandTrigger, OVRInput.Controller.Touch) > 0.9)
                                    {
                                        getGunInfo(other.gameObject);
                                        HandtoGun();
                                        Destroy(other.gameObject);
                                    }
                                }
                            }
                        }
                    }
                    break;
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if(other.gameObject.name == "BeltGunPos")
            {
                //밸트에 총 넣는 공간에서 손이 떠날 경우
                isHandOnColider = true;
            }
        }
        private void OnTriggerExit(Collider other)
        {
            if (other.gameObject.name == "BeltGunPos")
            {//밸트에 총 넣는 공간에서 손이 떠날 경우
                isHandOnColider = false;
            }
        }
        #endregion

        #region 교체 함수
        private void HandtoGun()
        {
            //컨트롤러->총으로 모델링 교체
            this.gameObject.transform.Find("OculusTouchForQuest2").GetComponentInChildren<SkinnedMeshRenderer>().enabled = false;
            this.gameObject.transform.Find("gun_hand").gameObject.SetActive(true);
            //사운드 효과
            HandAudio.clip = GM.GetComponent<GameManager>().LoadAudioClip("gripup");
            HandAudio.Play();
        }
        private void GuntoHand()
        {
            //컨트롤러로 교체
            this.gameObject.transform.Find("OculusTouchForQuest2").GetComponentInChildren<SkinnedMeshRenderer>().enabled = true;
            this.gameObject.transform.Find("gun_hand").gameObject.SetActive(false);
        }
        #endregion

        #region 총 들고있는지 확인
        private void DropCheck()
        {
            if(!isHandOnColider)
            {// 손이 이벤트 오브젝트와 충돌 안했을 경우
                if (this.gameObject.transform.Find("OculusTouchForQuest2").GetComponentInChildren<SkinnedMeshRenderer>().enabled == false)
                {//총을 들고있을 경우
                    if (side == "Left")
                    {
                        if (OVRInput.Get(OVRInput.Axis1D.PrimaryHandTrigger, OVRInput.Controller.Touch) <= 0.1)
                        {
                            //드랍 할 경우 손 위치에 총 모양 생성
                            GameObject Gun = Instantiate<GameObject>(GunSample, this.transform.position, Quaternion.identity) as GameObject;
                            Gun.tag = "DropObj";
                            setGunInfo(Gun);//총기 설정 저장
                            //컨트롤러로 교체
                            GuntoHand();
                        }
                    }
                    else if (side == "Right")
                    {
                        if (OVRInput.Get(OVRInput.Axis1D.SecondaryHandTrigger, OVRInput.Controller.Touch) <= 0.1)
                        {
                            //드랍 할 경우 손 위치에 총 모양 생성
                            GameObject Gun = Instantiate<GameObject>(GunSample, this.transform.position, Quaternion.identity) as GameObject;
                            Gun.tag = "DropObj";
                            setGunInfo(Gun);//총기 설정 저장
                            //컨트롤러로 교체
                            GuntoHand();
                        }
                    }
                }
            }
        }
        #endregion

        #region 강제 Drop
        private void Drop()
        {
            this.gameObject.GetComponent<SphereCollider>().enabled = false;
            if (this.gameObject.transform.Find("OculusTouchForQuest2").GetComponentInChildren<SkinnedMeshRenderer>().enabled == false)
            {
                //총을 들고있을 경우
                this.gameObject.GetComponent<SphereCollider>().enabled = false;
                //드랍 할 경우 손 위치에 총 모양 생성
                GameObject Gun = Instantiate<GameObject>(GunSample, this.transform.position, Quaternion.identity) as GameObject;
                Gun.tag = "DropObj";
                setGunInfo(Gun);//총기 설정 저장
                //컨트롤러로 교체
                GuntoHand();
            }
           
        }
        #endregion

        #region 총의 정보를 넣고 빼는 함수
        private void getGunInfo(GameObject gun)
        {
            //총에 정보에 접근 
            this.FireState = gun.GetComponent<Revolver>().FireState;//발사 가능 상태 체크
            this.Bullet = gun.GetComponent<Revolver>().cur_bullet;//사격가능한 총알 수 체크
            this.gameObject.transform.Find("gun_hand").gameObject.GetComponentInChildren<HandGunRayCast>().setGunInfo(ref this.FireState, ref this.Bullet);
        }
        private void setGunInfo(GameObject gun)
        {
            this.gameObject.transform.Find("gun_hand").gameObject.GetComponentInChildren<HandGunRayCast>().getGunInfo(ref FireState, ref Bullet);
            gun.GetComponent<Revolver>().setFireState(this.FireState);
            gun.GetComponent<Revolver>().setbullet(this.Bullet,false);
        }
        #endregion
    }
}
