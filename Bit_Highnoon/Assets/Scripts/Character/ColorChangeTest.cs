using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorChangeTest : MonoBehaviour
{
    [SerializeField]
    private Material[] CharacterMaterial;
    public string Colorstr;
    // Start is called before the first frame update
    void Start()
    {
        this.gameObject.GetComponent<Material>();
    }

    // Update is called once per frame
    void Update()
    {
        foreach(Material mat in CharacterMaterial)
        {
            if(Colorstr==mat.name)
            {
                this.gameObject.GetComponent<Renderer>().material =mat;
            }
        }
    }
}
