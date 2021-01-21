using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NpcDelay : MonoBehaviour
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

        //if (this.gameObject.name == "card_npc_1")
        //{
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
        //}
    }
}
