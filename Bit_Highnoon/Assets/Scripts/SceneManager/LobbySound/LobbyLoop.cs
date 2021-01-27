using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LobbyLoop : MonoBehaviour
{
    private AudioSource Casino;
    private GameManager GM;
    // Start is called before the first frame update
    void Start()
    {
        Casino = this.gameObject.GetComponent<AudioSource>();
        GM = GameObject.Find("GameManager").GetComponent<GameManager>();
        Casino.clip = GM.GetComponent<GameManager>().LoadAudioClip("casino");
    }

    void Update()
    {
        if (!Casino.isPlaying)
            ClipChange();
    }
    private void ClipChange()
    {
        Casino.clip = GM.GetComponent<GameManager>().LoadAudioClip("casino");
        Casino.Play();
    }

}
