using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Net_Revolver : MonoBehaviour
{
    private PhotonView PV;
    private GameManager GM;
    private bool NewGun = true;
    int Max_bullet=6;
    public int cur_bullet { get; private set; }
    public bool FireState { get; private set; }

    #region Audio
    private AudioSource HitOtherObjAudio;
    #endregion

    private void Start()
    {
        PV = this.gameObject.GetPhotonView();
            GM = GameObject.Find("GameManager").GetComponent<GameManager>();
            if (this.gameObject.name != "Gun(Clone)")
            {
                setbullet(Max_bullet, NewGun);
                setFireState(true);
            }
            HitOtherObjAudio = this.gameObject.GetComponent<AudioSource>();
    }
    public void setbullet(int bullet,bool isNewGun)
    {
        NewGun = isNewGun;
        if (NewGun)
        {
            cur_bullet = 6;
        }
        if(bullet>6)
        {
            Debug.LogError("잘못된 총알수");
            cur_bullet = 0;
        }
        else if(bullet<0)
        {
            Debug.LogError("잘못된 총알수");
            cur_bullet = 0;
        }
        else
        {
           this.cur_bullet = bullet;
        }

    }
    public void setFireState(bool state)
    {
        FireState = state;
    }

    #region 바닥충돌 감지
    private void OnCollisionEnter(Collision collision)
    {
        if (PV.IsMine)
        {
            if (this.gameObject.name != "BeltGun")
            {
                if (collision.gameObject.layer == 8)
                {
                    HitOtherObjAudio.clip = GM.GetComponent<GameManager>().LoadAudioClip("drop");
                    HitOtherObjAudio.Play();
                }
            }
        }
    }
    #endregion
}
