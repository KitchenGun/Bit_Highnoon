using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    GameObject easyai;
    GameObject normalai;
    GameObject hardlai;

    private bool isGameStart;

    // Start is called before the first frame update
    void Start()
    {
        easyai = GameObject.Find("EasyAI");
        normalai = GameObject.Find("NormalAI");
        hardlai = GameObject.Find("HardAI");

        isGameStart = false;
    }

    public void AIHit_easy()
    {
            easyai.SendMessage("Hit");
    }

    public void AIHit_normal()
    {
            normalai.SendMessage("Hit");
    }

    public void AIHit_hard()
    {
            hardlai.SendMessage("Hit");
    }

    public void AIHit()
    {
        if (isGameStart == true)
        {
            easyai.SendMessage("Hit");
            normalai.SendMessage("Hit");
            hardlai.SendMessage("Hit");
        }
    }

    private void GameStart()
    {
        Debug.Log("GameStart");
    }

    private void Dead()
    {
        //gameObject.SetActive(false);
        Debug.Log("Player Dead");

        isGameStart = false;
    }

    private void Win()
    {
        //gameObject.SetActive(false);
        Debug.Log("Win");
    }
}
