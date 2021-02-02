using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class BodyMaterial : MonoBehaviour
{
    private object[] materials;

    private PhotonView PV;
    private GameManager GM;    

    // Start is called before the first frame update
    void Start()
    {
        GM = GameObject.Find("GameManager").GetComponent<GameManager>();
        PV = this.gameObject.GetPhotonView();

        if(PV.IsMine == true)
        {
            GM.SendMessage("UserName", PV.Owner.NickName);
            PV.RPC("ChangeMaterial", RpcTarget.AllBuffered, GM.Char_Material);
        }
    }

    [PunRPC]
    private void ChangeMaterial(string char_material)
    {
        materials = Resources.LoadAll("CharacterMaterial");

        if (PV.Owner.NickName == GM.GetUserName())
        {
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
}
