using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandGunRayCast : MonoBehaviour
{
    private GameObject FirePos; // Ray가  시작할 위치 정보를 가진 오브젝트
    

    void Start()
    {
        FirePos = this.gameObject.transform.Find("GunFirePos").gameObject;
        Debug.Log(FirePos.tag);
    }

    void Update()
    {
        Debug.Log(FirePos.tag);
    }
}
