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
    public override void OnCreatedRoom()
    {
        print(PhotonNetwork.LocalPlayer.NickName + "님 방만들기 완료"); // 방만들기 콜백
    }

    public override void OnJoinedRoom()
    {
        print("방참가 완료"); // 방입장 콜백
        //GameManager.Instance.PreSceneIndex();
        GameManager.Instance.ChangeToScene(7);
        PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "LobbySpawn"), transform.position, transform.rotation);
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
}
