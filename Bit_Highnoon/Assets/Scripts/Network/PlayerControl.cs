using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class PlayerControl : MonoBehaviourPunCallbacks
{
    public Transform head;
    public Transform leftHand;
    public Transform rightHand;

    PhotonView PV;

    void Awake()
    {
        PV = GetComponent<PhotonView>();

    }
    void Start()
    {
    }
    void Update()
    {
        if (PV.IsMine)
        {
            rightHand.gameObject.SetActive(false);
            leftHand.gameObject.SetActive(false);
            head.gameObject.SetActive(false);

            MapPosition(transform, XRNode.Head);
            MapPosition(transform, XRNode.LeftHand);
            MapPosition(transform, XRNode.RightHand);
        }
    }
    void MapPosition(Transform target,XRNode node)
    {
        InputDevices.GetDeviceAtXRNode(node).TryGetFeatureValue(CommonUsages.devicePosition, out Vector3 position);
        InputDevices.GetDeviceAtXRNode(node).TryGetFeatureValue(CommonUsages.deviceRotation, out Quaternion rotation);

        target.position = position;
        target.rotation = rotation;

    }
}
