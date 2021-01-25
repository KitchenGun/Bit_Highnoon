using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Net_Ready : MonoBehaviour
{
    private bool isready;   //준비되었는지 확인

    private bool isload;    //준비하고 기다리는지 확인

    private PhotonView PV;

    public bool IsReady { get { return isready; } }

    void Start()
    {
        isready = isload = false;

        PV = this.gameObject.GetPhotonView();

        if (PV.IsMine == false)
            this.gameObject.transform.GetChild(0).gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (PV.IsMine)
        {
            if (isready == false && (OVRInput.GetDown(OVRInput.Button.One) || Input.GetKeyDown(KeyCode.A)))
            {
                //Ready UI 종료
                this.gameObject.transform.GetChild(0).gameObject.SetActive(false);

                PV.RPC("ReadyOk", RpcTarget.AllBuffered);

                //this.gameObject.transform.parent.GetChild(5).gameObject.SetActive(true);
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
            if (isload == true)
                this.gameObject.transform.parent.GetChild(5).gameObject.SetActive(false);

            this.gameObject.transform.parent.GetChild(4).gameObject.SetActive(true);

            Destroy(this.gameObject.transform.parent.GetChild(4).gameObject, 3f);
        }
    }

    private void LoadWait()
    {
        if (PV.IsMine)
        {
            if (isready == true)
            {
                this.gameObject.transform.parent.GetChild(5).gameObject.SetActive(true);
                isload = true;
            }
        }
    }
}
