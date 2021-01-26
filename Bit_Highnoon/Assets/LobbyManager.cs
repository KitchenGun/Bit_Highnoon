using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using Photon.Realtime;
using TMPro;
using UnityEngine;

public class LobbyManager : MonoBehaviourPunCallbacks
{
    [SerializeField] TMP_InputField RoomNameInput; //방이름 입력 텍스트

    #region Room 생성
    public void CreateRoom() => PhotonNetwork.CreateRoom(RoomNameInput.text, new RoomOptions { MaxPlayers = 2 });

    public void JoinRoom() => PhotonNetwork.JoinRoom(RoomNameInput.text);

    public void JoinRandomRoom() => PhotonNetwork.JoinRandomRoom();

    public void LeaveRoom()
    {
        PhotonNetwork.LeaveRoom();
        MenuManager.Instance.OpenMenu("title");
    }

    public void StartGame()
    {
        PhotonNetwork.LoadLevel(8);
    }

    public void JoinorCreateRoom() => PhotonNetwork.JoinOrCreateRoom(RoomNameInput.text, new RoomOptions { MaxPlayers = 2 }, null);
    public override void OnCreatedRoom()
    {
        Debug.Log("방만들기완료");
    }
    public override void OnJoinedRoom()
    {
        Debug.Log("방참가완료");
    }

    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        Debug.Log("방만들기실패");
    }

    public override void OnJoinRoomFailed(short returnCode, string message)
    {
        Debug.Log("방참가실패");
    }
    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        Debug.Log("방랜덤참가실패");
    }
    #endregion

    [ContextMenu("정보")]
    void Info()
    {
        if (PhotonNetwork.InRoom)
        {
            print("현재 방 이름 : " + PhotonNetwork.CurrentRoom.Name);
            print("현재 방 인원수 : " + PhotonNetwork.CurrentRoom.PlayerCount);
            print("현재 방 최대인원수 : " + PhotonNetwork.CurrentRoom.MaxPlayers);

            string playerStr = "방에 있는 플레이어 목록 : ";
            for (int i = 0; i < PhotonNetwork.PlayerList.Length; i++) playerStr += PhotonNetwork.PlayerList[i].NickName + ", ";
            print(playerStr);
        }
        else
        {
            print("접속한 인원 수 : " + PhotonNetwork.CountOfPlayers);
            print("방 개수 : " + PhotonNetwork.CountOfRooms);
            print("모든 방에 있는 인원 수 : " + PhotonNetwork.CountOfPlayersInRooms);
            print("로비에 있는지? : " + PhotonNetwork.InLobby);
            print("연결이됐는지? : " + PhotonNetwork.IsConnected);
        }
    }
}
