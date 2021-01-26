using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HatMaterial : MonoBehaviour
{
    private Object[] materials;
    // Start is called before the first frame update
    void Start()
    {
        materials = Resources.LoadAll("HatMaterials");
        this.GetComponent<SkinnedMeshRenderer>().sharedMaterial = (Material)materials[Random.Range(0, materials.Length)];
    }
}
