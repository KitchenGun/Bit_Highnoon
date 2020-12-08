using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIParent : MonoBehaviour
{
    public enum AIState { idle, attack, dead, playerdead }   

    protected AIState aistate;      //현재 적의 상태

    protected bool isdead;          //AI의 사망여부

    protected bool isplayerdead;    //player의 사망여부

    protected int idletime;          //대기시간

    protected int deadtime;         //player 사망시간

    GameObject player;              //플레이어

    // Start is called before the first frame update
    protected virtual void Start()
    {
        aistate = AIState.idle;          //처음에 대기 상태

        isdead = false;

        isplayerdead = false;

        Debug.Log("idle");

        StartCoroutine(CheckState());               //상태를 체크
        StartCoroutine(CheckStateForAction());      //상태의 따른 행동
    }

    IEnumerator CheckState()
    {
        while(!isdead)
        {
            if (Time.time < idletime)                      //시작후 일정 시간동안 대기
            {
                aistate = AIState.idle;
                Debug.Log("idle");
            }
            else if (isplayerdead == true)          //player가 죽음
            {
                aistate = AIState.playerdead;
                Debug.Log("playerdead");
            }
            else    //시작한 후 일정시간이 지나면 AI 공격
            {
                aistate = AIState.attack;
                Debug.Log("attack");
            }

            yield return null;
        }
    }

    IEnumerator CheckStateForAction()
    {
        while(!isdead)
        {
            switch(aistate)
            {
                case AIState.idle:
                    IdleAction();
                    break;
                case AIState.attack:
                    AttackAction(deadtime);
                    break;
                case AIState.playerdead:
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

        gameObject.transform.position += new Vector3(0, 0, 0.01f);
    }

    protected virtual void AttackAction(int deadtime)
    {
        Debug.Log("AttackAction");

        TurnAI();

        if (Time.time > deadtime)       //시작후 일정 시간이 지나면 플레이어 사망
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
    }
}
