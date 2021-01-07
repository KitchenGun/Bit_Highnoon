﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    private SoundDB sounddb;
    public GameObject normal;
    public GameObject hard;


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

        if(instance == null)
        {
            instance = this;
        }

        else if(instance != this)
        {
            Destroy(gameObject);
        }

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
                    {
                        ChangeToScene(2);
                        LockLevel();
                        break;
                    }
                case "Multi":
                    ChangeToScene(6); break;
                case "Option":
                    ChangeToScene(10); break;
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
        AudioSource Audio = GetComponent<AudioSource>();

        Audio.clip = LoadAudioClip("BattleStart");
        Audio.loop = false;

        Audio.Play();

        //yield return new WaitForSeconds(1.5f);  //노래 시작후 플레이어가 공격할 수 있는 시간
        if(GetSceneIndex() != 0)
            PlayerStart();
    }

    private void PlayerStart()
    {
        GameObject player = GameObject.Find("PlayerCtrl");

        player.transform.Find("Body").GetComponent<HoldFire>().SendMessage("OpenFire");
    }    

    public IEnumerator GameEnd(string winner)
    {
        AudioSource Audio = GetComponent<AudioSource>();

        Audio.clip = LoadAudioClip("BattleEnd");
        Audio.loop = false;

        Audio.Play();

        Debug.Log(winner);

        if (winner.Equals("AI"))
        {
            //AI가 이겼을 때
            if (GetSceneIndex() == 3)
            {
                //이지 난이도

            }
            else if (GetSceneIndex() == 4)
            {
                //노말 난이도

            }
            else if(GetSceneIndex() == 5)
            {
                //하드 난이도

            }
        }
        else if(winner.Equals("player"))
        {
            //player가 이겼을 때
            if (GetSceneIndex() == 3)
            {
                //이지 난이도

            }
            else if (GetSceneIndex() == 4)
            {
                //노말 난이도

            }
            else if (GetSceneIndex() == 5)
            {
                //하드 난이도

            }
        }

        if (GetSceneIndex() != 0)
        {
            yield return new WaitForSeconds(7.5f);

            ChangeToScene(1);
        }

        yield return null;
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
            string db = this.gameObject.AddComponent<LogicalDB>().Mode();
            //db에서 가져온 값이 이지일때(노말 하드 잠김)
            if (db == "easy")
            {
                normal.SetActive(false);
                hard.SetActive(false);
            }
            //db에서 가져온 값이 노말일때(하드 잠김)
            else if (db == "normal")
            {
                hard.SetActive(false);
            }
    }   
    #endregion

    #region 승률 출력
    public int ReturnResult(string level)
    {
        if (level.Equals("W/LResultEasy"))
        {
            return 10; //여기다 
        }
        else if (level.Equals("W/LResultNormal"))
        {
            return 20; //여기다 
        }
        else if(level.Equals("W/LResultHard"))
        {
            return 30; //여기다 
        }

        return -1; //여기다 
    }
    #endregion
}
