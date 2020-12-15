using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIHard : AIParent
{
    protected override void Start()
    {
        base.Start();

        idletime = 3;   //대기 시간

        walktime = 1;   //걷는 시간

        deadtime = 10;  //플레이어가 죽는 시간

        StartCoroutine(CheckState());               //상태를 체크
        StartCoroutine(CheckStateForAction());      //상태의 따른
    }

    protected override void AttackAction()
    {
        base.AttackAction();

        animator.SetTrigger("attack");
    }
}
