using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using System.IO;

public class PlayerManager : MonoBehaviour
{
    //[SerializeField] string player_prefab;
    public Transform[] Spawn_Points;
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
        Transform t_spawn = Spawn_Points[Random.Range(0, Spawn_Points.Length)];
        PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "PlayerController"), t_spawn.position, t_spawn.rotation);
        //PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "PlayerController"), Vector3.zero, Quaternion.identity);
    }
}
