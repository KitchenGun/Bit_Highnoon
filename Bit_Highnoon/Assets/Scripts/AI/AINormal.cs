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

        player_Dead_Count = 4;

        StartCoroutine(CheckState());               //상태를 체크
        StartCoroutine(CheckStateForAction());      //상태의 따른
    }

    #region 상태의 따른 행동
    protected override void AttackAction()
    {
        base.AttackAction();

        if (animator.GetCurrentAnimatorStateInfo(0).IsName("Turn"))
        {
            TurnS();
        }
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

        if (animator.GetCurrentAnimatorStateInfo(0).IsName("Kiss")
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
        base.GameEnd("player");
    }
    #endregion

    #region Animation 이벤트 호출
    protected override void PlayerDead()
    {
        if (player_Dead_Count == 0)
        {
            base.PlayerDead();         
        }
    }

    protected override void GameEnd(string winner)
    {
        player_Dead_Count--;

        if (player_Dead_Count == 0)
        {
            base.GameEnd(winner);

            SendMessageDead();
        }
    }
    #endregion

    #region 방향 맞추기..
    private void TurnS()
    {
        Quaternion Right = Quaternion.identity;
        Right.eulerAngles = new Vector3(0, -90, 0);
        gameObject.transform.rotation = Quaternion.Slerp(gameObject.transform.rotation, Right, Time.deltaTime * 2);
    }

    private void Turn()
    {
        gameObject.transform.rotation = Quaternion.Euler(0, 90, 0);
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
            = gameObject.transform.position + new Vector3(0.15f, 0.001f, -1.08f);
    }
    #endregion

    #region 콜라이더
    protected override void UpdateCollider()
    {
        base.UpdateCollider();

        collider.center = new Vector3(0, 0, 1.1f);
    }
    #endregion
}
