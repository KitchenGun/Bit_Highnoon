using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIParent : MonoBehaviour
{
    public enum AIState { IDLE, WALK, ATTACK, HIT, DEAD, PLAYERDEAD }   

    protected AIState aiState;          //현재 적의 상태

    protected bool isDead;              //AI의 사망여부

    protected bool isHit;               //AI가 공격을 맞았는지 여부

    protected bool isPlayerDead;        //player의 사망여부

    protected bool isTurn;              //뒤로 돌았는지 여부 판단

    protected float idleTime;           //대기시간

    protected float walkTime;           //걷는 시간
      
    protected float deadTime;           //player 사망시간

    protected Animator animator;        //Animator

    protected GameObject player;        //플레이어

    #region audio
    protected AudioSource AIAudio;
    [SerializeField]
    private AudioClip dead_SFX;
    [SerializeField]
    private AudioClip hit_SFX;
    [SerializeField]
    private AudioClip player_Dead_SFX;
    #endregion

    // Start is called before the first frame update
    protected virtual void Start()
    {
        animator = GetComponent<Animator>();    

        aiState = AIState.IDLE;          

        isDead = isPlayerDead = isHit = isTurn = false;  //초기 설정

        player = GameObject.Find("Cube");       //플레이어 찾기

        AIAudio = gameObject.transform.GetComponent<AudioSource>();
        AIAudio.loop = false;

        Debug.Log("idle");
    }

    //대기 시간 무시하고 강제로 시작
    private void GameStart()
    {
        animator.SetTrigger("walk");

        idleTime = -1;        
    } 

    //상태를 체크하고 바꿔준다
    protected IEnumerator CheckState()
    {
        while (!isDead)
        {
            //yield return new WaitForSeconds(2 * Time.deltaTime);

            if (isHit == true)                      //player의 공격을 맞았을때
            {
                aiState = AIState.HIT;
                Debug.Log("hit");
            }
            else if (isPlayerDead == true)          //player가 죽음
            {
                aiState = AIState.PLAYERDEAD;
                Debug.Log("playerdead");
            }
            else if (idleTime > 0)                  //대기 상태
            {
                aiState = AIState.IDLE;
                Debug.Log("idle");
            }
            else if (walkTime > 0 && idleTime < 0)  //걷는 상태
            {
                aiState = AIState.WALK;
                Debug.Log("walk");
            }            
            else if (walkTime < 0)                  //걷기후 공격 상태
            {
                aiState = AIState.ATTACK;
                Debug.Log("attack");
            }

            yield return null;            
        }
    }

    //상태의 따른 행동
    protected IEnumerator CheckStateForAction()
    {
        while(!isDead)
        {
            switch (aiState)
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
                case AIState.HIT:
                    HitAction();
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

        idleTime -= Time.deltaTime;
    }

    protected virtual void WalkAction()
    {
        Debug.Log("WalkAction");

        animator.SetTrigger("walk");

        if (!animator.GetCurrentAnimatorStateInfo(0).IsName("Idle"))
        {  
            walkTime -= Time.deltaTime;

            gameObject.transform.position += new Vector3(0, 0, 0.5f * Time.deltaTime);
        }
    }
    
    protected virtual void AttackAction()
    {
        Debug.Log("AttackAction");

        animator.SetBool("walk", false);
        animator.SetTrigger("attack");

        if (isTurn == false)
            TurnAI();

        deadTime -= Time.deltaTime;

        if (deadTime < 0)       //시작후 일정 시간이 지나면 플레이어 사망
            isPlayerDead = true;
    }

    protected virtual void HitAction()
    {
        Debug.Log("HitAction");

        HitAudio();

        animator.SetTrigger("hit");

        isHit = false;
    }

    protected virtual void PlayerDeadAction()
    {
        Debug.Log("PlayerDeadAction");

        player.SendMessage("Dead");   //플레이어에게 죽어다고 알리기
             
        animator.SetTrigger("playerdead");

        PlayerDeadAudio();

        isDead = true;
    }
    #endregion

    #region 플레이어가 호출
    //맞았을때
    private void Hit()
    {
        isHit = true;
    }

    //죽었을때
    protected virtual void Dead()
    {
        animator.SetTrigger("dead");
        isDead = true;

        player.SendMessage("Win");

        DeadAudio();
    }
    #endregion

    #region audio함수
    private void DeadAudio()
    {
        AIAudio.clip = dead_SFX;
        AIAudio.Play();
    }

    private void HitAudio()
    {
        AIAudio.clip = hit_SFX;
        AIAudio.Play();
    }

    private void PlayerDeadAudio()
    {
        AIAudio.clip = player_Dead_SFX;
        AIAudio.Play();
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
