using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;

public class LobbyExit : MonoBehaviourPunCallbacks
{
    private int ready_count;

    private PhotonView PV;

    // Start is called before the first frame update
    void Start()
    {
        PV = this.gameObject.GetPhotonView();

    }

    public void LeaveRoom() => PhotonNetwork.LeaveRoom();

    #region 나가기
    private void Exit()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            MasterExit();
        }
        else
        {
            PhotonNetwork.LeaveRoom();
        }
    }
    #endregion

    #region 방장이 나갈경우 호출
    [PunRPC]
    private void MasterExit()
    {
        PhotonNetwork.LeaveRoom();
    }
    #endregion

    #region 방나가고 호출되는 이벤트 함수
    public override void OnLeftRoom()
    {
        PhotonNetwork.LoadLevel(6);
    }
    #endregion
}
