using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class BodyMaterial : MonoBehaviour
{
    private object[] materials;

    private PhotonView PV;

    // Start is called before the first frame update
    void Start()
    {
        PV = this.gameObject.GetPhotonView();

        if(PV.IsMine == true)
        {
            PV.RPC("ChangeMaterial", RpcTarget.AllBuffered, GameObject.Find("GameManager").GetComponent<GameManager>().Char_Material);
        }
    }

    [PunRPC]
    private void ChangeMaterial(string char_material)
    {
        materials = Resources.LoadAll("CharacterMaterial");

            if (char_material.Equals(string.Empty) == false)
            {
                foreach (Material mat in materials)
                {
                    if (char_material.Equals(mat.name))
                    {
                        this.gameObject.GetComponent<Renderer>().material = mat;
                    }
                }
            }
    }
}
