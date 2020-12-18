using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using OVRTouchSample;


public class HandGunRayCast : MonoBehaviour
{
    #region Ray
    private GameObject FirePos; // Ray가  시작할 위치 정보를 가진 오브젝트
    private RaycastHit HitObj;
    #endregion
    #region 사격관련 변수
    private bool FireState;
    private int Bullet; 
    private bool ReloadState;
    private float ReloadStick;
    //총알 무한을 위한 씬넘버 획득용
    private GameObject GM;
    private int SceneIdx;
    #endregion
    #region audio
    private AudioSource HandGunFireClickAudio;
    private AudioSource HandGunReloadAudio;
    private AudioSource HandGunFireAudio;
    [SerializeField]
    private AudioClip GunFire_SFX;
    [SerializeField]
    private AudioClip GunBulletEmpty_SFX;
    [SerializeField]
    private AudioClip GunReload_SFX;
    #endregion
    #region Animation
    private Animator GunAni;
    #endregion

    void Start()
    {//초기화
        #region Animation
        GunAni = this.gameObject.GetComponent<Animator>();
        #endregion
        #region Scene
        GM = GameObject.Find("GameManager");
        if (GM == null)
        {
            SceneIdx = 0;
        }//gamemanager가 존재 안할경우
        else
        {
            SceneIdx = GM.GetComponent<GameManager>().GetSceneIndex();
        }
        #endregion
        #region Ray
        FirePos = this.gameObject.transform.parent.Find("GunFirePos").gameObject;
        #endregion
        #region Audio
        this.HandGunFireClickAudio = this.gameObject.transform.parent.GetComponent<AudioSource>(); //격발음 SFX
        this.HandGunFireClickAudio.loop = false;
        this.HandGunReloadAudio = this.gameObject.GetComponent<AudioSource>();//노리쇠 후퇴 SFX
        this.HandGunReloadAudio.loop = false;
        this.HandGunFireAudio = FirePos.GetComponent<AudioSource>();
        this.HandGunFireAudio = FirePos.GetComponent<AudioSource>();
        #endregion
    }

    private void Update()
    {
        #region 사격
        GunAni.SetBool("FireState", FireState);
        switch (FirePos.tag)//발사 위치 오브젝트의 태그를 통해서 판별
        {
            case "Left"://왼쪽
                if (OVRInput.GetDown(OVRInput.Button.PrimaryIndexTrigger, OVRInput.Controller.Touch))
                {
                    if (Fire())//총알 발사
                    {
                        Debug.DrawRay(FirePos.transform.position, FirePos.transform.forward * 2000, Color.red, 0.3f);//개발 확인용 레이 
                        if (Physics.Raycast(FirePos.transform.position, FirePos.transform.forward, out HitObj, 2000))
                        {
                            if (HitObj.transform.gameObject.tag == "Bottle")
                            {
                                BottleHit(HitObj.transform.gameObject);
                            }
                            else if (HitObj.transform.gameObject.tag == "Enemy")
                            {
                                EnemyHit(HitObj.transform.gameObject);
                            }
                        }
                    }
                }
                break;
            case "Right": //오른쪽
                if (OVRInput.GetDown(OVRInput.Button.SecondaryIndexTrigger, OVRInput.Controller.Touch))
                {
                    if (Fire())//총알 발사
                    {
                        Debug.DrawRay(FirePos.transform.position, FirePos.transform.forward * 2000, Color.red, 0.3f);//개발 확인용 레이 
                        if (Physics.Raycast(FirePos.transform.position, FirePos.transform.forward, out HitObj, 2000))
                        {
                            if (HitObj.transform.gameObject.tag == "Bottle")
                            {
                                BottleHit(HitObj.transform.gameObject);
                            }
                            else if (HitObj.transform.gameObject.tag == "Enemy")
                            {
                                EnemyHit(HitObj.transform.gameObject);
                            }
                        }
                    }
                }
                break;
        }
        #endregion

        #region 재장전 가능상황 파악
        if (FireState == false)//발사 불가능 상태 일 경우
        {
            switch (FirePos.tag)//발사 위치 오브젝트의 태그를 통해서 판별
            {
                case "Left"://왼쪽
                    if (OVRInput.Get(OVRInput.Axis2D.PrimaryThumbstick, OVRInput.Controller.Touch) == new Vector2(0, 0))
                    {
                        ReloadState = true;
                    }
                    break;
                case "Right": //오른쪽
                    if (OVRInput.Get(OVRInput.Axis2D.SecondaryThumbstick, OVRInput.Controller.Touch) == new Vector2(0, 0))
                    {
                        ReloadState = true;
                    }
                    break;
            }
        }
        #endregion

        #region 재장전
        if (ReloadState == true)
        {
            switch (FirePos.tag)//발사 위치 오브젝트의 태그를 통해서 판별
            {
                case "Left"://왼쪽
                    ReloadStick = OVRInput.Get(OVRInput.Axis2D.PrimaryThumbstick).y;//스틱컨트롤러y축 버튼
                    if (ReloadStick < -0.9)
                    {
                        Reload();
                    }
                    break;
                case "Right": //오른쪽
                    ReloadStick = OVRInput.Get(OVRInput.Axis2D.SecondaryThumbstick).y;//스틱컨트롤러y축 버튼
                    if (ReloadStick < -0.9)
                    {
                        Reload();
                    }
                    break;
            }
        }
        #endregion
    }

    #region SFX
    private void Gun_Fire_SFX()
    {
        HandGunFireAudio.clip = this.GunFire_SFX;
        HandGunFireAudio.Play();
    }
    private void Gun_BulletEmpty_SFX()
    {
        HandGunFireClickAudio.clip = this.GunBulletEmpty_SFX;
        HandGunFireClickAudio.Play();
    }
    private void Gun_Reload_SFX()
    {
        HandGunReloadAudio.clip = this.GunReload_SFX;
        HandGunReloadAudio.Play();
    }
    #endregion

    #region Gun
    private bool Fire()
    {
        if (Bullet > 0&&FireState)//총알이 있고 발사가능상태
        {
            //총알 감소 격발 상태 
            if (SceneIdx == 1 || SceneIdx == 2)//메뉴 씬이 아닐 경우
            {
                Debug.Log(SceneIdx);
            }
            else
            {
                Bullet--;
            }
            FireState = false;
            //격발 효과
            GunAni.SetTrigger("Fire");
            GunAni.SetBool("FireState", FireState);
            return true;
        }
        else if(!FireState)
        {
            GunAni.SetTrigger("FireF");
            FireState = false;
            GunAni.SetBool("FireState", FireState);
            return false;
        }
        else
        {
            GunAni.SetTrigger("FireF");
            FireState = false;
            GunAni.SetBool("FireState", FireState);
            return false;
        }
    }
    private void Reload()//재장전
    {
        FireState = true;
        ReloadState = false;
        GunAni.SetTrigger("Reload");
    }
    #endregion
    
    #region Gun 정보 전달
    public void setGunInfo(ref bool firestate, ref int bullet)
    {
        this.FireState=firestate;
        this.Bullet = bullet;
    }
    public void getGunInfo(ref bool firestate,ref int bullet)
    {
        firestate = this.FireState;
        bullet = this.Bullet;
    }
    #endregion

    #region 병 식별
    private void BottleHit(GameObject bottle)
    {
        bottle.GetComponent<BottleScript>().SendMessage("Hit");
    }
    #endregion

    #region 적 식별
    private void EnemyHit(GameObject enemy)
    {
        enemy.GetComponent<AIParent>().SendMessage("Hit");
    }
    #endregion
}
