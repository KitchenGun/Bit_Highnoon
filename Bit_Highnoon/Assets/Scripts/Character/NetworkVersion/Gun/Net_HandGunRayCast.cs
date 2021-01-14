using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using OVRTouchSample;
using UnityEngine.UI;
using Photon.Pun;

public class Net_HandGunRayCast : MonoBehaviourPunCallbacks
{
    private PhotonView PV;

    #region Ray
    private GameObject FirePos; // Ray가  시작할 위치 정보를 가진 오브젝트
    private RaycastHit HitObj;
    #endregion
    #region 사격관련 변수
    private bool FireState;
    private int Bullet; 
    private bool ReloadState;
    private float ReloadStick;
    //총알 무한을 위한 씬넘버 획득용
    private GameObject GM;
    private int SceneIdx;
    #endregion
    #region audio
    private AudioSource HandGunFireClickAudio;
    private AudioSource HandGunReloadAudio;
    private AudioSource HandGunFireAudio;
    #endregion
    #region Animation
    private Animator GunAni;
    private ParticleSystem GunFireEffect;
    #endregion
    #region UI
    [SerializeField]
    private Sprite[] BulletUI;
    [SerializeField]
    private Image BulletUIImage;
    #endregion
    #region 이펙트
    [SerializeField]
    private GameObject SandDecal;
    [SerializeField]
    private GameObject MetalDecal;
    [SerializeField]
    private GameObject WoodDecal;
    [SerializeField]
    private GameObject BloodDecal;
    #endregion

    void Start()
    {//초기화
        PV = this.gameObject.transform.parent.parent.gameObject.GetComponent<PhotonView>();
        #region Scene
        GM = GameObject.Find("GameManager");
        if (GM == null)
        {
            SceneIdx = 0;
        }//gamemanager가 존재 안할경우
        else
        {
            SceneIdx = GM.GetComponent<GameManager>().GetSceneIndex();
        }
        #endregion
        #region Ray
        FirePos = this.gameObject.transform.parent.Find("GunFirePos").gameObject;
        #endregion
        #region Audio
        this.HandGunFireClickAudio = this.gameObject.transform.parent.GetComponent<AudioSource>(); //격발음 SFX
        this.HandGunFireClickAudio.loop = false;
        this.HandGunReloadAudio = this.gameObject.GetComponent<AudioSource>();//노리쇠 후퇴 SFX
        this.HandGunReloadAudio.loop = false;
        this.HandGunFireAudio = FirePos.GetComponent<AudioSource>();
        this.HandGunFireAudio = FirePos.GetComponent<AudioSource>();
        #endregion
        #region Animation
        GunAni = this.gameObject.GetComponent<Animator>();
        GunAni.SetBool("FireState", FireState);
        GunFireEffect = FirePos.transform.GetChild(0).gameObject.GetComponent<ParticleSystem>();
        #endregion
    }

