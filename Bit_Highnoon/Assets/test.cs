using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test : MonoBehaviour
{
    SoundDB db;
    // Start is called before the first frame update
    void Start()
    {
        db = this.gameObject.GetComponent<SoundDB>();

        for (int i = 0; i < 5; i++)
        {
            db.GunFireSound();
        }
    }
}
