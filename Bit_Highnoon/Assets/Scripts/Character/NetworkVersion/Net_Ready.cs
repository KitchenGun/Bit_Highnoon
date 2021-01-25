using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Net_Ready : MonoBehaviour
{
    private bool isready = false;

    public bool IsReady { get { return isready; } }

    // Update is called once per frame
    void Update()
    {
        if(isready == false && (OVRInput.GetDown(OVRInput.Button.One) || Input.GetKeyDown(KeyCode.A)))
        {
            //Ready UI 종료
            this.gameObject.transform.GetChild(0).gameObject.SetActive(false);

            isready = true;
        }
    }

    private void Guide()
    {
        this.gameObject.transform.parent.GetChild(4).gameObject.SetActive(true);

        Destroy(this.gameObject.transform.parent.GetChild(4).gameObject, 3f);
    }
}
