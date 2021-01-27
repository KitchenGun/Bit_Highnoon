using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Net_Ready : MonoBehaviour
{
    private bool isready;           //준비되었는지 확인

    private bool is_wait_ready;    //준비하고 기다리는지 확인

    private bool is_wait_comein;    //상대가 접속할때 까지 기다리는지 확인

    private PhotonView PV;

    public bool IsReady { get { return isready; } }

    void Start()
    {
        PV = this.gameObject.GetPhotonView();

        if (GameManager.Instance.GetSceneIndex() == 1)
        {
            this.gameObject.transform.GetChild(0).gameObject.SetActive(true);

            if (PV.IsMine == true)
                isready = is_wait_ready = is_wait_comein = false;
            else if (PV.IsMine == false)
                this.gameObject.transform.GetChild(0).gameObject.SetActive(false);
        }
        else
        {
            this.gameObject.transform.GetChild(0).gameObject.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.Instance.GetSceneIndex() == 1)
        {
            if (PV.IsMine)
            {
                if (isready == false && (OVRInput.GetDown(OVRInput.Button.One) || Input.GetKeyDown(KeyCode.A)))
                {
                    //Ready UI 종료
                    this.gameObject.transform.GetChild(0).gameObject.SetActive(false);

                    PV.RPC("ReadyOk", RpcTarget.AllBuffered);
                }
            }
        }
    }

    [PunRPC]
    private void ReadyOk()
    {
        isready = true;
    }

    private void Guide()
    {
        if (PV.IsMine)
        {
            if (is_wait_comein == true)
                this.gameObject.transform.parent.GetChild(6).gameObject.SetActive(false);

            if (is_wait_ready == true)
                this.gameObject.transform.parent.GetChild(5).gameObject.SetActive(false);

            this.gameObject.transform.parent.GetChild(4).gameObject.SetActive(true);

            Destroy(this.gameObject.transform.parent.GetChild(4).gameObject, 3f);
        }
    }

    private void Wait_Ready()
    {
        if (PV.IsMine)
        {
            if (isready == true)
            {
                if (is_wait_comein == true)
                {
                    this.gameObject.transform.parent.GetChild(6).gameObject.SetActive(false);
                    is_wait_comein = false;
                }

                this.gameObject.transform.parent.GetChild(5).gameObject.SetActive(true);
                is_wait_ready = true;
            }
        }
    }

    private void Wait_Comein()
    {
        if (PV.IsMine)
        {
            if (isready == true)
            {
                this.gameObject.transform.parent.GetChild(6).gameObject.SetActive(true);
                is_wait_comein = true;
            }
        }
    }
}
