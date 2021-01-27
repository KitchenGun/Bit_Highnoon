using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenCustomize : MonoBehaviour
{
    private bool isopen = false;
    // Update is called once per frame
    void Update()
    {
        if (GameManager.Instance.GetSceneIndex() == 7)
        {
            if (OVRInput.GetDown(OVRInput.Button.One) || Input.GetKeyDown(KeyCode.A))
            {
                if (isopen == false)
                {
                    this.gameObject.transform.GetChild(0).gameObject.SetActive(true);
                    isopen = true;
                }
                else if (isopen == true)
                {
                    this.gameObject.transform.GetChild(0).gameObject.SetActive(false);

                    #region 선택정보로 변경

                    #endregion

                    isopen = false;
                }
            }
            else if (OVRInput.GetDown(OVRInput.Button.Two) || Input.GetKeyDown(KeyCode.B))
            {
                if (isopen == true)
                {
                    this.gameObject.transform.GetChild(0).gameObject.SetActive(false);
                    isopen = false;
                }
            }
        }
    }
}
