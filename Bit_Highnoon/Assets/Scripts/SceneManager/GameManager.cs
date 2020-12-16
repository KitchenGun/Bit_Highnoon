﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
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
        if(instance == null)
        {
            instance = this;
        }

        else if(instance != this)
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);
    }
    #endregion

    #region 함수
    //씬 이동
    public void ChangeToScene(int idx)
    {
        SceneManager.LoadScene(idx);
    }
   

    public void MoveScene(GameObject bottle)
    {
        if (SceneManager.GetActiveScene().buildIndex == 1)
        {
            switch (bottle.name)
            {
                case "Single":
                    ChangeToScene(2); break;
                case "Multi":
                    ChangeToScene(7); break;
                case "Option":
                    ChangeToScene(10); break;
                case "Exit":
                    ExitGame(); break;
            }
        }
        else if (SceneManager.GetActiveScene().buildIndex == 2)
        {
            switch (bottle.name)
            {
                case "":
                    ChangeToScene(2); break;
                case "Multi":
                    ChangeToScene(7); break;
                case "Option":
                    ChangeToScene(10); break;
                case "Exit":
                    ExitGame(); break;
            }
        }
    }

    //게임 종료
    public void ExitGame()
    {
        Application.Quit();
    }
    #endregion

    /*
    #region Update함수
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

    }
    #endregion
    */
}
