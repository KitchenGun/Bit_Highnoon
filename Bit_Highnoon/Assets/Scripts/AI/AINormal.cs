using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AINormal : AIParent
{
    protected override void Start()
    {
        base.Start();

        walktime = 2;   //걷는 시간

        idletime = 5;   //걷기 후 대기 시간

        deadtime = 15;  //플레이어가 죽는 시간
    }

    protected override void AttackAction()
    {
        base.AttackAction();

        if (animator.GetCurrentAnimatorStateInfo(0).IsName("Shoot"))
            Turn();
        else
            ReTurn();

        animator.SetTrigger("attack");
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
