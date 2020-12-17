﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTracking : MonoBehaviour
{
    [SerializeField]
    private GameObject Head; //머리의 위치정보를 담고 있는 오브젝트
    private GameObject Body; //플레이어의 머리 밑에 같이 따라 움직이는 몸통 오브젝트
    [SerializeField]
    private GameObject Belt;//사용자가 가지고 있는 홀스터


    void Start()
    {
        Body = this.gameObject;
    }

    void Update()
    {
        #region Body 회전과 위치 값 전달
        Body.transform.position = new Vector3
            (Head.transform.position.x,
            0,//이동 기능 제작시 플레이어의 최상위 오브젝트의 위치값을 받아오도록 제작
            Head.transform.position.z);
        Body.transform.eulerAngles = new Vector3(0, Head.transform.rotation.eulerAngles.y, 0); // 회전값 y축 만 전달
        #endregion
        #region 크기 변화
        Body.transform.localScale = new Vector3(
            Body.transform.localScale.x,
            (float)(Head.transform.position.y) / 2,
            Body.transform.localScale.z);
        #endregion
        #region  벨트 위치 회전값 변경
        Belt.transform.position = new Vector3(
            Body.transform.position.x,
            (float)(Head.transform.position.y)/2,
            Body.transform.position.z);
        Belt.transform.eulerAngles = new Vector3(0, Head.transform.rotation.eulerAngles.y, 0); // 회전값 y축 만 전달
        #endregion
    }
}
