using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AINormal : AIParent
{
    private int player_Dead_Count;

    protected override void Start()
    {
        base.Start();

        lifeCount = 2;

        idleTime = 2;   //대기 시간

        player_Dead_Count = 5;

        StartCoroutine(CheckState());               //상태를 체크
        StartCoroutine(CheckStateForAction());      //상태의 따른
    }

    protected override void AttackAction()
    {
        base.AttackAction();

        if (animator.GetCurrentAnimatorStateInfo(0).IsName("Turn"))
        {
            Turn();
        }
        else if(animator.GetCurrentAnimatorStateInfo(0).IsName("Shoot"))
        {
            Turn1();
        }
        else
            ReTurn();
    }

    protected override void PlayerDeadAction()
    {
        base.PlayerDeadAction();

        if (animator.GetCurrentAnimatorStateInfo(0).IsName("Kiss")
            && isPlayerDeadAudio == false)
        {
            ReTurn();
            PlayerDeadAudio();
        }
    }

    protected override void Dead()
    {
        ReTurn();

        base.Dead();
    }

    #region 방향 맞추기..
    private void Turn()
    {
        Quaternion Right = Quaternion.identity;
        Right.eulerAngles = new Vector3(0, -90, 0);
        gameObject.transform.rotation = Quaternion.Slerp(gameObject.transform.rotation, Right, Time.deltaTime * 2);
    }

    private void Turn1()
    {
        gameObject.transform.rotation = Quaternion.Euler(0, -90, 0);
    }

    private void ReTurn()
    {
        gameObject.transform.rotation = Quaternion.Euler(0, 0, 0);
    }
    #endregion

    
    protected override void PlayerDead()
    {
        player_Dead_Count--;

        if (player_Dead_Count == 0)
        {
            isPlayerDead = true;
            Debug.Log("normal");
        }
    }
}
