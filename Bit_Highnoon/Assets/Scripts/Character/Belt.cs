using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Belt : MonoBehaviour
{
    private List<GameObject> BeltGun;

    // Start is called before the first frame update
    void Start()
    {
        BeltGun = new List<GameObject>();
        //플레이어 밸트 총 찾기
        FindBeltGun();
    }

    #region 플레이어의 벨트에 달린 총 찾기
    private void FindBeltGun()
    {
        foreach (Transform gun  in GameObject.Find("Belt").transform)
        {
            if (gun.gameObject.name == "BeltGun")
            {
                Debug.Log("ADD");
                BeltGun.Add(gun.gameObject);
            }
        }
    }
    #endregion

    #region 벨트에서 총 꺼네기
    //왼쪽 
    public void GrabGun(string side)
    {
        foreach(GameObject Gun in BeltGun)
        {
            if(Gun.tag == side)
            {
                Gun.GetComponent<MeshRenderer>().enabled = false;
                //Gun.SetActive(false);
            }
        }
    }
    #endregion

    #region 벨트에서 총 넣기
    //왼쪽 
    public void DropGun(string side)
    {
        foreach (GameObject Gun in BeltGun)
        {
            if (Gun.tag == side)
            {
                Gun.GetComponent<MeshRenderer>().enabled = true;
                //Gun.SetActive(true);
            }
        }
    }
    #endregion
}
