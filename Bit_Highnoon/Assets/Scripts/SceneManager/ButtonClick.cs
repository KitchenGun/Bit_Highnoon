using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class ButtonClick : MonoBehaviour
{
   public void Hit()
    {
        Button btn = GameObject.Find("button").GetComponent<Button>();
        btn.onClick.Invoke();
    }
}
