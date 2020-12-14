using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    GameObject easyai;
    GameObject normalai;

    // Start is called before the first frame update
    void Start()
    {
        easyai = GameObject.Find("EasyAI");
        normalai = GameObject.Find("NormalAI");
    }

    public void GameStart()
    {
        easyai.SendMessage("GameStart");
        normalai.SendMessage("GameStart");
    }

    public void AIHit()
    {
        easyai.SendMessage("Hit");
        normalai.SendMessage("Hit");
    }

    public void AIDead()
    {
        easyai.SendMessage("Dead");
        normalai.SendMessage("Dead");
    }

    private void Dead()
    {
        //gameObject.SetActive(false);
        Debug.Log("Player Dead");
    }

    private void Win()
    {
        //gameObject.SetActive(false);
        Debug.Log("Win");
    }
}
