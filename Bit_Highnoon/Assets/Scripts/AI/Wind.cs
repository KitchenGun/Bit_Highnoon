using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wind : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        AudioSource audio = gameObject.GetComponent<AudioSource>();
        audio.clip = GameManager.Instance.LoadAudioClip("WindSound");
        audio.loop = true;

        audio.Play();
    }
}
