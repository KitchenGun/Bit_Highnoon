using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    [SerializeField] GameObject CameraHolder;
    bool grounded;
    PhotonView PV;
    void Awake()
    {
        PV = GetComponent<PhotonView>();
    }
    void Start()
    {
        if (!PV.IsMine)
        {
            Destroy(GetComponentInChildren<OVRCameraRig>().gameObject);
        }
    }
    void Update()
    {
        if (!PV.IsMine)
            return;
    }

    public void SetGroundedState(bool _grounded)
    {
        grounded = _grounded;
    }
}
