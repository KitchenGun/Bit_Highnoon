using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class OpenCustomize : MonoBehaviour
{
    private bool isopen;

    private GameObject head;

    private PhotonView PV;

    private DBServer DB;

    private void Start()
    {
        PV = this.gameObject.GetPhotonView();

        DB = GameObject.Find("GameManager").GetComponent<DBServer>();

        isopen = false;

        head = this.gameObject.transform.parent.GetChild(5).gameObject;

        if (GameManager.Instance.GetSceneIndex() == 7)
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
            if (GameManager.Instance.GetSceneIndex() == 7)
            {
                if (OVRInput.GetDown(OVRInput.Button.One) || Input.GetKeyDown(KeyCode.A))
                {
                    if (isopen == false)
                    {
                        DeleteOpenCus();

                        OpenCus(true);

                        #region 위치 조정
                        float y = this.gameObject.transform.parent.GetChild(3).transform.position.y;

                        this.gameObject.transform.position = new Vector3(this.gameObject.transform.position.x, y, this.gameObject.transform.position.z);
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

                        #region 변경 정보를 DB로 전송
                        //Debug.Log(PV.Owner.ToString().Split('\'')[1]);
                        //Debug.Log(this.gameObject.transform.GetChild(0).GetChild(2).GetComponent<SampleChange>().Selected_Character.name);
                        //Debug.Log(this.gameObject.transform.GetChild(0).GetChild(2).GetComponent<SampleChange>().Selected_Hat.name);
                        if (this.gameObject.transform.GetChild(0).GetChild(2).GetComponent<SampleChange>().Selected_Hat != null)
                        {
                            DB.SendUserChange(PhotonNetwork.LocalPlayer.NickName,
                            this.gameObject.transform.GetChild(0).GetChild(2).GetComponent<SampleChange>().Selected_Character.name,
                            this.gameObject.transform.GetChild(0).GetChild(2).GetComponent<SampleChange>().Selected_Hat.name);
                        }
                        else
                        {
                            DB.SendUserChange(PhotonNetwork.LocalPlayer.NickName,
                            this.gameObject.transform.GetChild(0).GetChild(2).GetComponent<SampleChange>().Selected_Character.name,
                            "NoHat");
                        }
                        #endregion

                        CloseCus();

                        isopen = false;
                    }
                }

                if (OVRInput.GetDown(OVRInput.Button.Two) || Input.GetKeyDown(KeyCode.B))
                {
                    if (isopen == true)
                    {
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

        if (this.gameObject.transform.GetChild(0).GetChild(2).GetComponent<SampleChange>().Selected_Hat == null)
        {
            this.gameObject.transform.parent.GetChild(5).GetChild(0).gameObject.SetActive(false);
        }
        else
        {
            this.gameObject.transform.parent.GetChild(5).GetChild(0).gameObject.SetActive(true);

            this.gameObject.transform.parent.GetChild(5).GetChild(0).GetChild(0).GetComponent<SkinnedMeshRenderer>().material =
                this.gameObject.transform.GetChild(0).GetChild(2).GetComponent<SampleChange>().Selected_Hat;
        }
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

        #region 샘플을 원래대로
        if (state == true)
        {
            this.gameObject.transform.GetChild(0).GetChild(2).GetComponent<SampleChange>().ChangeBodyColor(this.gameObject.transform.parent.GetChild(2).GetChild(0).GetComponent<Renderer>().material);

            if (this.gameObject.transform.parent.GetChild(5).GetChild(0).gameObject.activeSelf == true)
            {
                this.gameObject.transform.GetChild(0).GetChild(2).GetComponent<SampleChange>().ChangeHatColor(this.gameObject.transform.parent.GetChild(5).GetChild(0).GetChild(0).GetComponent<SkinnedMeshRenderer>().material);
            }
            else
            {
                Material met = null;
                this.gameObject.transform.GetChild(0).GetChild(2).GetComponent<SampleChange>().ChangeHatColor(met);
            }
        }
        #endregion
    }

}
