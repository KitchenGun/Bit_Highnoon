﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NpcCycle : MonoBehaviour
{
    private Animator animator;
    private float Timer;
    private bool tr;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        tr = true;
    }

    // Update is called once per frame
    void Update()
    {
        if(tr == true)
        {
            Timer = Timer + Time.deltaTime;
        }

        if (this.gameObject.name == "card_table1_1" || this.gameObject.name == "card_table1_2")
        {
            if (Timer > 10.0f)
            {
                animator.SetInteger("New Int", 10);
            }

            if (Timer > 40.0f)
            {
                animator.SetInteger("New Int", 40);
            }

            if (Timer > 50.0f)
            {
                animator.SetInteger("New Int", 50);
            }

            if (Timer > 80.0f)
            {
                animator.SetInteger("New Int", 80);
                Timer = 0.0f;
            }
        }
        else if(this.gameObject.name == "card_table3_1" || this.gameObject.name == "card_table3_2")
        {
            if (Timer > 30.0f)
            {
                animator.SetInteger("New Int", 30);
            }

            if (Timer > 40.0f)
            {
                animator.SetInteger("New Int", 40);
            }

            if (Timer > 70.0f)
            {
                animator.SetInteger("New Int", 70);
            }

            if (Timer > 80.0f)
            {
                animator.SetInteger("New Int", 80);
                Timer = 0.0f;
            }
        }
        else if(this.gameObject.name == "E1")
        {
            if (Timer > 5.0f)
            {
                animator.SetInteger("New Int", 5);
            }

            if (Timer > 35.0f)
            {
                animator.SetInteger("New Int", 35);
                Timer = 0.0f;
            }
        }
    }

    #region 이벤트 오류 방지
    private void IdleAudio()
    {

    }

    private void GameStart()
    {

    }
    #endregion
}
