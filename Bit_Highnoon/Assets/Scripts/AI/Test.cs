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
}
