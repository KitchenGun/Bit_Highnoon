using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    #region 업데이트 기능 함수
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F1))    //유리병 깨지는 이벤트 발생시로 변경
        {
            //싱글 메뉴 이동
            int idx = SceneManager.GetActiveScene().buildIndex + 1;
            GameManager.Instance.NextToScene(idx);
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
}
