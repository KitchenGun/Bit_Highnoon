using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using Photon.Realtime;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Register_Manager : MonoBehaviourPunCallbacks
{

    public static Register_Manager Instance;
    [SerializeField] TMP_InputField Login_ID_InputField;
    [SerializeField] TMP_InputField Login_PW_InputField;
    [SerializeField] TMP_InputField Account_ID_InputField;
    [SerializeField] TMP_InputField Account_PW_InputField;
    [SerializeField] TMP_InputField NickNameInput; //아이디 입력 텍스트


    private void Awake()
    {
        Instance = this;
    }

    #region 서버접속 & 연결
    public void Connect() => PhotonNetwork.ConnectUsingSettings();

    public override void OnConnectedToMaster()
    {
        PhotonNetwork.LocalPlayer.NickName = NickNameInput.text;
        Debug.Log(NickNameInput + "서버접속완료");
        JoinLobby();
    }
    #endregion

    #region 로비접속
    public void JoinLobby() => PhotonNetwork.JoinLobby();

    public override void OnJoinedLobby()
    {
        Debug.Log("로비접속 완료");
    }
    #endregion

    //버튼 클릭 함수
    public void Hit(GameObject button)
    {
        switch (button.name)
        {
            case "Login":
                Login();
                break;
            case "Account":
                MakeAccount();
                break;
        }
    }

    public void MakeAccount()
    {
        if(Account_ID_InputField.text == "" || Account_PW_InputField.text == "")
        {
            this.gameObject.transform.GetChild(1).GetChild(0).GetChild(2).gameObject.GetComponent<TextMeshProUGUI>().text = "Please enter your ID and PW correctly.";
            return;
        }
        else
        //ID PW를 DB로 보내 회원가입 처리
            GameObject.Find("GameManager").GetComponent<DBServer>().SendInsertUser(Account_ID_InputField.text, Account_PW_InputField.text);
    }

    //회원가입 성공, 실패 여부에 따른 행동
    public void AccountResult(bool register)
    {
        if (register == true)
        {
            //회원가입 성공시 텍스트
            this.gameObject.transform.GetChild(1).GetChild(0).GetChild(2).gameObject.GetComponent<TextMeshProUGUI>().text = "Your account has been registered.";
            GameObject.Find("Picket").transform.GetChild(1).gameObject.SetActive(false);
        }
        else if(register == false)
        {
            //사용 불가능일 경우
            this.gameObject.transform.GetChild(1).GetChild(0).GetChild(2).gameObject.GetComponent<TextMeshProUGUI>().text = "This ID is already in use.";
        }
    }

    public void Login()
    {
        if (Login_ID_InputField.text == "" || Login_PW_InputField.text == "")
        {
            this.gameObject.transform.GetChild(0).GetChild(0).GetChild(2).gameObject.GetComponent<TextMeshProUGUI>().text = "Please enter your ID and PW correctly.";
            return;
        }
        else
        {
            GameObject.Find("GameManager").gameObject.GetComponent<DBServer>().SendLoginUser(Login_ID_InputField.text, Login_PW_InputField.text);
        }
    }
    
    //로그인 성공, 실패 여부에 따른 행동
    public void LoginResult(bool register)
    {
        if (register == true)
        {
            //가능한 경우 로비로 이동
            Connect();
            GameManager.Instance.PreSceneIndex();
            GameManager.Instance.ChangeToScene(9);
        }
        else if (register == false)
        {
            //로그인 실패한 경우
            this.gameObject.transform.GetChild(0).GetChild(0).GetChild(2).gameObject.GetComponent<TextMeshProUGUI>().text = "Invalid information entered";
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
