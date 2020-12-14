using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandGunRayCast : MonoBehaviour
{
    private GameObject FirePos; // Ray가  시작할 위치 정보를 가진 오브젝트
    private RaycastHit HitObj;

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
        Debug.Log(FirePos.tag);
    }

    void Update()
    {
        #region Ray 발사
        switch (FirePos.tag)//발사 위치 오브젝트의 태그를 통해서 판별
        {
            case "Left"://왼쪽
                if(OVRInput.GetDown(OVRInput.Button.PrimaryIndexTrigger, OVRInput.Controller.Touch))
                {
                    HandGunAudio.clip = this.GunFire_SFX;
                    HandGunAudio.Play();
                    Debug.DrawRay(FirePos.transform.position, FirePos.transform.forward * 1000, Color.red, 0.3f);
                    if (Physics.Raycast(FirePos.transform.position, FirePos.transform.forward, out HitObj, 1000))
                    {
                        Debug.Log(HitObj.transform.gameObject.name);
                    }
                }
                break;
            case "Right": //오른쪽
                if (OVRInput.GetDown(OVRInput.Button.SecondaryIndexTrigger, OVRInput.Controller.Touch))
                {
                    HandGunAudio.clip = this.GunFire_SFX;
                    HandGunAudio.Play();
                    Debug.DrawRay(FirePos.transform.position, FirePos.transform.forward * 1000, Color.red, 0.3f);
                    if (Physics.Raycast(FirePos.transform.position, FirePos.transform.forward, out HitObj, 1000))
                    {
                        Debug.Log(HitObj.transform.gameObject.name);
                    }
                }
                break;
        }
        #endregion
    }
}
