using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SampleChange : MonoBehaviour
{
    private object[] CharacterMaterial;     //바디 색상

    private object[] HatMaterials;          //모자 종류

    private Material selected_character;

    private Material selected_hat;

    public Material Selected_Character { get { return selected_character; } }
    public Material Selected_Hat { get { return selected_hat; } }

    // Start is called before the first frame update
    void Start()
    {
        CharacterMaterial = Resources.LoadAll("CharacterMaterial");
        HatMaterials = Resources.LoadAll("HatMaterials");
    }

    private void ChangeBodyColor(string Colorstr)
    {
        foreach (Material mat in CharacterMaterial)
        {
            if (Colorstr == mat.name)
            {
                this.gameObject.transform.GetChild(0).gameObject.GetComponent<Renderer>().material= selected_character = mat;
            }
        }
    }

    private void ChangeHatColor(string hatmaterial)
    {
        foreach (Material mat in HatMaterials)
        {
            if (hatmaterial == mat.name)
            {
                this.gameObject.transform.GetChild(1).GetChild(0).GetChild(0).gameObject.GetComponent<Renderer>().material = selected_hat = mat;
            }
        }
    }
}
