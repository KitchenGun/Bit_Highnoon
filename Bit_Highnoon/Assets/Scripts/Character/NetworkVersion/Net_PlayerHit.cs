﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class Net_PlayerHit : MonoBehaviourPunCallbacks
{
    private GameManager GM;
    private PhotonView PV;
    [SerializeField]
    private  Image Panel;
    [SerializeField]
    private List<GameObject> Controllers;
    //맞은 횟수
    private int hitCount;
    private bool isDeath;

    #region Audio
    private AudioSource HitAudio;
    #endregion


    void Start()
    {
        HitAudio = this.gameObject.GetComponent<AudioSource>();
        GM = GameObject.Find("GameManager").GetComponent<GameManager>();
        PV = this.gameObject.GetPhotonView();//this.gameObject.transform.parent.gameObject.GetPhotonView();
        Panel.color = new Vector4(0, 0, 0, 0);
        hitCount = 0;
        isDeath = false;
    }

    private void FixedUpdate()
    {
        if (PV.IsMine)
        {
            #region 색상확인
            if (Panel.color != (Color)new Vector4(0, 0, 0, 0))
            {
                Panel.color = Panel.color - (Color)new Vector4(0.01f, 0, 0, 0.01f);
            }
            #endregion
        }
    }

    #region 패널 색상 변경
    private void PanelSetRed()
    {
        Panel.color = new Vector4(1, 0, 0, 0.8f);
    }
    #endregion

    #region 피격
    private void Hit()
    {
        hitCount++;
        if (hitCount>=2)//맞은 횟수가 상수보다 클경우 사망처리
        {
            Die();
        }
        else
        {
            if (!isDeath)
            {
                PanelSetRed();
                HitSFX("net_hit");
            }
        }
    }
    #endregion

    #region 사망
    private void Die()
    {
        if (!isDeath)
        {
            isDeath = true;
            PanelSetRed();
            HitSFX("net_death");
            PV.RPC("SendDropGun", RpcTarget.All);

            if(PV.IsMine)
            {
                GameObject gameui = GameObject.Find("GameUI").gameObject;
                gameui.transform.position = gameObject.transform.parent.transform.position + new Vector3(0, 0, 2f);
                gameui.transform.GetChild(2).gameObject.SetActive(true);
            }
        }
    }
    
    //죽을 경우 총을 바닥에 떨어 트림
    [PunRPC]
    private void SendDropGun()
    {
        foreach (GameObject controller in Controllers)
        {
            controller.GetComponent<OVRTouchSample.Net_TouchController>().SendMessage("Drop_Die");
        }
    }
    #endregion


    #region FX
    private void HitSFX(string name)
    {
        HitAudio.clip = GM.GetComponent<GameManager>().LoadAudioClip(name);
        HitAudio.Play();
    }
    #endregion
}
