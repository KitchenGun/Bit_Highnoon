using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AINormal : AIParent
{
    protected override void Start()
    {
        base.Start();

        idletime = 5;   //대기 시간

        walktime = 3;   //걷는 시간

        deadtime = 15;  //플레이어가 죽는 시간

        StartCoroutine(CheckState());               //상태를 체크
        StartCoroutine(CheckStateForAction());      //상태의 따른
    }

    protected override void AttackAction()
    {
        base.AttackAction();

        //if (animator.GetCurrentAnimatorStateInfo(0).IsName("Shoot"))
        //    Turn();
        //else
        //    ReTurn();

        animator.SetTrigger("attack");
    }

    protected override void PlayerDeadAction()
    {
        base.PlayerDeadAction();

        //ReTurn();
    }

    #region 방향 맞추기..
    private void Turn()
    {
        Quaternion Right = Quaternion.identity;
        Right.eulerAngles = new Vector3(0, 90, 0);
        gameObject.transform.rotation = Right;
    }

    private void ReTurn()
    {
        Quaternion Right = Quaternion.identity;
        Right.eulerAngles = new Vector3(0, 180, 0);
        gameObject.transform.rotation = Right;
    }
    #endregion
}
