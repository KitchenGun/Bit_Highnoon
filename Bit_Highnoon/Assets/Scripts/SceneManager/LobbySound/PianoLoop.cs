using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PianoLoop : MonoBehaviour
{
    private AudioSource piano;
    private GameManager GM;
    // Start is called before the first frame update
    void Start()
    {
        piano = this.gameObject.GetComponent<AudioSource>();
        GM = GameObject.Find("GameManager").GetComponent<GameManager>();
        piano.clip = GM.GetComponent<GameManager>().LoadAudioClip("piano");
    }

    // Update is called once per frame
    void Update()
    {
        if (!piano.isPlaying)
            ClipChange();
    }
    private void ClipChange()
    {
        piano.clip = GM.GetComponent<GameManager>().LoadAudioClip("piano");
        piano.Play();
    }

}
