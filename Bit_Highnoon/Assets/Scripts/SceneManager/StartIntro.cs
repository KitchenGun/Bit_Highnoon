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
    public Light pointlight;
    private AudioSource audios;
    
    void Start()
    {
        audios = GetComponent<AudioSource>();

        GameManager.Instance.GameStart();

        audios.clip = GameManager.Instance.LoadAudioClip("fire");

        Invoke("InvokeBullet1", 1.0f);

        Invoke("InvokeBullet2", 2.0f);

        Invoke("InvokeBullet3", 2.3f);

        Invoke("InvokeLogo", 2.7f);

        for (float i = 0.0f; i < 10; i++)
        {
            Invoke("InvokeLight", 2.7f+(0.02f*i));
        }

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
        audios.Play();
    }
    void InvokeBullet2()
    {
        bullet2.SetActive(true);
        audios.Play();
    }
    void InvokeBullet3()
    {
        bullet3.SetActive(true);
        audios.Play();
    }
    void InvokeLogo()
    {
        logo.SetActive(true);   
    }
    #endregion

    void InvokeLight()
    {
        /*
        for(int i = 0; i < 20; i++)
        {
            pointlight.intensity = pointlight.intensity + 0.1f;
        }
        */

        GameManager.Instance.GameEnd();

        if (pointlight.intensity < 2.0f)
            pointlight.intensity = pointlight.intensity + 0.2f;
        
    }
}
