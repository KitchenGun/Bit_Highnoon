using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test : MonoBehaviour
{
    LogicalDB db = new LogicalDB();
    // Start is called before the first frame update
    void Start()
    {
        SoundDB db = new SoundDB();

        for (int i = 0; i < 5; i++)
        {
            db.GunFireSound();
        }
    }
}
