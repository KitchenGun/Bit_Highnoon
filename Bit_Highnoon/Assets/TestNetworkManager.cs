using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class TestNetworkManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        ConnectedToServer();
    }
    void ConnectedToServer()
    {
        PhotonNetwork.ConnectUsingSettings();
    }
}
