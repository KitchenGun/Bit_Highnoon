using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIEasy : AIParent
{
    protected override void Start()
    {
        base.Start();

        idletime = 5;   //대기시간
            
        deadtime = 20;  //플레이어가 죽는 시간

        StartCoroutine(CheckState());               //상태를 체크
        StartCoroutine(CheckStateForAction());      //상태의 따른 행동
    }

    protected override void IdleAction()
    {
        base.IdleAction();

        animator.SetTrigger("drink");
    }

    protected override void AttackAction()
    {
        base.AttackAction();

        animator.SetTrigger("attack");
    }


    private void Hit()
    {
        animator.SetTrigger("hit");
    }

    private void Dead()
    {
        animator.SetTrigger("dead");
        isdead = true;
    }
}
