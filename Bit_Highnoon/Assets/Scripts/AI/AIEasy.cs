using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIEasy : AIParent
{
    GameObject shoot;

    bool chagecheck;    //Shoot상태에서 원래대로 돌아왔는지 판단

    protected override void Start()
    {
        base.Start();

        wakltime = 5;    //걷는 시간

        idletime = 10;   //대기시간
            
        deadtime = 20;  //플레이어가 죽는 시간

        chagecheck = false;

        shoot = GameObject.Find("EasyShoot").transform.FindChildRecursive("Shoot").gameObject;

        StartCoroutine(CheckState());               //상태를 체크
        StartCoroutine(CheckStateForAction());      //상태의 따른 행동
        
    }

    protected override void IdleAction()
    {
        base.IdleAction();

        animator.SetTrigger("idle");
    }
        
    protected override void AttackAction()
    {
        base.AttackAction();

        if (shoot.activeSelf == false)
            ChageShoot();
    }

    protected override void HitAction()
    {
        base.HitAction();

        if (shoot.activeSelf == true)
            ReChange();

        animator.SetTrigger("hit");

        if (chagecheck == false)
            HitFinsh();
        else
            Invoke("HitFinsh", 40 * Time.deltaTime);
    }

    private void HitFinsh()
    {
        ishit = false;

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

        chagecheck = true;
    }
    #endregion

    #region 플레이어가 호출?
    //맞았을때
    private void Hit()
    {
        ishit = true;
    }

    //죽었을때
    private void Dead()
    {
        if (shoot.activeSelf == true)
            ReChange();

        animator.SetTrigger("dead");
        isdead = true;

        player.SendMessage("Win");
    }
    #endregion
}
