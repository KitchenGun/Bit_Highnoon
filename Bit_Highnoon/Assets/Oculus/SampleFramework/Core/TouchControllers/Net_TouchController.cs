/************************************************************************************

Copyright (c) Facebook Technologies, LLC and its affiliates. All rights reserved.  

See SampleFramework license.txt for license terms.  Unless required by applicable law 
or agreed to in writing, the sample code is provided �AS IS� WITHOUT WARRANTIES OR 
CONDITIONS OF ANY KIND, either express or implied.  See the license for specific 
language governing permissions and limitations under the license.

************************************************************************************/

using UnityEngine;
using Photon.Pun;
using System.IO;

namespace OVRTouchSample
{
    // Animating controller that updates with the tracked controller.
    public class Net_TouchController : MonoBehaviourPun
    {
        private GameManager GM;
        private PhotonView PV;
        private int PVN;

        [SerializeField]
        private OVRInput.Controller m_controller = OVRInput.Controller.None;
        [SerializeField]
        private Animator m_animator = null;

        private bool m_restoreOnInputAcquired = false;

        private string side; //컨트롤러 방향
        private bool isHandOnColider;//손이 이벤트 오브젝트와 충돌하는가?

        [SerializeField]
        private GameObject Belt; //밸트

        private bool FireState;
        private int Bullet;

        private AudioSource HandAudio;
        private AudioClip Pistol_Garb;
        private AudioClip Pistol_DropOnHolster;

        private void Awake()
        {
            PV = this.gameObject.GetPhotonView();
        }

        private void Start()
        {
            GM = GameObject.Find("GameManager").GetComponent<GameManager>();
            HandAudio = this.gameObject.GetComponent<AudioSource>();//오디오 소스 선택
            side = this.gameObject.tag;//현재 컨트롤러 오른쪽 왼쪽 확인용
            isHandOnColider = false;
            this.gameObject.transform.Find("gun_hand").gameObject.SetActive(false);//현재 컨트롤러 총든손 총 내리도록 만들기
        }

        private void Update()
        {
            if (PV.IsMine)
            {

                //m_animator.SetFloat("Button 1", OVRInput.Get(OVRInput.Button.One, m_controller) ? 1.0f : 0.0f);
                //m_animator.SetFloat("Button 2", OVRInput.Get(OVRInput.Button.Two, m_controller) ? 1.0f : 0.0f);
                //m_animator.SetFloat("Joy X", OVRInput.Get(OVRInput.Axis2D.PrimaryThumbstick, m_controller).x);
                //m_animator.SetFloat("Joy Y", OVRInput.Get(OVRInput.Axis2D.PrimaryThumbstick, m_controller).y);
                //m_animator.SetFloat("Grip", OVRInput.Get(OVRInput.Axis1D.PrimaryHandTrigger, m_controller));
                //m_animator.SetFloat("Trigger", OVRInput.Get(OVRInput.Axis1D.PrimaryIndexTrigger, m_controller));

                OVRManager.InputFocusAcquired += OnInputFocusAcquired;
                OVRManager.InputFocusLost += OnInputFocusLost;

                DropCheck();

            }
        }

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

        private void OnCollisionStay(Collision collision)//벨트에서 총을 뽑을때
        {
            if (!PV.IsMine)
            {
                return;
            }
            if (collision.transform.gameObject.name == "BeltGun")
            {
                    if (Belt.GetComponent<Net_Belt>().isSet(collision.gameObject.tag))
                    {
                        //총을 들고있지 않을 경우
                        if (this.gameObject.transform.Find("OculusTouchForQuest2").GetComponentInChildren<SkinnedMeshRenderer>().enabled == true)
                        {
                            if (side == "Left")
                            {
                                if (OVRInput.Get(OVRInput.Axis1D.PrimaryHandTrigger, OVRInput.Controller.Touch) > 0.1)
                                {//그랩 할 경우
                                 //벨트에서 총 제거
                                    if (Belt.GetComponent<Net_Belt>().isSet(collision.gameObject.tag))
                                    {
                                        PV.RPC("GrapGun", RpcTarget.All, collision.gameObject.GetPhotonView().ViewID);                                    }
                                }
                            }
                            else if (side == "Right")
                            {
                                if (OVRInput.Get(OVRInput.Axis1D.SecondaryHandTrigger, OVRInput.Controller.Touch) > 0.1)
                                {//그랩 할 경우
                                 //벨트에서 총 제거
                                    if (Belt.GetComponent<Net_Belt>().isSet(collision.gameObject.tag))
                                    {
                                        PV.RPC("GrapGun", RpcTarget.All, collision.gameObject.GetPhotonView().ViewID);
                                    }
                                }
                            }
                        }

                    }
            }
        }

        [PunRPC]
        private void GrapGun(int ViewID)
        {
            PhotonView PVN = PhotonView.Find(ViewID);
            GameObject gun = PVN.gameObject;

            Belt.GetComponent<Net_Belt>().GrabGun(gun.tag);
            getGunInfo(gun);
            //컨트롤러->총으로 모델링 교체
            HandtoGun();
            //사운드 효과
            HandAudio.clip = GM.GetComponent<GameManager>().LoadAudioClip("gripup");
            HandAudio.Play();
        }

