using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIEasy : AIParent
{
    GameObject shoot;

    private bool isFirst = true;    //처음 공격인지 확인한다.

    protected override void Start()
    {
        base.Start();

        lifeCount = 1;

        idleTime = 4;   //대기시간                

        shoot = GameObject.Find("EasyShoot").transform.FindChildRecursive("Shoot").gameObject;

        StartCoroutine(CheckState());               //상태를 체크
        StartCoroutine(CheckStateForAction());      //상태의 따른
    }

    #region 상태의 따른 행동
    protected override void AttackAction()
    {
        base.AttackAction();

        if (shoot.activeSelf == false)
        {
            if ((isFirst == true) ||
                animator.GetCurrentAnimatorStateInfo(0).IsName("Hit") &&
                animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.99f)
            {
                ChageShoot();
                isFirst = false;                
            }
        }
    }

    protected override void PlayerDeadAction()
    {
        ReChange();

        base.PlayerDeadAction();

        if (animator.GetCurrentAnimatorStateInfo(0).IsName("Spread") 
            && isPlayerDeadAudio == false)
            PlayerDeadAudio();
    }
    #endregion

    #region 플레이어가 호출
    //죽었을때
    protected override void Dead()
    {
        ReChange();

        base.Dead();
        GameEnd();
    }
    #endregion

    #region 오브젝트 변경
    //Shoot으로 변경
    private void ChageShoot()
    {
        shoot.SetActive(true);
        shoot.transform.position = gameObject.transform.position;
        shoot.transform.rotation = gameObject.transform.rotation;

        //shoot.transform.LookAt(player.transform);

        for (int i = 0; i < 4; i++)
        {
            gameObject.transform.GetChild(i).gameObject.SetActive(false);
        }
    }

    //다시 원래대로 
    private void ReChange()
    {
        if (shoot.activeSelf == true)
        {
            shoot.SetActive(false);

            for (int i = 0; i < 4; i++)
            {
                gameObject.transform.GetChild(i).gameObject.SetActive(true);
            }
        }
    }
    #endregion
}
