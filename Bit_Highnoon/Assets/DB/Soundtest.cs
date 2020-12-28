using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class soundtest : MonoBehaviour
{
    SoundDB db = new SoundDB();
    List<AudioClip> list = new List<AudioClip>();
    // Start is called before the first frame update
    void Start()
    {
        AudioSource audio = this.gameObject.transform.GetComponent<AudioSource>();
        audio.clip = db.GunReloadSound();
        audio.Play();
    }
}
