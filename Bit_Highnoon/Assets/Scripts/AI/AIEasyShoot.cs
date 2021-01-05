using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIEasyShoot : MonoBehaviour
{
    GameObject easyAI;

    void Start()
    {
        easyAI = GameObject.Find("EasyAI");    
    }

    private void AttackAudio()
    {
        easyAI.SendMessage("AttackAudio");
    }

    private void SendMessageDead()
    {
        easyAI.SendMessage("SendMessageDead");
    }

    private void GameEnd()
    {
        easyAI.SendMessage("GameEnd");
    }

    private void PlayerDead()
    {
        easyAI.SendMessage("PlayerDead");
    }
}
