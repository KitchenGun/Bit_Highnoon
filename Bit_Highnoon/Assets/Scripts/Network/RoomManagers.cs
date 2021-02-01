using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;
using TMPro;
using System.IO;

public class RoomManagers : MonoBehaviourPunCallbacks
{
    public static RoomManagers Instances;

    [SerializeField] TMP_InputField Login_ID_InputField;
    public Button[] RoomBtn;
    public Button PrevBtn, NextBtn;
    List<RoomInfo> myList = new List<RoomInfo>();
    int currentPage = 1, maxPage, multiple;



    #region 방생성
    public void CreateRoom() => PhotonNetwork.CreateRoom(PhotonNetwork.LocalPlayer.NickName, new RoomOptions { MaxPlayers = 2 }); // 방생성

    public void JoinRandomRoom() => PhotonNetwork.JoinRandomRoom(); // 방 랜덤 입장

    public void LeaveRoom()
    {
        PhotonNetwork.LeaveRoom();
        PhotonNetwork.LoadLevel(6);
    }
    public override void OnCreatedRoom()
    {
        print(PhotonNetwork.LocalPlayer.NickName + "님 방만들기 완료"); // 방만들기 콜백
    }

    public override void OnJoinedRoom()
    {
        PhotonNetwork.AutomaticallySyncScene = true;

        if (PhotonNetwork.IsMasterClient)
        {
            PhotonNetwork.LoadLevel(7);
        }
        myList.Clear();
    }
    public override void OnCreateRoomFailed(short returnCode, string message) => print("방만들기실패"); // 방만들기 실패 콜백

    public override void OnJoinRandomFailed(short returnCode, string message) => print("방랜덤입장실패"); // 방랜덤입장실패 콜백
    #endregion


    private void Awake()
    {
        Instances = this;
    }
    #region 방리스트 갱신
    public void MyListClick(int num)
    {
        if (num == -2) --currentPage;
        else if (num == -1) ++currentPage;
        else if (num == -3) LeaveRoom();
        else PhotonNetwork.JoinRoom(myList[multiple + num].Name);
        myListRenewal();
    }
    public void myListRenewal()
    {
        //최대페이지
        maxPage = (myList.Count % RoomBtn.Length == 0) ? myList.Count / RoomBtn.Length : myList.Count / RoomBtn.Length + 1;
        //prev,next
        PrevBtn.interactable = (currentPage <= 1) ? false : true;
        NextBtn.interactable = (currentPage >= maxPage) ? false : true;


        //페이지에 맞는 리스트 대입
        multiple = (currentPage - 1) * RoomBtn.Length;
        for (int i = 0; i < RoomBtn.Length; i++)
        {
            RoomBtn[i].interactable = (multiple + i < myList.Count) ? true : false;
            if (RoomBtn[i].GetComponent<Button>().interactable == true)
            {
                RoomBtn[i].GetComponent<BoxCollider>().enabled = true;
            }
            else
            {
                RoomBtn[i].GetComponent<BoxCollider>().enabled = false;
            }
            RoomBtn[i].transform.GetChild(0).GetComponent<Text>().text = (multiple + i < myList.Count) ? myList[multiple + i].Name : "";
            RoomBtn[i].transform.GetChild(1).GetComponent<Text>().text = (multiple + i < myList.Count) ? myList[multiple + i].PlayerCount + "/" + myList[multiple + i].MaxPlayers : "";
        }

    }
    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        print("방갱신");
        int roomCount = roomList.Count;
        for (int i = 0; i < roomCount; i++)
        {
            if (!roomList[i].RemovedFromList)
            {
                if (!myList.Contains(roomList[i])) myList.Add(roomList[i]);
                else myList[myList.IndexOf(roomList[i])] = roomList[i];
            }
            else if (myList.IndexOf(roomList[i]) != -1) myList.RemoveAt(myList.IndexOf(roomList[i]));
        }
        myListRenewal();
    }
    #endregion

    #region 로비정보보기
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
            print("연결됐는지? : " + PhotonNetwork.IsConnected);
        }
    }
    #endregion
}
