﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using OVRTouchSample;


public class HandGunRayCast : MonoBehaviour
{
    #region 사격 위치와 맞은 오브젝트 변수
    private GameObject FirePos; // Ray가  시작할 위치 정보를 가진 오브젝트
    private RaycastHit HitObj;
    #endregion

    #region 격발 관련 변수
    private bool FireState;
    private int Bullet;
    private bool ReloadState;
    private float ReloadStick;
    //총알 무한을 위한 씬넘버 획득용
    private GameObject GM;
    private int SceneIdx;
    #endregion

    #region audio
    private AudioSource HandGunAudio;
    [SerializeField]
    private AudioClip GunFire_SFX;
    [SerializeField]
    private AudioClip GunBulletEmpty_SFX;
    [SerializeField]
    private AudioClip GunReload_SFX;

    #endregion

    void Start()
    {
        GM = GameObject.Find("GameManager");
        SceneIdx = GM.GetComponent<GameManager>().GetSceneIndex();
        FirePos = this.gameObject.transform.parent.Find("GunFirePos").gameObject;
        this.HandGunAudio = this.gameObject.transform.parent.GetComponent<AudioSource>();
        this.HandGunAudio.loop = false;
    }

    void Update()
    {
        #region Ray 발사
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

        #region 재장전
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
        if(ReloadState == true)
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

    #region 격발음 & 격발 이펙트
    private void Gun_Fire_SFX()
    {
        HandGunAudio.clip = this.GunFire_SFX;
        HandGunAudio.Play();
    }
    private void Gun_BulletEmpty_SFX()
    {
        HandGunAudio.clip = this.GunBulletEmpty_SFX;
        HandGunAudio.Play();
    }
    private void Gun_Reload_SFX()
    {
        HandGunAudio.clip = this.GunReload_SFX;
        HandGunAudio.Play();
    }
    #endregion

    #region 총 재장전 사격 관련 함수 & 정보 전달 및 전송 
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
            Gun_Fire_SFX();
            return true;
        }
        else if(!FireState)
        {
            FireState = false;
            Gun_BulletEmpty_SFX();
            return false;
        }
        else
        {
            FireState = false;
            Gun_BulletEmpty_SFX();
            return false;
        }
    }
    private void Reload()//재장전
    {
        FireState = true;
        ReloadState = false;
        Gun_Reload_SFX();
    }

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

    #region 병 식별 시 실행
    private void BottleHit(GameObject bottle)
    {
        bottle.GetComponent<BottleScript>().SendMessage("Hit");
    }
    #endregion

    #region 적 식별 시 실행
    private void EnemyHit(GameObject enemy)
    {
        enemy.GetComponent<AIParent>().SendMessage("Hit");
    }
    #endregion


}
