using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class SampleChange : MonoBehaviour
{
    private object[] CharacterMaterial;     //바디 색상

    private object[] HatMaterials;          //모자 종류

    private Material selected_character;

    private Material selected_hat;

    private PhotonView PV;

    public Material Selected_Character { get { return selected_character; } }
    public Material Selected_Hat { get { return selected_hat; } }

    // Start is called before the first frame update
    void Start()
    {
        CharacterMaterial = Resources.LoadAll("CharacterMaterial");
        HatMaterials = Resources.LoadAll("HatMaterials");
        selected_character = this.gameObject.transform.parent.parent.parent.GetChild(2).GetChild(0).GetComponent<Renderer>().material;
        selected_hat = this.gameObject.transform.parent.parent.parent.GetChild(5).GetChild(0).GetChild(0).GetComponent<Renderer>().material;

        PV = this.gameObject.GetPhotonView();
    }

    public void ChangeBodyColor(string Colorstr)
    {
        foreach (Material mat in CharacterMaterial)
        {
            if (Colorstr == mat.name)
            {
                this.gameObject.transform.GetChild(0).gameObject.GetComponent<Renderer>().material = mat;
                
                PV.RPC("SaveBodyMaterial", RpcTarget.AllBuffered, Colorstr);
            }
        }
    }

    [PunRPC]
    private void SaveBodyMaterial(string Colorstr)
    {
        foreach (Material mat in CharacterMaterial)
        {
            if (Colorstr == mat.name)
            {
                selected_character = mat;
            }
        }
    }

    public void ChangeBodyColor(Material original)
    {
        this.gameObject.transform.GetChild(0).gameObject.GetComponent<Renderer>().material = original;
    }

    public void ChangeHatColor(string hatmaterial)
    {
        foreach (Material mat in HatMaterials)
        {
            if (hatmaterial == mat.name)
            {
                this.gameObject.transform.GetChild(1).GetChild(0).GetChild(0).gameObject.GetComponent<SkinnedMeshRenderer>().material = mat;

                PV.RPC("SaveHatMaterial", RpcTarget.AllBuffered, hatmaterial);
            }
        }
    }

    [PunRPC]
    private void SaveHatMaterial(string hatmaterial)
    {
        foreach (Material mat in HatMaterials)
        {
            if (hatmaterial == mat.name)
            {
                selected_hat = mat;
            }
        }
    }

    public void ChangeHatColor(Material original)
    {
        this.gameObject.transform.GetChild(1).GetChild(0).GetChild(0).gameObject.GetComponent<SkinnedMeshRenderer>().material = original;
    }
}
