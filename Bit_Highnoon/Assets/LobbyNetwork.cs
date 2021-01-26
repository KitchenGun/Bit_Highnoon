using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using Photon.Realtime;
using TMPro;
using UnityEngine;

public class LobbyNetwork : MonoBehaviourPunCallbacks
{
    [SerializeField] TMP_InputField NickNmaeInput;

    public void Connect() => PhotonNetwork.ConnectUsingSettings();

    public override void OnConnectedToMaster()
    {
        Debug.Log("서버접속완료");
        PhotonNetwork.LocalPlayer.NickName = NickNmaeInput.text;
    }
}
