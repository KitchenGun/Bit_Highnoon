﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;

public class LobbyReady : MonoBehaviour
{
    private int ready_count;

    private PhotonView PV;

    // Start is called before the first frame update
    void Start()
    {
        PV = this.gameObject.GetPhotonView();

        ready_count = 0;
    }

    #region 준비
    private void Ready()
    {
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");

        if (players.Length >= 2 && (this.gameObject.transform.GetChild(0).GetComponent<Text>().text.Equals("Ready")))
        {
            PV.RPC("CountUp", RpcTarget.AllBuffered);

            this.gameObject.transform.GetChild(0).GetComponent<Text>().text = "OK";
        }
    }
    #endregion

    #region 모든 사용자 레디시 전투씬으로 이동
    [PunRPC]
    private void CountUp()
    {
        ready_count++;
        if (PhotonNetwork.IsMasterClient)
        {
            if (ready_count == 2)
            {
                PhotonNetwork.AutomaticallySyncScene = true;
                PhotonNetwork.LoadLevel(8);
            }
        }
    }
    #endregion
}
