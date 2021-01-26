using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public static SpawnManager instance;

    Spawnpoint[] spawnpoints;

    int count = 0;

    void Awake()
    {
        instance = this;
        spawnpoints = GetComponentsInChildren<Spawnpoint>();
    }

    public Transform GetSpawnpoint()
    {
        Transform spawnpoint = spawnpoints[count].transform;

        count++;

        return spawnpoint;
    }
}
