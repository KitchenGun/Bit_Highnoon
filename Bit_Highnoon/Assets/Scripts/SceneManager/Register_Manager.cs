using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using Photon.Realtime;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Register_Manager : MonoBehaviourPunCallbacks
{
    [SerializeField] TMP_InputField Login_ID_InputField;
    [SerializeField] TMP_InputField Login_PW_InputField;
    [SerializeField] TMP_InputField Account_ID_InputField;
    [SerializeField] TMP_InputField Account_PW_InputField;
    [SerializeField] TMP_InputField NickNameInput; //아이디 입력 텍스트

    #region 서버접속 & 연결
    public void Connect() => PhotonNetwork.ConnectUsingSettings();

    public override void OnConnectedToMaster()
    {
        PhotonNetwork.LocalPlayer.NickName = NickNameInput.text;
        print(NickNameInput + "서버접속완료");
        JoinLobby();
    }
    #endregion

    #region 로비접속
    public void JoinLobby() => PhotonNetwork.JoinLobby();

    public override void OnJoinedLobby()
    {
       print("로비접속 완료");
        GameManager.Instance.ChangeToScene(7);
    }
    #endregion
    public void Hit(GameObject button)
    {
        Button btn = button.gameObject.GetComponent<Button>();

        if (button.gameObject == this.gameObject.transform.GetChild(3).gameObject)
        {
            btn.onClick.Invoke();
        }
    }

    public void MakeAccount()
    {
        //DB에서 id값 비교 후 사용가능한지 확인
        //가능한 경우 Id와 PW db에 저장

        //회원가입 성공시 텍스트
        GameObject.Find("Picket").transform.GetChild(1).GetChild(0).GetChild(2).gameObject.GetComponent<Text>().text = "Your account has been registered.";

        //사용 불가능일 경우
        GameObject.Find("Picket").transform.GetChild(1).GetChild(0).GetChild(2).gameObject.GetComponent<Text>().text = "This ID is already in use.";
    }

    public void Login()
    {
        //DB에서 ID & PW 확인 후 가능한지 확인
        bool login = GameManager.Instance.Login(Login_ID_InputField.text);

        if (login == true)
        {
            //가능한 경우 로비로 이동
            Connect();
        }
        else if (login == false)
        {
            //로그인 실패한 경우
            GameObject.Find("Picket").transform.GetChild(0).GetChild(0).GetChild(2).gameObject.GetComponent<Text>().text = "Invalid information entered";
        }
    }
    
}
