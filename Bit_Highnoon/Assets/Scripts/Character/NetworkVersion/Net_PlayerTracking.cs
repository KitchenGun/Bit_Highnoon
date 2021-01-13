using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using Photon.Pun;

public class Net_PlayerTracking : MonoBehaviourPunCallbacks
{
    private PhotonView PV;
    private OVRCameraRig ovrCamRig;
    [SerializeField]
    private GameObject Head; //머리의 위치정보를 담고 있는 오브젝트
    private Camera HeadCam;
    private GameObject LHand;
    private GameObject RHand;
    private GameObject Body; //플레이어의 머리 밑에 같이 따라 움직이는 몸통 오브젝트
    [SerializeField]
    private GameObject Belt;//사용자가 가지고 있는 홀스터
    private void Awake()
    {
        ovrCamRig = this.gameObject.transform.parent.Find("OVRCameraRig").gameObject.GetComponent<OVRCameraRig>();
        PV = this.gameObject.transform.parent.GetComponent<PhotonView>();
        HeadCam = this.transform.parent.GetChild(1).GetChild(0).Find("CenterEyeAnchor").GetComponent<Camera>();
        Body = this.gameObject;
        LHand = Body.transform.parent.Find("LeftControllerAnchor").gameObject;
        RHand = Body.transform.parent.Find("RightControllerAnchor").gameObject;
    }

    private void Start()
    {
        if (!PV.IsMine)
        {
            #region PV
            HeadCam.transform.gameObject.tag = "Untagged";
            HeadCam.transform.gameObject.SetActive(false);
            #endregion
        }
    }
    void Update()
    {
        if (PV.IsMine)
        {
            #region 머리위치 추적
            Head.transform.position = HeadCam.transform.gameObject.transform.position;
            Head.transform.rotation = HeadCam.transform.gameObject.transform.rotation;
            #endregion
            #region 손위치 추적
            LHand.transform.position = this.transform.parent.GetChild(1).GetChild(0).Find("LeftHandAnchor").GetChild(0).gameObject.transform.position;
            LHand.transform.rotation = this.transform.parent.GetChild(1).GetChild(0).Find("LeftHandAnchor").GetChild(0).gameObject.transform.rotation;
            RHand.transform.position = this.transform.parent.GetChild(1).GetChild(0).Find("RightHandAnchor").GetChild(0).gameObject.transform.position;
            RHand.transform.rotation = this.transform.parent.GetChild(1).GetChild(0).Find("RightHandAnchor").GetChild(0).gameObject.transform.rotation;
            #endregion
            #region PV
            ovrCamRig.enabled = true;
            HeadCam.transform.gameObject.tag = "MainCamera";
            HeadCam.enabled = true;
            #endregion
            #region Body 회전과 위치 값 전달
            Body.transform.position = new Vector3
                (Head.transform.position.x,
                0,//이동 기능 제작시 플레이어의 최상위 오브젝트의 위치값을 받아오도록 제작
                Head.transform.position.z);
            Body.transform.eulerAngles = new Vector3(0, Head.transform.rotation.eulerAngles.y, 0); // 회전값 y축 만 전달
            #endregion
            #region 크기 변화
            Body.transform.localScale = new Vector3(
                Body.transform.localScale.x,
                (float)(Head.transform.position.y) / 2,
                Body.transform.localScale.z);
            #endregion
            #region  벨트 위치 회전값 변경
            Belt.transform.position = new Vector3(
                Body.transform.position.x,
                (float)(Head.transform.position.y) / 3 * 2,
                Body.transform.position.z);
            Belt.transform.eulerAngles = new Vector3(0, Head.transform.rotation.eulerAngles.y, 0); // 회전값 y축 만 전달
            #endregion
        }
      
    }

    #region 벨트 오브젝트 반환
    public GameObject GetBeltObj()//HoldFire에서 사용되는 함수
     {
         return Belt;
     }
     #endregion
}