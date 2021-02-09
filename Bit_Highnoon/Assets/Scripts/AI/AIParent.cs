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

    private bool isIdleAudio;           //대기상태의 대사를 했는지 판단

    private bool isGameStart;           //게임이 시작 했는지 판단
    #endregion

    #region 게임타임 관련 변수
    protected int lifeCount;             //0이 되면 사망

    protected float idleTime;           //대기시간

    protected float walkTime;           //걷는 시간
    #endregion

    protected Animator animator;                //Animator

    protected GameObject player;                //플레이어

    protected AudioSource AIAudio;              //오디오 소스  

    protected GameObject bloodEffect;           //바닥에 출혈 효과
        
    protected new CapsuleCollider collider;     //콜라이더

    private GameObject canvas;                  //게임 안내 캠버스

    #region 게임 승패 출력 오디오 관련 변수
    private AudioSource winloseAudio;
    #endregion

    // Start is called before the first frame update
    protected virtual void Start()
    {
        animator = GetComponent<Animator>();

        bloodEffect = gameObject.transform.Find("BloodDecal").gameObject;

        aiState = AIState.WALK;

        walkTime = 5;

        //초기 설정
        isDead = isPlayerDead = isHit = isPlayerDeadAudio = isIdleAudio = isGameStart = false;  

        player = GameObject.Find("PlayerCtrl");       //플레이어 찾기

        AIAudio = GetComponent<AudioSource>();
        AIAudio.loop = false;

        collider = GetComponent<CapsuleCollider>();

        canvas = GameObject.Find("GameUI");
        winloseAudio = canvas.GetComponent<AudioSource>();
        //Debug.Log("idle");
    }

    #region 상태 체크 및 행동
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
    #endregion

    #region 자식에서 필요의 의해 재정의(상태의 따른 행동)
    protected virtual void IdleAction()
    {
        //Debug.Log("IdleAction");

        //TurnAI();

        idleTime -= Time.deltaTime;

        animator.SetTrigger("idle");
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

        Invoke("UpdateCollider", 1);

        DeadAudio();
        Invoke("CreateBloodEffect", 2.5f);
    }
    #endregion

    #region audio함수
    private void IdleAudio()
    {
        if (isIdleAudio == false)
        {
            AIAudio.clip = GameManager.Instance.LoadAudioClip("Start");
            AIAudio.Play();
            isIdleAudio = true;

            canvas.transform.GetChild(0).gameObject.SetActive(false);
        }
    }

    private void DeadAudio()
    {
        AIAudio.clip = GameManager.Instance.LoadAudioClip("Dead");
        AIAudio.Play();
    }

    private void HitAudio()
    {
        AIAudio.clip = GameManager.Instance.LoadAudioClip("Hit");
        AIAudio.Play();
    }

    private void AttackAudio()
    {
        AIAudio.clip = GameManager.Instance.LoadAudioClip("enemyfire");
        AIAudio.Play();
    }

    protected void PlayerDeadAudio()
    {
        AIAudio.clip = GameManager.Instance.LoadAudioClip("PlayerDead");
        AIAudio.Play();

        isPlayerDeadAudio = true;
    }

    private void WalkAudio()
    {
        AIAudio.clip = GameManager.Instance.LoadAudioClip("walk");
        AIAudio.Play();
    }
    #endregion

    #region Animation 이벤트 호출
    private void GameStart()
    {
        if (isGameStart == false)
        {
            GameManager.Instance.GameStart();
            isGameStart = true;
        }
    }

    protected void SendMessageDead()
    {
        //플레이어에게 죽어다고 알리기
        player.transform.Find("Body").GetComponent<PlayerHit>().SendMessage("Die");

        //테스트
        //GameObject.Find("Canvas").transform.GetChild(2).gameObject.SetActive(false);
    }

    protected virtual void GameEnd(string winner)
    {
        StartCoroutine(GameManager.Instance.GameEnd(winner));
    }

    protected virtual void PlayerDead()
    {
        isPlayerDead = true;

        canvas.transform.GetChild(2).gameObject.SetActive(true);
        winloseAudio.clip = GameManager.Instance.LoadAudioClip("popup");
        winloseAudio.Play();
        winloseAudio.clip = GameManager.Instance.LoadAudioClip("lose");
        winloseAudio.Play();
    }
    #endregion

    #region 혈흔 효과
    protected virtual void CreateBloodEffect()
    {
        bloodEffect.SetActive(true);

        canvas.transform.GetChild(1).gameObject.SetActive(true);
        winloseAudio.clip = GameManager.Instance.LoadAudioClip("popup");
        winloseAudio.Play();
        winloseAudio.clip = GameManager.Instance.LoadAudioClip("win");
        winloseAudio.Play();
    }
    #endregion

    #region 콜라이더
    protected virtual void UpdateCollider()
    {
        collider.direction = 2;
        collider.center = new Vector3(0.1f, 0f, -0.9f);
    }
    #endregion
}