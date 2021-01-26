using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class SpawnManager : MonoBehaviour
{
    public static SpawnManager instance;

    Spawnpoint[] spawnpoints;

    PhotonView PV;

    int count;

    void Awake()
    {
        instance = this;
        PV = this.gameObject.GetPhotonView();
        spawnpoints = GetComponentsInChildren<Spawnpoint>();

        IsCount();
    }

    public Transform GetSpawnpoint()
    {
        Transform spawnpoint = spawnpoints[count].transform;

        PV.RPC("Count", RpcTarget.AllBuffered);

        return spawnpoint;
    }

    [PunRPC]
    private void Count()
    {
        count++;
    }

    private void IsCount()  //카운트의 값을 판단한다.
    {
        if (count >= 1)
        { }
        else
            count = 0;
    }
}
