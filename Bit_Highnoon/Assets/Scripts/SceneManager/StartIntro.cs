using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartIntro : MonoBehaviour
{
    // Start is called before the first frame update

    #region 시간 지연 씬 전환
    void Start()
    {
        Invoke("InvokeMain", 5f);
    }

    void InvokeMain()
    {
        //현재 인덱스의 다음 인덱스값 저장
        int idx = SceneManager.GetActiveScene().buildIndex + 1;

        //씬 넘기는 싱글톤 함수로 인덱스값 전달
        GameManager.Instance.NextToScene(idx);
    }
    #endregion
}
