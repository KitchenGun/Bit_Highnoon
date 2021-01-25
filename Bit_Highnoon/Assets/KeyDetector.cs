using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;

public class KeyDetector : MonoBehaviour
{
    private TextMeshPro playerTextOutput;
    // Start is called before the first frame update
    void Start()
    {
        //playerTextOutput = GameObject.FindGameObjectWithTag("PlayerTextOutput").GetComponentInChildren<TextMeshPro>();
    }

    private void OnTriggerEnter(Collider other)
    {
        var key = other.GetComponentInChildren<Label>();
        if (key != null)
        {
            var keyFeedBack = other.gameObject.GetComponent<KeyFeedback>();
            if (keyFeedBack.keyCanBeHitAgain)
            {
                if (key.text == "SPACE")
                {
                    playerTextOutput.text += " ";
                }
                else if (key.text == "<-")
                {
                    playerTextOutput.text = playerTextOutput.text.Substring(0, playerTextOutput.text.Length - 1);
                }
                else
                {
                    playerTextOutput.text += key.text;
                    
                }
                keyFeedBack.keyHit = true;
            }
        }
    }
}
