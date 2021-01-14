using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Net_HoldFire : MonoBehaviourPunCallbacks
{
    private PhotonView PV;
    
    private GameManager GM;
    private int SceneNum;
    private List<GameObject> Holster;
    // Start is called before the first frame update
    private void Awake()
    {
        PV = this.gameObject.GetPhotonView();//this.gameObject.transform.parent.GetComponent<PhotonView>();
        #region 씬확인
        GM = GameObject.Find("GameManager").GetComponent<GameManager>();
        SceneNum = GM.GetSceneIndex();
        #endregion
        #region 홀스터
        Holster = new List<GameObject>();
        Holster.Add(this.gameObject.GetComponent<Net_PlayerTracking>().GetBeltObj().transform.GetChild(1).gameObject);
        Holster.Add(this.gameObject.GetComponent<Net_PlayerTracking>().GetBeltObj().transform.GetChild(2).gameObject);
        #endregion
        //확인한 씬을 통해서 총기 사용가능을 확인
        SceneCheck();
    }

    #region 씬에 따른 충돌체 상태 초기화
    private void SceneCheck()
    {
        if(SceneNum==3||SceneNum==4||SceneNum==5)
        {
            HoldGunFire();
        }
        else
        {
            OpenFire();
        }
    }
    #endregion

    #region 사격 불&가능하게 하는 함수
    private void OpenFire()
    {
        foreach (GameObject holsterGun in Holster)
        {
            holsterGun.GetComponent<SphereCollider>().enabled = true;
            holsterGun.transform.GetChild(0).gameObject.GetComponent<BoxCollider>().enabled = true;
        }
    }

    private void HoldGunFire()
    {
        foreach (GameObject holsterGun in Holster)
        {
            holsterGun.GetComponent<SphereCollider>().enabled = false;
            holsterGun.transform.GetChild(0).gameObject.GetComponent<BoxCollider>().enabled = false;
        }
    }
    #endregion

}
