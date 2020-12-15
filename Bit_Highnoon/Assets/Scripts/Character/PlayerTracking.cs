using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTracking : MonoBehaviour
{
    [SerializeField]
    private GameObject Head; //머리의 위치정보를 담고 있는 오브젝트
    private GameObject Body; //플레이어의 머리 밑에 같이 따라 움직이는 몸통 오브젝트


    void Start()
    {
        Body = this.gameObject;
    }

    void Update()
    {
        #region 회전과 위치 값 전달
        Body.transform.position = Head.transform.position - new Vector3(0, 1f, 0);//머리보다 조금 아래에 몸통 배치
        Body.transform.eulerAngles = new Vector3(0, Head.transform.rotation.eulerAngles.y, 0); // 회전값 y축 만 전달
        #endregion
    }
}
