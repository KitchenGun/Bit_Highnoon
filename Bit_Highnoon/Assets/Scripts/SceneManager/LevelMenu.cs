using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelMenu : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    #region 업데이트 기능 함수
    // Update is called once per frame
    void Update()
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
}
