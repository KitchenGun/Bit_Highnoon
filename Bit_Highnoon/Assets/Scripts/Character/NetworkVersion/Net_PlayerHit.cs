using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class Net_PlayerHit : MonoBehaviourPunCallbacks
{
    private PhotonView PV;
    [SerializeField]
    private  Image Panel;
    [SerializeField]
    private List<GameObject> Controllers;
    //맞은 횟수
    private int hitCount;


    void Start()
    {
        PV = this.gameObject.GetPhotonView();//this.gameObject.transform.parent.gameObject.GetPhotonView();
        Panel.color = new Vector4(0, 0, 0, 0);
        hitCount = 0;
    }

    private void FixedUpdate()
    {
        if (PV.IsMine)
        {
            #region 색상확인
            if (Panel.color != (Color)new Vector4(0, 0, 0, 0))
            {
                Panel.color = Panel.color - (Color)new Vector4(0.01f, 0, 0, 0.01f);
            }
            #endregion
        }
    }

    #region 패널 색상 변경
    private void PanelSetRed()
    {
        Panel.color = new Vector4(1, 0, 0, 0.8f);
    }
    #endregion

    #region 피격
    private void Hit()
    {
        hitCount++;
        PanelSetRed();
        if(hitCount>=2)//맞은 횟수가 상수보다 클경우 사망처리
        {
            Die();
        }
    }
    #endregion

    #region 사망
    private void Die()
    {
        PanelSetRed();
        PV.RPC("SendDropGun", RpcTarget.All);
    }
    
    [PunRPC]
    private void SendDropGun()
    {
        foreach (GameObject controller in Controllers)
        {
            controller.GetComponent<OVRTouchSample.Net_TouchController>().SendMessage("Drop_Die");
        }
    }
    #endregion

}
