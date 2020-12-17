using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIHard : AIParent
{
    protected override void Start()
    {
        base.Start();

        idleTime = 3;   //대기 시간

        //walkTime = 1;   //걷는 시간

        deadTime = 5;  //플레이어가 죽는 시간

        StartCoroutine(CheckState());               //상태를 체크
        StartCoroutine(CheckStateForAction());      //상태의 따른
    }

    protected override void AttackAction()
    {
        base.AttackAction();

        if (animator.GetCurrentAnimatorStateInfo(0).IsName("Shoot"))
        {
            Turn();
            if (isAttackAudio == false)
            {
                Invoke("AttackAudio", 2);
                isAttackAudio = true;
            }
        }
        else
            ReTurn();
    }

    protected override void PlayerDeadAction()
    {
        base.PlayerDeadAction();

        ReTurn();
    }

    #region 방향 맞추기..
    private void Turn()
    {
        gameObject.transform.rotation = Quaternion.Euler(0, -90, 0);
    }

    private void ReTurn()
    {
        gameObject.transform.rotation = Quaternion.Euler(0, 180, 0);
    }
    #endregion

    protected override void AttackAudio()
    {
        base.AttackAudio();

        AIAudio.loop = false;
    }
}
