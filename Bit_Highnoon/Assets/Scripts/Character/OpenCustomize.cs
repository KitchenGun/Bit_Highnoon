using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenCustomize : MonoBehaviour
{
    private bool isopen = false;

    // Update is called once per frame
    void Update()
    {
        if (GameManager.Instance.GetSceneIndex() == 8)
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
                    this.gameObject.transform.parent.GetChild(2).GetChild(0).GetComponent<Renderer>().material =
                        this.gameObject.transform.GetChild(0).GetChild(2).GetComponent<SampleChange>().Selected_Character;

                    this.gameObject.transform.parent.GetChild(5).GetChild(0).GetChild(0).GetComponent<Renderer>().material =
                        this.gameObject.transform.GetChild(0).GetChild(2).GetComponent<SampleChange>().Selected_Hat;
                    #endregion

                    isopen = false;
                }
            }

            if (OVRInput.GetDown(OVRInput.Button.Two) || Input.GetKeyDown(KeyCode.B))
            {
                if (isopen == true)
                {
                    this.gameObject.transform.GetChild(0).gameObject.SetActive(false);

                    #region 샘플을 원래대로
                    this.gameObject.transform.GetChild(0).GetChild(2).GetComponent<SampleChange>().ChangeBodyColor(this.gameObject.transform.parent.GetChild(2).GetChild(0).GetComponent<Renderer>().material);

                    this.gameObject.transform.GetChild(0).GetChild(2).GetComponent<SampleChange>().ChangeHatColor(this.gameObject.transform.parent.GetChild(5).GetChild(0).GetChild(0).GetComponent<Renderer>().material);
                    #endregion

                    isopen = false;
                }
            }
        }
    }
}
