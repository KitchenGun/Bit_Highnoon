using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIHard : AIParent
{
    protected override void Start()
    {
        base.Start();

        lifeCount = 2;

        idleTime = 3;   //대기 시간

        StartCoroutine(CheckState());               //상태를 체크
        StartCoroutine(CheckStateForAction());      //상태의 따른
    }

    #region 상태의 따른 행동
    protected override void AttackAction()
    {
        base.AttackAction();

        if (animator.GetCurrentAnimatorStateInfo(0).IsName("Shoot"))
        {
            Turn();
        }
        else
            ReTurn();
    }

    protected override void PlayerDeadAction()
    {
        base.PlayerDeadAction();

        if (animator.GetCurrentAnimatorStateInfo(0).IsName("Laugh")
            && isPlayerDeadAudio == false)
        {
            ReTurn();
            PlayerDeadAudio();
        }
    }
    #endregion

    #region 플레이어가 호출
    protected override void Dead()
    {
        ReTurn();

        base.Dead();
        GameEnd("player");
    }
    #endregion

    #region 방향 맞추기..
    private void Turn()
    {
        Quaternion Right = Quaternion.identity;
        Right.eulerAngles = new Vector3(0, -90, 0);
        gameObject.transform.rotation = Quaternion.Slerp(gameObject.transform.rotation, Right, Time.deltaTime * 2);
    }

    private void ReTurn()
    {
        gameObject.transform.rotation = Quaternion.Euler(0, 180, 0);
    }
    #endregion

    #region 혈흔 효과
    protected override void CreateBloodEffect()
    {
        base.CreateBloodEffect();

        bloodEffect.transform.position
            = gameObject.transform.position + new Vector3(-0.05f, 0.001f, 1.2f);
    }
    #endregion
}
