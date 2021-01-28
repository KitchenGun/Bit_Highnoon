﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using System.IO;
using UnityEngine.SceneManagement;

public class LobbySpawn : MonoBehaviourPunCallbacks
{
    //[SerializeField] string player_prefab;
    PhotonView PV;

    GameObject controller;
    // Start is called before the first frame update
    void Awake()
    {
        PV = GetComponent<PhotonView>();
    }

    // Update is called once per frame
    void Start()
    {
        if (PV.IsMine)
        {
            CreateController();
        }
    }
    void CreateController()
    {
        /*Transform spawnpoint = SpawnManager.instance.GetSpawnpoint();
        controller = PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "NetworkPlayer"), spawnpoint.position, spawnpoint.rotation,0,new object[] { PV.ViewID });
        */
        controller = PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "NetworkPlayer"), transform.position, transform.rotation);
    }
}
