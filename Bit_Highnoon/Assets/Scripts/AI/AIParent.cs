﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIParent : MonoBehaviour
{
    public enum AIState { IDLE, WALK, ATTACK, DEAD, PLAYERDEAD }   

    protected AIState aistate;      //현재 적의 상태

    protected bool isdead;          //AI의 사망여부

    protected bool isplayerdead;    //player의 사망여부

    protected float idletime;          //대기시간

    protected float deadtime;         //player 사망시간

    protected Animator animator;    //Animator

    GameObject player;              //플레이어

    // Start is called before the first frame update
    protected virtual void Start()
    {
        animator = GetComponent<Animator>();

        aistate = AIState.WALK;          //처음에 걷기

        isdead = false;

        isplayerdead = false;

        Debug.Log("idle");
    }

    protected IEnumerator CheckState()
    {
        
        while (!isdead)
        {
            //yield return new WaitForSeconds(2 * Time.deltaTime);

            idletime -= Time.deltaTime;

            if (idletime > -6 && idletime < 1)           
            {
                aistate = AIState.IDLE;
                Debug.Log("idle");
            }
            else if (idletime > 1)     
            {
                aistate = AIState.WALK;
                Debug.Log("walk");
            }
            else if (isplayerdead == true)          //player가 죽음
            {
                aistate = AIState.PLAYERDEAD;
                Debug.Log("playerdead");
            }
            else if (idletime < -6)      //시작한 후 일정시간이 지나면 AI 공격
            {
                aistate = AIState.ATTACK;
                Debug.Log("attack");
            }

            yield return null;
        }
    }

    protected IEnumerator CheckStateForAction()
    {
        while(!isdead)
        {
            switch(aistate)
            {
                case AIState.IDLE:
                    IdleAction();
                    break;
                case AIState.WALK:
                    WalkAction();
                    break;
                case AIState.ATTACK:
                    AttackAction();
                    break;
                case AIState.PLAYERDEAD:
                    PlayerDeadAction();
                    break;
            }

            yield return null;
        }
    }

    #region 자식에서 필요의 의해 재정의(상태의 따른 행동)
    protected virtual void IdleAction()
    {
        Debug.Log("IdleAction");

        TurnAI();
    }

    protected virtual void WalkAction()
    {
        Debug.Log("WalkAction");

        gameObject.transform.position += new Vector3(0, 0, 0.5f * Time.deltaTime);
    }

    protected virtual void AttackAction()
    {
        Debug.Log("AttackAction");

        deadtime -= Time.deltaTime;

        if (deadtime < 0)       //시작후 일정 시간이 지나면 플레이어 사망
            isplayerdead = true;
    }

    protected virtual void PlayerDeadAction()
    {
        Debug.Log("PlayerDeadAction");

        //player.SendMessage("Dead");   //플레이어에게 죽어다고 알리기

        gameObject.SetActive(false);
    }
    #endregion

    //뒤로 도는 함수
    private void TurnAI()
    {
        Quaternion Right = Quaternion.identity;
        Right.eulerAngles = new Vector3(0, 180, 0);
        gameObject.transform.rotation = Quaternion.Slerp(transform.rotation, Right, Time.deltaTime * 2);
        //gameObject.transform.rotation = Right;
    }
}
