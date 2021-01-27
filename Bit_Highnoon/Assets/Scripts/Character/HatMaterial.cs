using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HatMaterial : MonoBehaviour
{
    private object[] materials;
    // Start is called before the first frame update
    void Start()
    {
        materials = Resources.LoadAll("HatMaterials");
        //this.GetComponent<Renderer>().material = (Material)materials[Random.Range(0, materials.Length)];
    }
}
