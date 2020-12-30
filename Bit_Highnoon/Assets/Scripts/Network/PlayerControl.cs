using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class PlayerControl : MonoBehaviourPunCallbacks
{
    [SerializeField] GameObject cameras;
    public Transform head;
    public Transform leftHand;
    public Transform rightHand;

    PhotonView PV;

    PlayerManager playerManager;
    void Awake()
    {
        PV = GetComponent<PhotonView>();

        playerManager = PhotonView.Find((int)PV.InstantiationData[0]).GetComponent<PlayerManager>();
    }
    void Start()
    {
        if (!PV.IsMine) {
            Destroy(GetComponentInChildren<OVRCameraRig>().gameObject);
        }
    }
    void Update()
    {
        if (!PV.IsMine)
            return;
        MapPosition(transform, XRNode.Head);
        MapPosition(transform, XRNode.LeftHand);
        MapPosition(transform, XRNode.RightHand);
    }
    void MapPosition(Transform target,XRNode node)
    {
        InputDevices.GetDeviceAtXRNode(node).TryGetFeatureValue(CommonUsages.devicePosition, out Vector3 position);
        InputDevices.GetDeviceAtXRNode(node).TryGetFeatureValue(CommonUsages.deviceRotation, out Quaternion rotation);

        target.position = position;
        target.rotation = rotation;

    }
}
