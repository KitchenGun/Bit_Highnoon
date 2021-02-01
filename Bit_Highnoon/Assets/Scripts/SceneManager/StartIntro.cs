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

        Invoke("InvokeBullet1", 1.5f*2);

        Invoke("InvokeBullet2", 1.5f*2.5f);

        Invoke("InvokeBullet3", 1.5f*3f);

        Invoke("InvokeLogo", 1.5f*3.5f);

        Invoke("InvokeMain", 1.5f * 6f);
    }

    #region 시간 지연 씬 전환
    void InvokeMain()
    {
        //현재 인덱스의 다음 인덱스값 저장
        int idx = SceneManager.GetActiveScene().buildIndex + 1;

        //씬 넘기는 싱글톤 함수로 인덱스값 전달
        GameManager.Instance.NextSceneIndex(idx);
        GameManager.Instance.ChangeToScene(9);
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
        StartCoroutine(InvokeLight());
    }
    #endregion

    private IEnumerator InvokeLight()
    {
        GameObject GM = GameObject.Find("GameManager").gameObject;
        GM.GetComponent<GameManager>().StartCoroutine("GameEnd","Intro");

        for (int i = 0; i < 200; i++)
        {
            pointlight.intensity += 0.01f;
            yield return new WaitForSeconds(0.01f);
        }
    }
}
