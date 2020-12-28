using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStart : MonoBehaviour
{
    [SerializeField]
    private AudioClip gameStartAudio;

    private IEnumerator AudioPlay()
    {
        AudioSource Audio = GetComponent<AudioSource>();

        Audio.clip = gameStartAudio;
        Audio.loop = false;

        if (Audio.isPlaying == false)
            Audio.Play();

        yield return new WaitForSeconds(2.5f);
         
        PlayerStart();
    }

    private void PlayerStart()
    {
        GameObject player = GameObject.Find("PlayerCtrl");

        player.SendMessage("GameStart");
    }
}
