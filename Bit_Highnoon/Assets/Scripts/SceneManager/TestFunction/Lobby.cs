using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Lobby : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    #region 업데이트 기능 함수
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F1))    //유리병 깨지는 이벤트 발생시로 변경 or 버튼
        {
            //게임 시작
            int idx = SceneManager.GetActiveScene().buildIndex + 1;
            GameManager.Instance.NextToScene(idx);
        }

        if (Input.GetKeyDown(KeyCode.F1))    //유리병 깨지는 이벤트 발생시로 변경 or 버튼
        {
            //뒤로가기
            int idx = SceneManager.GetActiveScene().buildIndex;
            GameManager.Instance.BackToScene(idx);
        }
    }
    #endregion
}
