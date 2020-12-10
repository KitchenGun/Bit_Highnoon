using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SingleMenu : MonoBehaviour
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
}
