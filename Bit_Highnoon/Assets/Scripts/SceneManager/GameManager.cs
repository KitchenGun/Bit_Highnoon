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
    private bool login = false;
    private string id = "SEX";
    private int pre_idx;

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

    #region 로그인 여부 반환
    public bool Login(string u_id)
    {
        if(id == u_id)
        {
            login = true;
            return login;
        }
        else
            return false;
    }
    #endregion

    #region 전 씬 인덱스 get & set
    public void PreSceneIndex()
    {
        pre_idx = SceneManager.GetActiveScene().buildIndex;
    }

    public int PreSceneIndexCall()
    {
        return pre_idx;
    }
    #endregion
}
