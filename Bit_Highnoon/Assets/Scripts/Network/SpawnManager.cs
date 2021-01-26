using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public static SpawnManager instance;

    Spawnpoint[] spawnpoints;

    List<Spawnpoint> spawnpointlist;

    void Awake()
    {
        instance = this;
        spawnpoints = GetComponentsInChildren<Spawnpoint>();

        spawnpointlist.AddRange(spawnpoints);
    }

    public Transform GetSpawnpoint()
    {
        Transform spawnpoint = spawnpointlist[0].transform;

        spawnpointlist.Remove(spawnpointlist[0]);

        return spawnpoint;
    }
}
