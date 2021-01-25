using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Net_Ready : MonoBehaviour
{
    private bool isready;

    private PhotonView PV;

    public bool IsReady { get { return isready; } }

    void Start()
    {
        isready = false;

        PV = this.gameObject.GetPhotonView();
    }

    // Update is called once per frame
    void Update()
    {
        if (PV.IsMine)
            PV.RPC("ReadyOk", RpcTarget.All);
    }

    [PunRPC]
    private void ReadyOk()
    {
        if (isready == false && (OVRInput.GetDown(OVRInput.Button.One) || Input.GetKeyDown(KeyCode.A)))
        {
            //Ready UI 종료
            this.gameObject.transform.GetChild(0).gameObject.SetActive(false);

            isready = true;
        }
    }

    private void Guide()
    {
        if (PV.IsMine)
        {
            this.gameObject.transform.parent.GetChild(4).gameObject.SetActive(true);

            Destroy(this.gameObject.transform.parent.GetChild(4).gameObject, 3f);
        }
    }
}
