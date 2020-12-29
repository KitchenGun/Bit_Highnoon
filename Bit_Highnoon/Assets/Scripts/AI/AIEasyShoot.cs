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

    private void PlayerDead()
    {
        easyAI.SendMessage("PlayerDead");
    }

    private void GameEnd()
    {
        easyAI.SendMessage("GameEnd");
    }
}
