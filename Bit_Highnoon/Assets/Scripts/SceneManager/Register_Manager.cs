using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using Photon.Realtime;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Register_Manager : MonoBehaviourPunCallbacks
{

    public Button[] RoomBtn;
    public Button PrevBtn, NextBtn;
    List<RoomInfo> myList = new List<RoomInfo>();

    int currentPage = 1, maxPage, multiple;

    [SerializeField] TMP_InputField Login_ID_InputField;
    [SerializeField] TMP_InputField Login_PW_InputField;
    [SerializeField] TMP_InputField Account_ID_InputField;
    [SerializeField] TMP_InputField Account_PW_InputField;
    [SerializeField] Transform roomListContent;
    [SerializeField] GameObject roomListItemPrefab;


    #region 서버접속 & 연결
    public void Connect() => PhotonNetwork.ConnectUsingSettings();

    public override void OnConnectedToMaster()
    {
        PhotonNetwork.LocalPlayer.NickName = Login_ID_InputField.text;
        print(Login_ID_InputField.text + "서버접속완료");
        JoinLobby();
    }
    #endregion

    #region 로비접속
    public void JoinLobby() => PhotonNetwork.JoinLobby();

    public override void OnJoinedLobby()
    {
        print("로비접속 완료");
        GameObject.Find("DB").gameObject.GetComponent<DBServer>().SendLoginUser(Login_ID_InputField.text, Login_PW_InputField.text);
    }
    #endregion
    #region 방생성
    public void CreateRoom() => PhotonNetwork.CreateRoom(PhotonNetwork.LocalPlayer.NickName, new RoomOptions { MaxPlayers = 2 }); // 방생성

    public void JoinRoom() => PhotonNetwork.JoinRoom(Login_ID_InputField.text); // 방입장 

    public void JoinOrCreateRoom() => PhotonNetwork.JoinOrCreateRoom(Login_ID_InputField.text, new RoomOptions { MaxPlayers = 2 }, null); // 방입장 or 방없을시 생성

    public void JoinRandomRoom() => PhotonNetwork.JoinRandomRoom(); // 방 랜덤 입장
    public override void OnCreatedRoom() {
        print("방만들기 완료"); // 방만들기 콜백
    }
    
    public void BtnClick(int num)
    {
        if (num == -2) --currentPage;
        else if (num == -1) ++currentPage;
        else PhotonNetwork.JoinRoom(myList[multiple + num].Name);
        myListRenewal();
    }
    void myListRenewal()
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
            RoomBtn[i].interactable = (multiple +i < myList.Count) ? true : false;
            RoomBtn[i].transform.GetChild(0).GetComponent<Text>().text = (multiple + i < myList.Count) ? myList[multiple + i].Name : "";
            RoomBtn[i].transform.GetChild(1).GetComponent<Text>().text = (multiple + i < myList.Count) ? myList[multiple + i].PlayerCount + "/" + myList[multiple + i].MaxPlayers : "";
        }

    }

    public override void OnJoinedRoom()
    {
        print("방참가 완료"); // 방입장 콜백
    }

    public override void OnCreateRoomFailed(short returnCode, string message) => print("방만들기실패"); // 방만들기 실패 콜백

    public override void OnJoinRoomFailed(short returnCode, string message) => print("방입장실패"); //방입장 실패 콜백

    public override void OnJoinRandomFailed(short returnCode, string message) => print("방랜덤입장실패"); // 방랜덤입장실패 콜백
    #endregion

    #region 방 리스트 갱신
    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        int roomCount = roomList.Count;
        for(int i = 0; i < roomCount; i++)
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

    //버튼 클릭 함수
    public void Hit(GameObject button)
    {
        Button btn = button.gameObject.GetComponent<Button>();

        if (button.gameObject == this.gameObject.transform.GetChild(3).gameObject)
        {
            btn.onClick.Invoke();
        }
    }
    public void CreateHit(GameObject button)
    {
        Button btn = button.gameObject.GetComponent<Button>();

        if (button.gameObject == this.gameObject.transform.GetChild(0).gameObject)
        {
            btn.onClick.Invoke();
        }
    }

    public void MakeAccount()
    {
        if(Account_ID_InputField.text == "" && Account_PW_InputField.text == "")
        {
            GameObject.Find("Picket").transform.GetChild(1).GetChild(0).GetChild(2).gameObject.GetComponent<Text>().text = "Please enter your ID and PW correctly.";
            return;
        }
        else
        //ID PW를 DB로 보내 회원가입 처리
            GameObject.Find("DB").gameObject.GetComponent<DBServer>().SendInsertUser(Account_ID_InputField.text, Account_PW_InputField.text);
    }

    //회원가입 성공, 실패 여부에 따른 행동
    public void AccountResult(bool register)
    {
        if (register == true)
        {
            //회원가입 성공시 텍스트
            GameObject.Find("Picket").transform.GetChild(1).GetChild(0).GetChild(2).gameObject.GetComponent<Text>().text = "Your account has been registered.";
        }
        else if(register == false)
        { 
            //사용 불가능일 경우
            GameObject.Find("Picket").transform.GetChild(1).GetChild(0).GetChild(2).gameObject.GetComponent<Text>().text = "This ID is already in use.";
        }
    }

    public void Login()
    {
        //if(GameManager.Instance.Login(Login_ID_InputField.text) == true)
        //{
            //가능한 경우 로비로 이동
        //    Connect();
        //   GameManager.Instance.PreSceneIndex();
        //    GameManager.Instance.ChangeToScene(9);
        //}
        //else
        //입력한 ID PW를 DB로 보내 로그인 처리
        Connect();
    }
    
    //로그인 성공, 실패 여부에 따른 행동
    public void LoginResult(bool register)
    {
        if (register == true)
        {
            //가능한 경우 로비로 이동
            //GameManager.Instance.PreSceneIndex();
            //GameManager.Instance.ChangeToScene(9);
            if (GameObject.Find("Picket").transform.GetChild(0).gameObject.activeSelf == true)
            {
                GameObject.Find("Picket").transform.GetChild(0).gameObject.SetActive(false);
            }
            GameObject.Find("Picket").transform.GetChild(2).gameObject.SetActive(true);
        }
        else if (register == false)
        {
            //로그인 실패한 경우
            GameObject.Find("Picket").transform.GetChild(0).GetChild(0).GetChild(2).gameObject.GetComponent<Text>().text = "Invalid information entered";
        }
    }

    #region 방 관리 보드
    public void HideOutRoom()
    {
        if (GameManager.Instance.Login(Login_ID_InputField.text) == true)
        {
            if (GameObject.Find("Picket").transform.GetChild(0).gameObject.activeSelf == true)
            {
                GameObject.Find("Picket").transform.GetChild(0).gameObject.SetActive(false);
            }
            GameObject.Find("Picket").transform.GetChild(2).gameObject.SetActive(true);
        }
        Connect();
    }
    #endregion

}
