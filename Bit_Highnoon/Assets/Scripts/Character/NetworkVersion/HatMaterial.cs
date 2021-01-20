using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HatMaterial : MonoBehaviour
{
    [SerializeField]
    private Material[] materials;
    // Start is called before the first frame update
    void Start()
    {
        this.GetComponent<SkinnedMeshRenderer>().sharedMaterial = materials[Random.Range(0, materials.Length)];
    }
}
