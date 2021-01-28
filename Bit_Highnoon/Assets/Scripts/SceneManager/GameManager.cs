using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    private SoundDB sounddb;
    private LogicalDB leveldb;
    private GameObject normal;
    private GameObject hard;
    private string id = "SEX";
    private bool login = false;    
    private int p_index;

    #region DB정보
    private bool register = false;

    private Queue<string> messageQueue = new Queue<string>();

    private string char_material = string.Empty;

    private string hat_material = string.Empty;

    public string Char_Material { get { return char_material; } }
    public string Hat_Material { get { return hat_material; } }

    //큐에 데이타 삽입
    public void PushData(string data)
    {
        messageQueue.Enqueue(data);
    }

    public string GetData()
    {
        //데이타가 1개라도 있을 경우 꺼내서 반환
        if (messageQueue.Count > 0)
            return messageQueue.Dequeue();
        else
            return string.Empty;    //없으면 빈값을 반환
    }

    private IEnumerator CheckQueue()
    {
        //1초 주기로 탐색
        WaitForSeconds waitSec = new WaitForSeconds(1);

        while (true)
        {
            //우편함에서 데이타 꺼내기
            string data = GetData();

            //우편함에 데이타가 있는 경우
            if (!data.Equals(string.Empty))
            {
                //데이타로 UI 갱신
                ResponseData(data);
                yield break;
            }

            yield return waitSec;
        }
    }

    public void ResponseData(string data)
    {
        string[] filter = data.Split('\a');

        if (filter[0].Equals("S_InsertUser") == true)
        {
            UserInsert(filter[1]);
        }
        else if (filter[0].Equals("S_UserLogin") == true)
        {
            UserLogin(filter[1]);
        }
        else if (filter[0].Equals("S_UserColorHat") == true)
        {
            if (filter[1] == "E")
                Debug.Log("DB서버 에러");
            else
            {
                string[] info = filter[1].Split('#');
                UserColor(info[0]);
                UserHat(info[1]);
            }
        }
    }

    #region DB수신정보 처리

    //유저 추가
    private void UserInsert(string msg)
    {
        try
        {
            if (msg == "true")
            {
                Debug.Log("유저추가 성공");
                register = true;
                //GameObject.Find("Picket").transform.GetChild(1).GetChild(0).gameObject.GetComponent<Register_Manager>().SendMessage("AccountResult", register);
                Register_Manager.Instance.AccountResult(register);
            }
            else if (msg == "same")
            {
                Debug.Log("유저추가 실패 : 동일한 id");
                register = false;
                Register_Manager.Instance.AccountResult(register);
            }
            else
            {
                Debug.Log("유저추가 실패 : db오류");
                register = false;
                Register_Manager.Instance.AccountResult(register);
            }
        }
        catch (Exception)
        {
            Debug.Log("유저추가 실패 : 서버오류");
        }
    }

    //유저 로그인
    private void UserLogin(string msg)
    {
        try
        {
            if (msg == "S")
            {
                Debug.Log("로그인 성공");
                register = true;
                GameObject.Find("Picket").transform.GetChild(0).GetChild(0).gameObject.GetComponent<Register_Manager>().SendMessage("LoginResult", register);
            }
            else if (msg == "N")
            {
                Debug.Log("로그인 실패 :유저 없음");
                register = false;
                GameObject.Find("Picket").transform.GetChild(0).GetChild(0).gameObject.GetComponent<Register_Manager>().SendMessage("LoginResult", register);
            }
            else
            {
                Debug.Log("로그인 실패");
                register = false;
                GameObject.Find("Picket").transform.GetChild(0).GetChild(0).gameObject.GetComponent<Register_Manager>().SendMessage("LoginResult", register);
            }
        }
        catch (Exception)
        {
            Debug.Log("로그인 실패 : 서버오류");
        }
    }

    //유저 모자, 색상정보
    private void UserHat(string msg)
    {
        try
        {
            char_material = msg;
            if (msg == "Camohat")
            {                
                Debug.Log("모자 : Camohat");
            }
            else if (msg == "Cowboyhat")
            {
                Debug.Log("모자 : Cowboyhat");
            }
            else if (msg == "Crocohat")
            {
                Debug.Log("모자 : Crocohat");
            }
            else if (msg == "Detectivehat")
            {
                Debug.Log("모자 : Detectivehat");
            }
            else if (msg == "Firefighter2hat")
            {
                Debug.Log("모자 : Firefighter2hat");
            }
            else if (msg == "Firefighterhat")
            {
                Debug.Log("모자 : Firefighterhat");
            }
            else if (msg == "Ghat")
            {
                Debug.Log("모자 : Ghat");
            }
            else if (msg == "Poor")
            {
                Debug.Log("모자 : Poor");
            }
            else if (msg == "Wovenhat")
            {
                Debug.Log("모자 : Wovenhat");
            }
            else
            {
                Debug.Log("모자변경 실패 : DB에러");
            }
        }
        catch (Exception)
        {
            Debug.Log("모자변경 실패");
        }
    }

    //유저 색상
    private void UserColor(string msg)
    {
        try
        {
            hat_material = msg;
            if (msg == "White")
            {
                Debug.Log("색상 : White");
            }
            else if (msg == "Black")
            {
                Debug.Log("색상 : Black");
            }
            else if (msg == "Brown")
            {
                Debug.Log("색상 : Brown");
            }
            else if (msg == "Green")
            {
                Debug.Log("색상 : Green");
            }
            else if (msg == "LightGreen")
            {
                Debug.Log("색상 : LightGreen");
            }
            else if (msg == "Mint")
            {
                Debug.Log("색상 : Mint");
            }
            else if (msg == "Orange")
            {
                Debug.Log("색상 : Orange");
            }
            else if (msg == "Pink")
            {
                Debug.Log("색상 : Pink");
            }
            else if (msg == "Purple")
            {
                Debug.Log("색상 : Purple");
            }
            else if (msg == "Red")
            {
                Debug.Log("색상 : Red");
            }  
            else
            {
                Debug.Log("색상변경 실패 : DB에러");
            }
            
        }
        catch (Exception)
        {
            Debug.Log("색상변경 실패");
        }
    }

    #endregion
    #endregion

    #region 네트워크 GameEnd 체크
    private bool is_netgame_end;    //네트워크 게임이 끝났는지 체크

    public bool Is_netgame_end { get { return is_netgame_end; } }
    #endregion

    #region Singleton 싱글톤
    private static GameManager instance;

    public static GameManager Instance 
    {
        get
        {
            if(!instance)
            {
                instance = FindObjectOfType(typeof(GameManager)) as GameManager;

                if (instance == null)
                    Debug.Log("no Singleton obj");
            }
            return instance;
        }
    }

    private void Awake()
    {

        sounddb = this.gameObject.AddComponent<SoundDB>();
        leveldb = this.gameObject.AddComponent<LogicalDB>();

        this.gameObject.AddComponent<DBServer>().enabled = false;
        this.gameObject.AddComponent<SpawnManager>().enabled = false;
        if (SceneManager.GetActiveScene().buildIndex == 6)
        {
            this.gameObject.GetComponent<DBServer>().enabled = true;
            this.gameObject.GetComponent<SpawnManager>().enabled = false;
        }
        else if(SceneManager.GetActiveScene().buildIndex == 7 || SceneManager.GetActiveScene().buildIndex == 8)
        {
            this.gameObject.GetComponent<DBServer>().enabled = true;
            this.gameObject.GetComponent<SpawnManager>().enabled = true;
        }
        else
        {
            this.gameObject.GetComponent<DBServer>().enabled = false;
            this.gameObject.GetComponent<SpawnManager>().enabled = false;
        }

        
        if(instance == null)
        {
            instance = this;
        }

        else if(instance != this)
        {
            Destroy(gameObject);
        }

        leveldb.StartXml();
        sounddb.SoundUpdate(GetSceneIndex());

        DontDestroyOnLoad(gameObject);

        StartCoroutine(CheckQueue());
    }
    #endregion

    #region 사운드 호출 함수
    public AudioClip LoadAudioClip(string filename)
    {
        if (sounddb.AudioList.ContainsKey(filename))
            return sounddb.AudioList[filename];

        int i = 1;
        string temp = filename + i;

        while (sounddb.AudioList.ContainsKey(temp))
        {
            i++;
            temp = filename + i;
        }

        int rand = UnityEngine.Random.Range(1, i);

        filename += rand;

        return sounddb.AudioList[filename];
    }
    #endregion

    #region 함수
    //씬 이동
    public void ChangeToScene(int idx)
    {
        sounddb.SoundUpdate(idx);        

        SceneManager.LoadScene(idx);
    }

    //게임 종료
    public void ExitGame()
    {
        Application.Quit();
    }

    public int GetSceneIndex()
    {
        int idx;
        idx = SceneManager.GetActiveScene().buildIndex;

        return idx;
    }
    #endregion

    #region 씬 이동
    public void MoveScene(GameObject bottle)
    {
        //메인 메뉴
        if (SceneManager.GetActiveScene().buildIndex == 1)
        {
            switch (bottle.name)
            {
                case "Single":
                    ChangeToScene(2); break;
                case "Multi":
                    ChangeToScene(6); break;
                case "Option":
                    ChangeToScene(9); break;
                case "Exit":
                    ExitGame(); break;
            }
        }
        //레벨 선택 메뉴
        else if (SceneManager.GetActiveScene().buildIndex == 2)
        {
            switch (bottle.name)
            {
                case "Easy":
                    ChangeToScene(3); break;
                case "Normal":
                    ChangeToScene(4); break;
                case "Hard":
                    ChangeToScene(5); break;
                case "Back":
                    ChangeToScene(1); break;
            }
        }
    }
    #endregion

    #region 게임 Start & End 음악

    public void GameStart()
    {
        is_netgame_end = false;

        AudioPlay("BattleStart");

        int scene = GetSceneIndex();
        if (scene == 3 || scene == 4 || scene == 5) //싱글 결투가 시작할 때
            PlayerStart();
        else if (scene == 8)                        //네트워크 결투가 시작할 때
            Net_PlayerStart();
    }

    private void PlayerStart()
    {
        GameObject player = GameObject.Find("PlayerCtrl");

        player.transform.Find("Body").GetComponent<HoldFire>().SendMessage("OpenFire");
    }

    private void Net_PlayerStart()
    {
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");

        foreach (GameObject player in players)
            player.transform.Find("Body").GetComponent<Net_HoldFire>().SendMessage("OpenFire");
    }

    public IEnumerator GameEnd(string winner)
    {
        if(is_netgame_end == false)
            AudioPlay("BattleEnd");

        #region 싱글 전적 관리
        if (winner.Equals("AI"))
        {
            //AI가 이겼을 때
            if (GetSceneIndex() == 3)
            {
                //이지 난이도
                leveldb.EasyLoseCount();
            }
            else if (GetSceneIndex() == 4)
            {
                //노말 난이도
                leveldb.NormalLoseCount();
            }
            else if(GetSceneIndex() == 5)
            {
                //하드 난이도
                leveldb.HardLoseCount();
            }
        }
        else if(winner.Equals("player"))
        {
            //player가 이겼을 때
            if (GetSceneIndex() == 3)
            {
                //이지 난이도
                leveldb.EasyWinCount();
                leveldb.NormalUser();
            }
            else if (GetSceneIndex() == 4)
            {
                //노말 난이도
                leveldb.NormalWinCount();
                leveldb.HardUser();
            }
            else if (GetSceneIndex() == 5)
            {
                //하드 난이도
                leveldb.HardWinCount();
            }
        }
        #endregion

        int scene = GetSceneIndex();
        if (scene == 3 || scene == 4 || scene == 5) //싱글 결투가 끝난후
        {
            yield return new WaitForSeconds(7.5f);

            ChangeToScene(1);
        }
        else if (scene == 8)                        //네트워크 결투가 끝난후
        {
            if (is_netgame_end == false)
            {
                GameObject[] players = GameObject.FindGameObjectsWithTag("Player");

                for (int i = 0; i < players.Length; i++)
                    players[i].transform.Find("Body").GetComponent<Net_PlayerHit>().SendMessage("GameEnd");
            }

            is_netgame_end = true;
        }

        yield return null;
    }

    private void AudioPlay(string filename)
    {
        AudioSource Audio = GetComponent<AudioSource>();

        Audio.clip = LoadAudioClip(filename);
        Audio.loop = false;

        Audio.Play();
    }
    #endregion

    #region Update함수
    /*
    void Update()
    {
        #region 메인 메뉴 씬 기능
        if (SceneManager.GetActiveScene().buildIndex == 1)
        {
            if (Input.GetKeyDown(KeyCode.F1))    //유리병 깨지는 이벤트 발생시로 변경
            {
                //싱글 메뉴 이동
                int idx = SceneManager.GetActiveScene().buildIndex + 1;
                NextToScene(idx);
            }

            if (Input.GetKeyDown(KeyCode.F2))    //유리병 깨지는 이벤트 발생시로 변경
            {
                //로비 이동
                int idx = SceneManager.GetActiveScene().buildIndex + 6;
                GameManager.Instance.NextToScene(idx);
            }

            if (Input.GetKeyDown(KeyCode.F3))    //유리병 깨지는 이벤트 발생시로 변경
            {
                //옵션 이동
                int idx = SceneManager.GetActiveScene().buildIndex + 9;
                GameManager.Instance.NextToScene(idx);
            }

            if (Input.GetKeyDown(KeyCode.F4))    //유리병 깨지는 이벤트 발생시로 변경
            {
                //게임 종료
                GameManager.Instance.ExitGame();
            }
        }
        #endregion

        #region 싱글 메뉴 씬 기능
        else if (SceneManager.GetActiveScene().buildIndex == 2)
        {
            if (Input.GetKeyDown(KeyCode.F1))    //유리병 깨지는 이벤트 발생시로 변경
            {
                //레벨 메뉴 이동
                int idx = SceneManager.GetActiveScene().buildIndex + 1;
                GameManager.Instance.NextToScene(idx);
            }

            if (Input.GetKeyDown(KeyCode.F2))    //유리병 깨지는 이벤트 발생시로 변경
            {
                //튜토리얼 이동
                int idx = SceneManager.GetActiveScene().buildIndex + 9;
                GameManager.Instance.NextToScene(idx);
            }

            if (Input.GetKeyDown(KeyCode.F3))    //유리병 깨지는 이벤트 발생시로 변경
            {
                //뒤로가기(메인 메뉴 이동)
                int idx = SceneManager.GetActiveScene().buildIndex;
                GameManager.Instance.BackToScene(idx);
            }
        }
        #endregion

        #region 레벨 메뉴 씬 기능
        else if (SceneManager.GetActiveScene().buildIndex == 3)
        {
            if (Input.GetKeyDown(KeyCode.F1))    //유리병 깨지는 이벤트 발생시로 변경
            {
                //쉬움 모드 실행
                int idx = SceneManager.GetActiveScene().buildIndex + 1;
                GameManager.Instance.NextToScene(idx);
            }

            if (Input.GetKeyDown(KeyCode.F2))    //유리병 깨지는 이벤트 발생시로 변경
            {
                //보통 모드 실행
                int idx = SceneManager.GetActiveScene().buildIndex + 2;
                GameManager.Instance.NextToScene(idx);
            }

            if (Input.GetKeyDown(KeyCode.F3))    //유리병 깨지는 이벤트 발생시로 변경
            {
                //어려움 모드 실행
                int idx = SceneManager.GetActiveScene().buildIndex + 3;
                GameManager.Instance.NextToScene(idx);
            }

            if (Input.GetKeyDown(KeyCode.F4))    //유리병 깨지는 이벤트 발생시로 변경
            {
                //뒤로가기(싱글 메뉴 이동)
                int idx = SceneManager.GetActiveScene().buildIndex;
                GameManager.Instance.BackToScene(idx);
            }
        }
        #endregion

        #region 로비 기능
        else if (SceneManager.GetActiveScene().buildIndex == 7)
        {
            if (Input.GetKeyDown(KeyCode.F1))    //유리병 깨지는 이벤트 발생시로 변경 or 버튼
            {
                //게임 시작
                int idx = SceneManager.GetActiveScene().buildIndex + 1;
                GameManager.Instance.NextToScene(idx);
            }

            if (Input.GetKeyDown(KeyCode.F2))    //유리병 깨지는 이벤트 발생시로 변경 or 버튼
            {
                //뒤로가기
                int idx = SceneManager.GetActiveScene().buildIndex;
                GameManager.Instance.BackToScene(idx);
            }
        }
        #endregion

    }*/
    #endregion

    #region 난이도 잠김

    public void LockLevel()
    {
        string db = leveldb.Mode();
        StageLock(db);
    }   

    private void StageLock(string stage)
    {
        if(stage.Equals("easy"))
        {
            normal = GameObject.Find("Bottle").transform.GetChild(1).gameObject;
            normal.GetComponent<MeshRenderer>().enabled = false;
            normal.GetComponent<BoxCollider>().enabled = false;

            hard = GameObject.Find("Bottle").transform.GetChild(2).gameObject;
            hard.GetComponent<MeshRenderer>().enabled = false;
            hard.GetComponent<BoxCollider>().enabled = false;
        }
        else if(stage.Equals("normal"))
        {
            hard = GameObject.Find("Bottle").transform.GetChild(2).gameObject;
            hard.GetComponent<MeshRenderer>().enabled = false;
            hard.GetComponent<BoxCollider>().enabled = false;
        }
    }
    #endregion

    #region 승률 출력
    public int ReturnResult(string level)
    {
        if (level.Equals("W/LResultEasy"))
        {
            return leveldb.EasyRate(); 
        }
        else if (level.Equals("W/LResultNormal"))
        {
            return leveldb.NormalRate(); 
        }
        else if(level.Equals("W/LResultHard"))
        {
            return leveldb.HardRate();
        }

        return -1;
    }
    #endregion

    #region 회원가입 & 로그인 병 처리
    public void IdBottleHit(GameObject bottle)
    {
        switch (bottle.name)
        {
            case "Login":
                {
                    if (GameObject.Find("Picket").transform.GetChild(1).gameObject.activeSelf == true)
                    {
                        GameObject.Find("Picket").transform.GetChild(1).gameObject.SetActive(false);
                    }
                    GameObject.Find("Picket").transform.GetChild(0).gameObject.SetActive(true);
                    bottle.SetActive(true);
                    break;
                }
            case "Account":
                {
                    if (GameObject.Find("Picket").transform.GetChild(0).gameObject.activeSelf == true)
                    {
                        GameObject.Find("Picket").transform.GetChild(0).gameObject.SetActive(false);
                    }
                    GameObject.Find("Picket").transform.GetChild(1).gameObject.SetActive(true);
                    bottle.SetActive(true);
                    break;
                }
            case "Back":
                ChangeToScene(1); break;

        }
    }
    #endregion

    #region 지난 씬 인덱스 Get & Set
    public void PreSceneIndex()
    {
        p_index = SceneManager.GetActiveScene().buildIndex;
    }

    public int PreSceneIndexCall()
    {
        return p_index;
    }
    #endregion

    #region 관리자 아이디
    public bool Login(string u_id)
    {
        if (id == u_id)
        {
            login = true;
            return login;
        }
        else
            return false;
    }
    #endregion 
}
