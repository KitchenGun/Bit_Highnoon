using System.Collections;
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
    //다음 씬 이동
    public void NextToScene(int idx)
    {
        SceneManager.LoadScene(idx);
    }

    //뒤로가기 씬 이동
    public void BackToScene(int idx)
    {
        //싱글 메뉴 -> 메인 메뉴
        if(idx == 2 || idx == 7 || idx == 10)
        {
            SceneManager.LoadScene(1);
        }
        else if(idx == 3)
        {
            SceneManager.LoadScene(2);
        }
    }

    //게임 종료
    public void ExitGame()
    {
        Application.Quit();
    }
    #endregion
}