    private void Update()
    {
        if (PV.IsMine)
        {
            #region UI
            BulletUIImage.sprite = BulletUI[Bullet];
            #endregion

            #region 사격
            GunAni.SetBool("FireState", FireState);
            switch (FirePos.tag)//발사 위치 오브젝트의 태그를 통해서 판별
            {
                case "Left"://왼쪽
                    if (OVRInput.GetDown(OVRInput.Button.PrimaryIndexTrigger, OVRInput.Controller.Touch))
                    {
                        if (Fire())//총알 발사
                        {
                            if (Physics.Raycast(FirePos.transform.position, FirePos.transform.forward, out HitObj, 2000))
                            {
                                //오브젝트 태그로 식별
                                if (HitObj.transform.gameObject.tag == "Bottle")
                                {
                                    BottleHit(HitObj.transform.gameObject);
                                }
                                else if (HitObj.transform.gameObject.tag == "Enemy")
                                {
                                    EnemyHit(HitObj.transform.gameObject);
                                }
                                else if (HitObj.transform.gameObject.tag == "Button")
                                {
                                    ButtonHit(HitObj.transform.gameObject);
                                }
                                //오브젝트 레이어로 식별
                                if (HitObj.transform.gameObject.layer == 8)
                                {
                                    GameObject BulletHole = Instantiate<GameObject>(SandDecal, HitObj.point, Quaternion.identity) as GameObject;
                                    BulletHole.transform.LookAt(this.gameObject.transform.position);
                                    BulletHole.GetComponent<AudioSource>().clip = GM.GetComponent<GameManager>().LoadAudioClip("etc");
                                    BulletHole.GetComponent<AudioSource>().Play();
                                    Destroy(BulletHole, 3f);
                                }
                                else if (HitObj.transform.gameObject.layer == 9)
                                {
                                    GameObject BulletHole = Instantiate<GameObject>(MetalDecal, HitObj.point, Quaternion.identity) as GameObject;
                                    BulletHole.transform.LookAt(this.gameObject.transform.position);
                                    BulletHole.GetComponent<AudioSource>().clip = GM.GetComponent<GameManager>().LoadAudioClip("metal");
                                    BulletHole.GetComponent<AudioSource>().Play();
                                    Destroy(BulletHole, 3f);

                                }
                                else if (HitObj.transform.gameObject.layer == 10)
                                {
                                    GameObject BulletHole = Instantiate<GameObject>(WoodDecal, HitObj.point, Quaternion.identity) as GameObject;
                                    BulletHole.transform.LookAt(this.gameObject.transform.position);
                                    BulletHole.GetComponent<AudioSource>().clip = GM.GetComponent<GameManager>().LoadAudioClip("wood");
                                    BulletHole.GetComponent<AudioSource>().Play();
                                    Destroy(BulletHole, 3f);
                                }
                                else if (HitObj.transform.gameObject.layer == 20)
                                {
                                    GameObject BloodParticle = Instantiate<GameObject>(BloodDecal, HitObj.point, Quaternion.identity) as GameObject;
                                    Destroy(BloodParticle, 3f);
                                }

                            }
                        }
                    }
                    break;
                case "Right": //오른쪽
                    if (OVRInput.GetDown(OVRInput.Button.SecondaryIndexTrigger, OVRInput.Controller.Touch))
                    {
                        if (Fire())//총알 발사
                        {
                            Debug.DrawRay(FirePos.transform.position, FirePos.transform.forward * 2000, Color.red, 0.3f);//개발 확인용 레이 
                            if (Physics.Raycast(FirePos.transform.position, FirePos.transform.forward, out HitObj, 2000))
                            {
                                //오브젝트 태그로 식별
                                if (HitObj.transform.gameObject.tag == "Bottle")
                                {
                                    BottleHit(HitObj.transform.gameObject);
                                }
                                else if (HitObj.transform.gameObject.tag == "Enemy")
                                {
                                    EnemyHit(HitObj.transform.gameObject);
                                }
                                else if (HitObj.transform.gameObject.tag == "Button")
                                {
                                    ButtonHit(HitObj.transform.gameObject);
                                }

                                //오브젝트 레이어로 식별
                                if (HitObj.transform.gameObject.layer == 8)
                                {
                                    GameObject BulletHole = Instantiate<GameObject>(SandDecal, HitObj.point, Quaternion.identity) as GameObject;
                                    BulletHole.transform.LookAt(this.gameObject.transform.position);
                                    BulletHole.GetComponent<AudioSource>().clip = GM.GetComponent<GameManager>().LoadAudioClip("etc");
                                    BulletHole.GetComponent<AudioSource>().Play();
                                    Destroy(BulletHole, 3f);
                                }
                                else if (HitObj.transform.gameObject.layer == 9)
                                {
                                    GameObject BulletHole = Instantiate<GameObject>(MetalDecal, HitObj.point, Quaternion.identity) as GameObject;
                                    BulletHole.transform.LookAt(this.gameObject.transform.position);
                                    BulletHole.GetComponent<AudioSource>().clip = GM.GetComponent<GameManager>().LoadAudioClip("metal");
                                    BulletHole.GetComponent<AudioSource>().Play();
                                    Destroy(BulletHole, 3f);

                                }
                                else if (HitObj.transform.gameObject.layer == 10)
                                {
                                    GameObject BulletHole = Instantiate<GameObject>(WoodDecal, HitObj.point, Quaternion.identity) as GameObject;
                                    BulletHole.transform.LookAt(this.gameObject.transform.position);
                                    BulletHole.GetComponent<AudioSource>().clip = GM.GetComponent<GameManager>().LoadAudioClip("wood");
                                    BulletHole.GetComponent<AudioSource>().Play();
                                    Destroy(BulletHole, 3f);
                                }
                                else if (HitObj.transform.gameObject.layer == 20)
                                {
                                    GameObject BloodParticle = Instantiate<GameObject>(BloodDecal, HitObj.point, Quaternion.identity) as GameObject;
                                    Destroy(BloodParticle, 3f);
                                }

                            }
                        }
                    }
                    break;
            }
            #endregion

            #region 재장전 가능상황 파악
            if (FireState == false)//발사 불가능 상태 일 경우
            {
                switch (FirePos.tag)//발사 위치 오브젝트의 태그를 통해서 판별
                {
                    case "Left"://왼쪽
                        if (OVRInput.Get(OVRInput.Axis2D.PrimaryThumbstick, OVRInput.Controller.Touch) == new Vector2(0, 0))
                        {
                            ReloadState = true;
                        }
                        break;
                    case "Right": //오른쪽
                        if (OVRInput.Get(OVRInput.Axis2D.SecondaryThumbstick, OVRInput.Controller.Touch) == new Vector2(0, 0))
                        {
                            ReloadState = true;
                        }
                        break;
                }
            }
            #endregion

            #region 재장전
            if (ReloadState == true)
            {
                switch (FirePos.tag)//발사 위치 오브젝트의 태그를 통해서 판별
                {
                    case "Left"://왼쪽
                        ReloadStick = OVRInput.Get(OVRInput.Axis2D.PrimaryThumbstick).y;//스틱컨트롤러y축 버튼
                        if (ReloadStick < -0.9)
                        {
                            PV.RPC("Reload",RpcTarget.All);
                        }
                        break;
                    case "Right": //오른쪽
                        ReloadStick = OVRInput.Get(OVRInput.Axis2D.SecondaryThumbstick).y;//스틱컨트롤러y축 버튼
                        if (ReloadStick < -0.9)
                        {
                            PV.RPC("Reload", RpcTarget.All);
                        }
                        break;
                }
            }
            #endregion
        }
    }

