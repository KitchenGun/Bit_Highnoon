using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    GameObject easyai;
    GameObject normalai;
    GameObject hardlai;

    // Start is called before the first frame update
    void Start()
    {
        easyai = GameObject.Find("EasyAI");
        normalai = GameObject.Find("NormalAI");
        hardlai = GameObject.Find("HardAI");
    }

    public void AIHit()
    {
        easyai.SendMessage("Hit");
        normalai.SendMessage("Hit");
        hardlai.SendMessage("Hit");
    }

    public void AIDead()
    {
        easyai.SendMessage("Dead");
        normalai.SendMessage("Dead");
        hardlai.SendMessage("Dead");
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
