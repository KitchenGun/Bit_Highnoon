using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test : MonoBehaviour
{
    SoundDB db = new SoundDB();
    // Start is called before the first frame update
    void Start()
    {
        AudioSource audio = this.gameObject.transform.GetComponent<AudioSource>();
        audio.clip = db.GunFireFile("fire2");
        audio.Play();
    }
}
