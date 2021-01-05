using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ServerDB;

public class dlltest : MonoBehaviour
{
    void Start()
    {
        ServerManager sdb = new ServerManager();
        sdb.UserInsert("ksw");
    }
}
