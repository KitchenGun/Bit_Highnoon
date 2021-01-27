using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorChangeTest : MonoBehaviour
{
    private object[] CharacterMaterial;

    public string Colorstr;
    // Start is called before the first frame update
    void Start()
    {
        CharacterMaterial = Resources.LoadAll("CharacterMaterial");
        this.gameObject.GetComponent<Material>();
    }

    // Update is called once per frame
    void Update()
    {
        ChangeColor(Colorstr);
    }

    private void ChangeColor(string Colorstr)
    {
        foreach (Material mat in CharacterMaterial)
        {
            if (Colorstr == mat.name)
            {
                this.gameObject.GetComponent<Renderer>().material = mat;
            }
        }
    }
}
