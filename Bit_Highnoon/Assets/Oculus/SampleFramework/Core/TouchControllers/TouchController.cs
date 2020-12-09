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
        #region 기존 오큘러스 컨트롤러 스크립트 변수
        [SerializeField]
        private OVRInput.Controller m_controller = OVRInput.Controller.None;
        [SerializeField]
        private Animator m_animator = null;

        private bool m_restoreOnInputAcquired = false;
        #endregion

        private string side; //컨트롤러 방향

        private void Start()
        {
            side=this.gameObject.tag;//현재 컨트롤러 오른쪽 왼쪽 확인용
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
        private void OnCollisionStay(Collision collision)
        {
            Debug.Log(collision.transform.gameObject.name);
            if(collision.transform.gameObject.name == "BeltGun")
            {
                //총을 들고있지 않을 경우
                if (this.gameObject.transform.Find("OculusTouchForQuest2").GetComponentInChildren<SkinnedMeshRenderer>().enabled == true)
                {  
                    if (OVRInput.Get(OVRInput.Axis1D.PrimaryHandTrigger, OVRInput.Controller.Touch) > 0 || OVRInput.Get(OVRInput.Axis1D.SecondaryHandTrigger, OVRInput.Controller.Touch) > 0)
                    { //그랩 할 경우
                        //벨트에서 총 제거
                        GameObject.Find("Belt").GetComponent<Belt>().GrabGun(collision.transform.gameObject.tag);
                        //컨트롤러 총으로 교체
                        this.gameObject.transform.Find("OculusTouchForQuest2").GetComponentInChildren<SkinnedMeshRenderer>().enabled = false;
                        this.gameObject.transform.Find("gun_hand").gameObject.SetActive(true);
                    }
                }
                //총을 들고있을 경우  
                else if (this.gameObject.transform.Find("OculusTouchForQuest2").GetComponentInChildren<SkinnedMeshRenderer>().enabled == false)
                {  
                    if (OVRInput.Get(OVRInput.Axis1D.PrimaryHandTrigger, OVRInput.Controller.Touch) <= 0 || OVRInput.Get(OVRInput.Axis1D.SecondaryHandTrigger, OVRInput.Controller.Touch) <= 0)
                    {//드랍 할 경우
                        //벨트에서 총 제거
                        GameObject.Find("Belt").GetComponent<Belt>().GrabGun(collision.transform.gameObject.tag);
                        //총 컨트롤러로 교체
                        this.gameObject.transform.Find("OculusTouchForQuest2").GetComponentInChildren<SkinnedMeshRenderer>().enabled = true;
                        this.gameObject.transform.Find("gun_hand").gameObject.SetActive(false);
                    }
                }
            }
        }
        #endregion

    }
}
