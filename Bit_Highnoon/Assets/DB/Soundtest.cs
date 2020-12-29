using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Soundtest : MonoBehaviour
{
    SoundDB db = new SoundDB();
    // Start is called before the first frame update
    void Start()
    {
        AudioSource audio = this.gameObject.transform.GetComponent<AudioSource>();

        //순서 맞출것
        //1
        db.GunDropSound();
        //2
        db.GunGripSound();
        //3
        db.GunFireSound();
        //4
        db.GunFire3dSound();
        //5
        db.GunFireWhizSound();
        //6
        db.GunReloadSound();

        audio.clip = db.Reload1();
        audio.Play();
    }
}
