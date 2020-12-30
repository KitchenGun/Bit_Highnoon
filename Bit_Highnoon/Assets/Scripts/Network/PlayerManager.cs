using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using System.IO;

public class PlayerManager : MonoBehaviour
{   
    //[SerializeField] string player_prefab;
    PhotonView PV;
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
        Transform spawnpoint = SpawnManager.instance.GetSpawnpoint();
        PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "PlayerController"), spawnpoint.position, spawnpoint.rotation,0,new object[] { PV.ViewID });
    }
}
