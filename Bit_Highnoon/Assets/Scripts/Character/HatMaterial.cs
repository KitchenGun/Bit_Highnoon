using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class HatMaterial : MonoBehaviour
{
    private object[] materials;

    private PhotonView PV;

    // Start is called before the first frame update
    void Start()
    {
        materials = Resources.LoadAll("HatMaterials");

        PV = this.gameObject.GetPhotonView();

        if (PV.IsMine == true)
        {
            PV.RPC("ChangeMaterial", RpcTarget.AllBuffered, GameManager.Instance.Hat_Material);
        }
    }

    [PunRPC]
    private void ChangeMaterial(string hat_material)
    {
        if (hat_material.Equals(string.Empty) == false)
        {
            if (hat_material.Equals("NoHat"))
            {
                this.gameObject.transform.parent.gameObject.SetActive(false);
            }
            else
            {
                foreach (Material mat in materials)
                {
                    if (hat_material.Equals(mat.name))
                    {
                        this.GetComponent<SkinnedMeshRenderer>().material = mat;
                    }
                }
            }
        }
    }
}
