﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawnpoint : MonoBehaviour
{
    [SerializeField] GameObject graphics;

    void Awake()
    {
        this.transform.SetParent(GameObject.Find("GameManager").transform);
        graphics.SetActive(false);
    }
}
