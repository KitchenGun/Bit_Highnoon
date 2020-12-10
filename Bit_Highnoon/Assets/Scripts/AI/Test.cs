using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    GameObject easyai;

    // Start is called before the first frame update
    void Start()
    {
        easyai = GameObject.Find("EasyAI");
    }

    public void AIHit()
    {
        easyai.SendMessage("Hit");
    }

    public void AIDead()
    {
        easyai.SendMessage("Dead");
    }

    private void Dead()
    {
        gameObject.SetActive(false);
    }

    private void Win()
    {
        gameObject.SetActive(false);
    }
}
