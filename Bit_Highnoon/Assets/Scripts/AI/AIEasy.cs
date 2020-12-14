using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIEasy : AIParent
{
    GameObject shoot;

    protected override void Start()
    {
        base.Start();

        walktime = 5;    //걷는 시간

        idletime = 10;   //걷기 후 대기시간
            
        deadtime = 20;  //플레이어가 죽는 시간

        shoot = GameObject.Find("EasyShoot").transform.FindChildRecursive("Shoot").gameObject;        
    }

    protected override void AttackAction()
    {
        base.AttackAction();

        animator.SetBool("idle", false);
        animator.SetTrigger("start");

        if (shoot.activeSelf == false)
        {
            if (animator.GetCurrentAnimatorStateInfo(0).IsName("Idle") ||
                animator.GetCurrentAnimatorStateInfo(0).IsName("Hit") &&
                animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.99f)
                ChageShoot();
        }

    }

    protected override void HitAction()
    {
        base.HitAction();

        if (shoot.activeSelf == true)
            ReChange();
    }

    protected override void PlayerDeadAction()
    {
        base.PlayerDeadAction();

        shoot.SetActive(false);
    }

    #region 오브젝트 변경
    //Shoot으로 변경
    private void ChageShoot()
    {
        shoot.SetActive(true);
        shoot.transform.position = gameObject.transform.position;
        shoot.transform.rotation = gameObject.transform.rotation;

        for (int i = 0; i < 4; i++)
        {
            gameObject.transform.GetChild(i).gameObject.SetActive(false);
        }
    }

    //다시 원래대로 
    private void ReChange()
    {
        shoot.SetActive(false);

        for (int i = 0; i < 4; i++)
        {
            gameObject.transform.GetChild(i).gameObject.SetActive(true);
        }
    }
    #endregion

    #region 플레이어가 호출
    //죽었을때
    protected override void Dead()
    {
        if (shoot.activeSelf == true)
            ReChange();

        base.Dead();
    }
    #endregion
}