        private void OnTriggerStay(Collider other)
        {
            if (!PV.IsMine)
            {
                return;
            }
            switch (other.gameObject.name)
                {
                    case "BeltGunPos":
                        if (!Belt.GetComponent<Net_Belt>().isSet(other.gameObject.tag))
                        {
                            if (this.gameObject.transform.Find("OculusTouchForQuest2").GetComponentInChildren<SkinnedMeshRenderer>().enabled == false)
                            {//총을 들고있는 상태
                                if (side == "Left")
                                {
                                    if (OVRInput.Get(OVRInput.Axis1D.PrimaryHandTrigger, OVRInput.Controller.Touch) <= 0.1)
                                    {
                                        PV.RPC("DropGun_Belt", RpcTarget.All, other.gameObject.GetPhotonView().ViewID);
                                    }
                                }
                                else if (side == "Right")
                                {
                                    if (OVRInput.Get(OVRInput.Axis1D.SecondaryHandTrigger, OVRInput.Controller.Touch) <= 0.1)
                                    {
                                        PV.RPC("DropGun_Belt", RpcTarget.All, other.gameObject.GetPhotonView().ViewID);
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
                                            PV.RPC("GrapGun_Ground", RpcTarget.All, other.gameObject.GetPhotonView().ViewID);
                                            PhotonNetwork.Destroy(other.gameObject);
                                        }
                                    }
                                    else if (side == "Right")
                                    {
                                        if (OVRInput.Get(OVRInput.Axis1D.SecondaryHandTrigger, OVRInput.Controller.Touch) > 0.9)
                                        {
                                            PV.RPC("GrapGun_Ground", RpcTarget.All, other.gameObject.GetPhotonView().ViewID);
                                            PhotonNetwork.Destroy(other.gameObject);
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
                                            PV.RPC("GrapGun_Ground", RpcTarget.All, other.gameObject.GetPhotonView().ViewID);
                                            PhotonNetwork.Destroy(other.gameObject);
                                        }
                                    }
                                    else if (side == "Right")
                                    {
                                        if (OVRInput.Get(OVRInput.Axis1D.SecondaryHandTrigger, OVRInput.Controller.Touch) > 0.9)
                                        {
                                            PV.RPC("GrapGun_Ground", RpcTarget.All, other.gameObject.GetPhotonView().ViewID);
                                            PhotonNetwork.Destroy(other.gameObject);
                                        }
                                    }
                                }
                            }
                        }
                        break;
                }
        }

        [PunRPC]
        private void DropGun_Belt(int ViewID)
        {
            PhotonView PVN = PhotonView.Find(ViewID);
            GameObject gun = PVN.gameObject;

            //밸트 위에서 드랍 할 경우 밸트에 총 생성
            Belt.GetComponent<Net_Belt>().DropGun(gun.tag);
            setGunInfo(gun.transform.Find("BeltGun").gameObject);
            //Belt.GetComponent<Net_Belt>().DropGun(other.gameObject.tag);
            //setGunInfo(other.gameObject.transform.Find("BeltGun").gameObject);
            //컨트롤러로 교체
            GuntoHand();
            //사운드 효과
            HandAudio.clip = GM.GetComponent<GameManager>().LoadAudioClip("gripdown");
            HandAudio.Play();
        }

        [PunRPC]
        private void GrapGun_Ground(int ViewID)
        {
            PhotonView PVN = PhotonView.Find(ViewID);
            GameObject gun = PVN.transform.gameObject;

            //총에 정보에 접근 
            getGunInfo(gun);
            //컨트롤러->총으로 모델링 교체
            HandtoGun();
            
            //Destroy(gun);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (!PV.IsMine)
            {
                return;
            }
            if (other.gameObject.name == "BeltGunPos")
            {
                //밸트에 총 넣는 공간에서 손이 떠날 경우
                isHandOnColider = true;
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (!PV.IsMine)
            {
                return;
            }
            if (other.gameObject.name == "BeltGunPos")
            {//밸트에 총 넣는 공간에서 손이 떠날 경우
                isHandOnColider = false;
            }
        }

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
                            GameObject Gun = PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "Net_Gun"), this.transform.position, Quaternion.identity, 0);
                            PV.RPC("DropGun", RpcTarget.All, Gun.GetPhotonView().ViewID);
                        }
                    }
                    else if (side == "Right")
                    {
                        if (OVRInput.Get(OVRInput.Axis1D.SecondaryHandTrigger, OVRInput.Controller.Touch) <= 0.1)
                        {
                            GameObject Gun = PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "Net_Gun"), this.transform.position, Quaternion.identity, 0);
                            PV.RPC("DropGun", RpcTarget.All, Gun.GetPhotonView().ViewID);
                        }
                    }
                }
            }
        }

        [PunRPC]
        private void DropGun(int ViewID)
        {
            PhotonView PVN = PhotonView.Find(ViewID);
            GameObject Gun = PVN.gameObject;

            //드랍 할 경우 손 위치에 총 모양 생성            
            Gun.name = "Gun";
            Gun.tag = "DropObj";

            setGunInfo(Gun);//총기 설정 저장
                            //컨트롤러로 교체
            GuntoHand();
        }

        private void getGunInfo(GameObject gun)
        {
            //총에 정보에 접근 
            this.FireState = gun.GetComponent<Net_Revolver>().FireState;//발사 가능 상태 체크
            this.Bullet = gun.GetComponent<Net_Revolver>().cur_bullet;//사격가능한 총알 수 체크
            this.gameObject.transform.Find("gun_hand").gameObject.GetComponentInChildren<Net_HandGunRayCast>().setGunInfo(ref this.FireState, ref this.Bullet);
        }
        private void setGunInfo(GameObject gun)
        {
            this.gameObject.transform.Find("gun_hand").gameObject.GetComponentInChildren<Net_HandGunRayCast>().getGunInfo(ref FireState, ref Bullet);
            gun.GetComponent<Net_Revolver>().setFireState(this.FireState);
            gun.GetComponent<Net_Revolver>().setbullet(this.Bullet,false);
        }

    }
}
