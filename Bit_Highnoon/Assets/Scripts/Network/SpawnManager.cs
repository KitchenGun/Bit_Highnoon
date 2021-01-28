using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class SpawnManager : MonoBehaviour
{
    public static SpawnManager instance;

    Spawnpoint[] spawnpoints;

    void Awake()
    {
        instance = this;
        spawnpoints = GetComponentsInChildren<Spawnpoint>();
    }

    private void Start()
    {
        
    }

    public Transform GetSpawnpoint()
    {
        return spawnpoints[Random.Range(0, spawnpoints.Length)].transform;
    }
}
