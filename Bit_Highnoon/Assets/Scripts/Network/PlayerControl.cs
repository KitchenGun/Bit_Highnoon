using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class PlayerControl : MonoBehaviour
{
    public Transform lefteye;
    public Transform righteye;
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
        if (!PV.IsMine)
        {
            head.transform.gameObject.tag = "Untagged";
            lefteye.gameObject.SetActive(false);
            righteye.gameObject.SetActive(false);
            rightHand.gameObject.SetActive(false);
            leftHand.gameObject.SetActive(false);
            head.gameObject.SetActive(false);

            //Destroy(GetComponentInChildren<OVRCameraRig>().gameObject);
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
