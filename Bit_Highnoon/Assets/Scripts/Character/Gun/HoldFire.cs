using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoldFire : MonoBehaviour
{
    private GameManager GM;
    private int SceneNum;
    private List<GameObject> Holster;
    // Start is called before the first frame update
    void Start()
    {
        #region 씬확인
        GM = GameObject.Find("GameManager").GetComponent<GameManager>();
        SceneNum = GM.GetSceneIndex();
        SceneCheck();
        #endregion
        #region 홀스터
        Holster = new List<GameObject>();
        Holster.Add(this.gameObject.GetComponent<PlayerTracking>().GetBeltObj().transform.GetChild(1).gameObject);
        Holster.Add(this.gameObject.GetComponent<PlayerTracking>().GetBeltObj().transform.GetChild(2).gameObject);
        #endregion
    }

    #region 씬에 따른 충돌체 상태 초기화
    private void SceneCheck()
    {
        if(SceneNum==3||SceneNum==4||SceneNum==5)
        {
            foreach(GameObject holsterGun in Holster)
            {
                holsterGun.GetComponent<SphereCollider>().enabled = false;
            }
        }
        else
        {
            foreach (GameObject holsterGun in Holster)
            {
                holsterGun.GetComponent<SphereCollider>().enabled = true;
            }
        }
    }
    #endregion

    #region 사격 가능하게 하는 함수
    private void OpenFire()
    {
        foreach (GameObject holsterGun in Holster)
        {
            holsterGun.GetComponent<SphereCollider>().enabled = true;
        }
    }
    #endregion

}
