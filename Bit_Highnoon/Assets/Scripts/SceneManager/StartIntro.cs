using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartIntro : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject bullet1;
    public GameObject bullet2;
    public GameObject bullet3;
    public GameObject logo;
    
    void Start()
    {
        Invoke("InvokeBullet1", 1.0f);
        Invoke("InvokeBullet2", 2.0f);
        Invoke("InvokeBullet3", 2.5f);

        Invoke("InvokeLogo", 3.0f);

        Invoke("InvokeMain", 5f);
    }

    #region 시간 지연 씬 전환
    void InvokeMain()
    {
        //현재 인덱스의 다음 인덱스값 저장
        int idx = SceneManager.GetActiveScene().buildIndex + 1;
 
        //씬 넘기는 싱글톤 함수로 인덱스값 전달
        GameManager.Instance.ChangeToScene(idx);
    }
    #endregion

    #region 탄 자국 나타나게 하기
    void InvokeBullet1()
    {
        bullet1.SetActive(true);
    }
    void InvokeBullet2()
    {
        bullet2.SetActive(true);
    }
    void InvokeBullet3()
    {
        bullet3.SetActive(true);
    }

    void InvokeLogo()
    {
        logo.SetActive(true);   
    }
    #endregion
}
