using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class OpenCustomize : MonoBehaviour
{
    private bool isopen;

    private GameObject head;

    private PhotonView PV;

    private void Start()
    {
        isopen = false;

        head = this.gameObject.transform.parent.GetChild(5).gameObject;

        PV = this.gameObject.GetPhotonView();

        if (GameManager.Instance.GetSceneIndex() == 8)
        {
            head.transform.GetChild(7).gameObject.SetActive(true);

            Invoke("DeleteOpenCus", 3f);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (PV.IsMine)
        {
            if (GameManager.Instance.GetSceneIndex() == 8)
            {
                if (OVRInput.GetDown(OVRInput.Button.One) || Input.GetKeyDown(KeyCode.A))
                {
                    if (isopen == false)
                    {
                        DeleteOpenCus();

                        OpenCus(true);

                        #region 위치 조정
                        float y = this.gameObject.transform.parent.GetChild(1).transform.position.y;

                        this.gameObject.transform.position = new Vector3(this.gameObject.transform.position.x, y - 1.5f, this.gameObject.transform.position.z);
                        #endregion

                        #region UI
                        head.transform.GetChild(8).gameObject.SetActive(true);

                        Invoke("DeleteSelect", 3f);
                        #endregion

                        isopen = true;
                    }
                    else if (isopen == true)
                    {
                        OpenCus(false);

                        #region 선택정보로 변경
                        PV.RPC("SaveMaterial", RpcTarget.AllBuffered);
                        #endregion

                        CloseCus();

                        isopen = false;
                    }
                }

                if (OVRInput.GetDown(OVRInput.Button.Two) || Input.GetKeyDown(KeyCode.B))
                {
                    if (isopen == true)
                    {
                        #region 샘플을 원래대로
                        this.gameObject.transform.GetChild(0).GetChild(2).GetComponent<SampleChange>().ChangeBodyColor(this.gameObject.transform.parent.GetChild(2).GetChild(0).GetComponent<Renderer>().material);

                        this.gameObject.transform.GetChild(0).GetChild(2).GetComponent<SampleChange>().ChangeHatColor(this.gameObject.transform.parent.GetChild(5).GetChild(0).GetChild(0).GetComponent<Renderer>().material);
                        #endregion

                        OpenCus(false);

                        CloseCus();

                        isopen = false;
                    }
                }
            }
        }
    }

    [PunRPC]
    private void SaveMaterial()
    {
        this.gameObject.transform.parent.GetChild(2).GetChild(0).GetComponent<Renderer>().material =
            this.gameObject.transform.GetChild(0).GetChild(2).GetComponent<SampleChange>().Selected_Character;

        this.gameObject.transform.parent.GetChild(5).GetChild(0).GetChild(0).GetComponent<Renderer>().material =
            this.gameObject.transform.GetChild(0).GetChild(2).GetComponent<SampleChange>().Selected_Hat;
    }

    private void DeleteOpenCus()
    {
        head.transform.GetChild(7).gameObject.SetActive(false);
    }

    private void DeleteSelect()
    {
        head.transform.GetChild(8).gameObject.SetActive(false);

        Invoke("OpenSave", 5f);
    }

    private void OpenSave()
    {
        if (isopen == true)
            head.transform.GetChild(9).gameObject.SetActive(true);

        Invoke("DeleteSave", 3f);
    }

    private void DeleteSave()
    {
        head.transform.GetChild(9).gameObject.SetActive(false);
    }

    private void CloseCus()
    {
        head.transform.GetChild(8).gameObject.SetActive(false);
        head.transform.GetChild(9).gameObject.SetActive(false);
    }

    private void OpenCus(bool state)
    {
        this.gameObject.transform.GetChild(0).GetChild(0).gameObject.SetActive(state);
        this.gameObject.transform.GetChild(0).GetChild(1).gameObject.SetActive(state);
        this.gameObject.transform.GetChild(0).GetChild(2).GetChild(0).gameObject.SetActive(state);
        this.gameObject.transform.GetChild(0).GetChild(2).GetChild(1).gameObject.SetActive(state);
    }

}
