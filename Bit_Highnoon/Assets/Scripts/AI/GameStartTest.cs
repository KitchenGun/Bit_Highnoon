using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStartTest : MonoBehaviour
{
    [SerializeField]
    private AudioClip gameStartAudio;

    private IEnumerator GameStart()
    {
        AudioSource Audio = GetComponent<AudioSource>();

        Audio.clip = gameStartAudio;
        Audio.loop = false;
        
        Audio.Play();

        yield return new WaitForSeconds(1.5f); 

        PlayerStart();
    }

    private void PlayerStart()
    {
        GameObject player = GameObject.Find("PlayerCtrl");

        player.SendMessage("GameStart");
    }

    [SerializeField]
    private AudioClip gameEndAudio;

    private void GameEnd()
    {
        AudioSource Audio = GetComponent<AudioSource>();

        Audio.clip = gameEndAudio;
        Audio.loop = false;

        Audio.Play();
    }
}
