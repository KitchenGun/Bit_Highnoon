using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class KeyboardSet : MonoBehaviour
{
    [SerializeField]
    private GameObject[] NumKey;

    // Start is called before the first frame update
    void Start()
    {
        foreach(GameObject numkey in NumKey)
        {
            numkey.transform.GetChild(0).gameObject.GetComponent<TextMeshPro>().text=numkey.name;
        }
    }
}