    #region FX
    [PunRPC]
    private void Gun_Fire_FX()
    {
        //격발 효과
        GunAni.SetTrigger("Fire");
        GunFireEffect.Play();
        //사운드 효과
        HandGunFireAudio.clip = GM.GetComponent<GameManager>().LoadAudioClip("fire");
        HandGunFireAudio.Play();
    }
    [PunRPC]
    private void Gun_BulletEmpty_FX()
    {
        //격발불가 효과
        GunAni.SetTrigger("FireF");
        //사운드 효과
        HandGunFireClickAudio.clip = GM.GetComponent<GameManager>().LoadAudioClip("empty");
        HandGunFireClickAudio.Play();
    }
    [PunRPC]
    private void Gun_Reload_FX()
    {
        //사운드 효과
        HandGunReloadAudio.clip = GM.GetComponent<GameManager>().LoadAudioClip("reload");
        HandGunReloadAudio.Play();
    }


    #endregion

    #region Gun
    private bool Fire()
    {
        if (Bullet > 0&&FireState)//총알이 있고 발사가능상태
        {
            PV.RPC("Gun_Fire_SFX",RpcTarget.All);
            //총알 감소 격발 상태 
            if (SceneIdx == 1 || SceneIdx == 2|| SceneIdx ==6 )//메뉴 씬이 아닐 경우
            {

            }
            else
            {
                //----------수정요망-----------------
                Bullet--;
                #region UI
                BulletUIImage.sprite = BulletUI[Bullet];
                #endregion
                //----------수정요망-----------------
            }
            //----------수정요망-----------------
            FireState = false;
            //----------수정요망-----------------
            return true;
        }
        else if(!FireState)
        {
           
            PV.RPC("Gun_BulletEmpty_FX",RpcTarget.All);
            //----------수정요망-----------------
            FireState = false;
            //----------수정요망-----------------
            return false;
        }
        else
        {
            PV.RPC("Gun_BulletEmpty_FX", RpcTarget.All);
            //----------수정요망-----------------
            FireState = false;
            //----------수정요망-----------------
            return false;
        }
    }
    [PunRPC]
    private void Reload()//재장전
    {
        GunAni.SetTrigger("Reload");
        Gun_Reload_FX();
        //----------수정요망-----------------
        FireState = true;
        ReloadState = false;
        //----------수정요망-----------------
    }
    #endregion

    #region Gun 정보 전달
    [PunRPC]
    public void setGunInfo(ref bool firestate, ref int bullet)
    {
        this.FireState=firestate;
        this.Bullet = bullet;
    }
    [PunRPC]
    public void getGunInfo(ref bool firestate,ref int bullet)
    {
        firestate = this.FireState;
        bullet = this.Bullet;
    }
    #endregion

    #region 병 식별
    private void BottleHit(GameObject bottle)
    {
        bottle.GetComponent<BottleScript>().SendMessage("Hit");
    }
    #endregion

    #region 적 식별
    private void EnemyHit(GameObject enemy)
    {
        enemy.GetComponent<AIParent>().SendMessage("Hit");
    }
    #endregion

    #region 버튼 식별
    private void ButtonHit(GameObject button)
    {
        button.GetComponent<ButtonClick>().SendMessage("Hit", button);
    }
    #endregion
}
