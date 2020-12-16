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

        idleTime = 10;   //대기시간

        walkTime = 5;    //걷는 시간        
            
        deadTime = 20;  //플레이어가 죽는 시간

        shoot = GameObject.Find("EasyShoot").transform.FindChildRecursive("Shoot").gameObject;

        StartCoroutine(CheckState());               //상태를 체크
        StartCoroutine(CheckStateForAction());      //상태의 따른
    }
    
    protected override void AttackAction()
    {
        base.AttackAction();

        Quaternion Right = Quaternion.identity;
        Right.eulerAngles = new Vector3(0, -175, 0);

        if (shoot.activeSelf == false)
        {
            if ((isFirst == true && gameObject.transform.rotation == Right) ||
                animator.GetCurrentAnimatorStateInfo(0).IsName("Hit") &&
                animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.99f)
            {
                ChageShoot();
                isFirst = false;
            }
        }
    }

    protected override void HitAction()
    {
        base.HitAction();

        ReChange();
    }

    protected override void PlayerDeadAction()
    {
        ReChange();

        base.PlayerDeadAction();
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
