using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIParent : MonoBehaviour
{
    public enum AIState { IDLE, WALK, ATTACK, HIT, DEAD, PLAYERDEAD }   

    protected AIState aiState;          //현재 적의 상태

    #region 상태 여부 관련 변수
    protected bool isDead;              //AI의 사망여부

    protected bool isHit;               //AI가 공격을 맞았는지 여부

    protected bool isPlayerDead;        //player의 사망여부

    protected bool isPlayerDeadAudio;   //PlayerDead오디오가 실행됬는지 판단

    private bool isGameStartAudio;      //게임 시작 음악이 재생되었는지 판단

    private bool isIdleAudio;           //대기상태의 대사를 했는지 판단
    #endregion

    #region 게임타임 관련 변수
    protected int lifeCount;             //0이 되면 사망

    protected float idleTime;           //대기시간

    protected float walkTime;           //걷는 시간
    #endregion

    protected Animator animator;        //Animator

    protected GameObject player;        //플레이어

    private GameObject gameManager;

    #region audio
    protected AudioSource AIAudio;
    [SerializeField]
    private AudioClip idle_SFX;
    [SerializeField]
    private AudioClip dead_SFX;
    [SerializeField]
    private AudioClip hit_SFX;
    [SerializeField]
    protected AudioClip attack_SFX;
    [SerializeField]
    private AudioClip player_Dead_SFX;
    
    #endregion

    // Start is called before the first frame update
    protected virtual void Start()
    {
        animator = GetComponent<Animator>();        

        aiState = AIState.WALK;

        walkTime = 3;

        //초기 설정
        isDead = isPlayerDead = isHit = isPlayerDeadAudio = isGameStartAudio = isIdleAudio = false;  

        player = GameObject.Find("PlayerCtrl");       //플레이어 찾기

        gameManager = GameObject.Find("GameManager");

        AIAudio = GetComponent<AudioSource>();
        AIAudio.loop = false;

        //Debug.Log("idle");
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
                //Debug.Log("hit");
            }
            else if (isPlayerDead == true)          //player가 죽음
            {
                aiState = AIState.PLAYERDEAD;
                //Debug.Log("playerdead");
            }
            else if (idleTime > 0 && walkTime < 0)   //대기 상태
            {
                aiState = AIState.IDLE;
                //Debug.Log("idle");
            }
            else if (walkTime > 0)                  //걷는 상태
            {
                aiState = AIState.WALK;
                //Debug.Log("walk");
            }            
            else if (idleTime < 0)                  //걷기후 공격 상태
            {
                aiState = AIState.ATTACK;
                //Debug.Log("attack");
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
        //Debug.Log("IdleAction");

        //TurnAI();

        idleTime -= Time.deltaTime;

        animator.SetTrigger("idle");

        if (isIdleAudio == false)
        {
            IdleAudio();
            isIdleAudio = true;
        }

        if (isGameStartAudio == false && AIAudio.isPlaying == false && isIdleAudio == true)
        {
            gameManager.SendMessage("GameStart");
            idleTime = -1;
            isGameStartAudio = true;
        }
    }

    protected virtual void WalkAction()
    {
        //Debug.Log("WalkAction");
        walkTime -= Time.deltaTime;

        gameObject.transform.position += new Vector3(0, 0, -0.5f * Time.deltaTime);
    }

    
    protected virtual void AttackAction()
    {
        //Debug.Log("AttackAction");

        animator.SetTrigger("attack");
    }

    protected virtual void HitAction()
    {
        //Debug.Log("HitAction");

        HitAudio();

        animator.SetTrigger("hit");

        isHit = false;    
    }

    protected virtual void PlayerDeadAction()
    {
        //Debug.Log("PlayerDeadAction");
             
        animator.SetTrigger("playerdead");
    }
    #endregion

    #region 플레이어가 호출
    //맞았을때
    private void Hit()
    {
        isHit = true;

        lifeCount--;

        if (lifeCount == 0)
            Dead();
    }

    //죽었을때
    protected virtual void Dead()
    {
        animator.SetTrigger("dead");
        isDead = true;

        //player.SendMessage("Win");            //플레이어에게 적이 죽었다고 알리기

        DeadAudio();
        GameEnd();
    }
    #endregion

    #region audio함수
    private void IdleAudio()
    {
        AIAudio.clip = idle_SFX;
        AIAudio.Play();
    }

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

    private void AttackAudio()
    {
        
        AIAudio.clip = attack_SFX;
        AIAudio.Play();
    }

    protected void PlayerDeadAudio()
    {
        AIAudio.clip = player_Dead_SFX;
        AIAudio.Play();

        isPlayerDeadAudio = true;
    }
    #endregion

    protected virtual void PlayerDead()
    {
        isPlayerDead = true;

        //player.transform.Find("Body").SendMessage("Dead");      //플레이어에게 죽어다고 알리기
        //player.SendMessage("Dead");      //플레이어에게 죽어다고 알리기
    }

    protected virtual void GameEnd()
    {
        gameManager.SendMessage("GameEnd");   //게임 종료 음악
    }
}
