using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHit : MonoBehaviour
{
    [SerializeField]
    private  Image Panel;
    [SerializeField]
    private List<GameObject> Controllers;



    void Start()
    {
        //Controllers = new List<GameObject>();
        Panel.color = new Vector4(0,0,0,0);

    }

    private void Update()
    {
        #region 색상확인
        if (Panel.color != (Color)new Vector4(0, 0, 0, 0))
        {
            Panel.color = Panel.color - (Color)new Vector4(0.01f, 0, 0, 0.01f);
        }
        #endregion
    }

    #region 패널 색상 변경
    private void PanelSetRed()
    {
        Panel.color = new Vector4(1, 0, 0, 0.5f);
    }
    #endregion

    #region 피격
    private void Hit()
    {
        PanelSetRed();
    }
    #endregion

    #region 사망
    private void Die()
    {
        PanelSetRed();
        foreach(GameObject controller in Controllers)
        {
            controller.GetComponent<OVRTouchSample.TouchController>().SendMessage("Drop");
        }
    }
    #endregion

}
