using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PrintResult : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
        int result = GameManager.Instance.ReturnResult(this.name);

        this.gameObject.GetComponent<Text>().text = result + "%";
    }
}
