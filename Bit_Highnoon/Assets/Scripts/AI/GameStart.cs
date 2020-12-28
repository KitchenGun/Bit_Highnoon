using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStart : MonoBehaviour
{
    private AudioSource Audio;

    private GameObject player;

    [SerializeField]
    private AudioClip gameStartAudio;

    // Start is called before the first frame update
    void Start()
    {
        Audio = GetComponent<AudioSource>();

        Audio.clip = gameStartAudio;
        Audio.loop = false;

        player = GameObject.Find("PlayerCtrl");
    }

    private void Update()
    {
        if (Audio.time > 2.5f)
            PlayerStart();
    }

    private void AudioPlay()
    {
        Audio.Play();
    }

    private void PlayerStart()
    {
        player.SendMessage("GameStart");
    }
}
