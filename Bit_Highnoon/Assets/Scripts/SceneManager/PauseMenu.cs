using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    #region 업데이트 기능 함수
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F1))    //버튼
        {
            //게임 재개
        }

        if (Input.GetKeyDown(KeyCode.F2))    //버튼 / 옵션창
        {
            //옵션
        }

        if (Input.GetKeyDown(KeyCode.F3))    //버튼
        {
            //메인 메뉴 이동
            int idx = SceneManager.GetActiveScene().buildIndex;
            GameManager.Instance.BackToScene(idx);
        }

        if (Input.GetKeyDown(KeyCode.F4))    //버튼
        {
            //게임 종료
            GameManager.Instance.ExitGame();
        }
    }
    #endregion
}
