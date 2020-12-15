using System.Collections;
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
    #endregion

    #region audio
    private AudioSource HandGunAudio;
    [SerializeField]
    private AudioClip GunFire_SFX;

    #endregion

    void Start()
    {
        FirePos = this.gameObject.transform.parent.Find("GunFirePos").gameObject;
        this.HandGunAudio = this.gameObject.transform.parent.GetComponent<AudioSource>();
        this.HandGunAudio.loop = false;
    }

    void Update()
    {
        #region Ray 발사
        if (FireState == true)//발사 가능 상태 일 경우
        {
            switch (FirePos.tag)//발사 위치 오브젝트의 태그를 통해서 판별
            {
                case "Left"://왼쪽
                    if (OVRInput.GetDown(OVRInput.Button.PrimaryIndexTrigger, OVRInput.Controller.Touch))
                    {
                        Fire();
                        Debug.DrawRay(FirePos.transform.position, FirePos.transform.forward * 2000, Color.red, 0.3f);//개발 확인용 레이 
                        if (Physics.Raycast(FirePos.transform.position, FirePos.transform.forward, out HitObj, 2000))
                        {
                            if (HitObj.transform.gameObject.tag == "Bottle")
                            {
                                BottleHit(HitObj.transform.gameObject);
                            }
                        }
                    }
                    break;
                case "Right": //오른쪽
                    if (OVRInput.GetDown(OVRInput.Button.SecondaryIndexTrigger, OVRInput.Controller.Touch))
                    {
                        Gun_Fire_FX();
                        Debug.DrawRay(FirePos.transform.position, FirePos.transform.forward * 2000, Color.red, 0.3f);//개발 확인용 레이 
                        if (Physics.Raycast(FirePos.transform.position, FirePos.transform.forward, out HitObj, 2000))
                        {
                            if (HitObj.transform.gameObject.tag == "Bottle")
                            {
                                BottleHit(HitObj.transform.gameObject);
                            }
                        }
                    }
                    break;
            }
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
                    if (ReloadStick<-0.9)
                    {
                        Reload();
                    }
                    break;
                case "Right": //오른쪽
                   ReloadStick = OVRInput.Get(OVRInput.Axis2D.SecondaryThumbstick).y;//스틱컨트롤러y축 버튼
                    if (ReloadStick<-0.9)
                    {
                        Reload();
                    }
                    break;
            }
        }
        #endregion
    }

    #region 격발음 & 격발 이펙트
    private void Gun_Fire_FX()
    {
        HandGunAudio.clip = this.GunFire_SFX;
        HandGunAudio.Play();
    }
    #endregion

    #region 총 관련 함수 & 정보 전달 및 전송 
    private void Fire()
    {
        //총알 감소 격발 상태 
        --Bullet;
        FireState = false;
        //격발 효과
        Gun_Fire_FX();
    }
    private void Reload()
    {
        FireState = true;
        ReloadState = false;
    }

    public void setGunInfo(bool firestate, int bullet)
    {
        this.FireState=firestate;
        this.Bullet = bullet;
    }
    public void getGunInfo(out bool firestate,out int bullet)
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


}
