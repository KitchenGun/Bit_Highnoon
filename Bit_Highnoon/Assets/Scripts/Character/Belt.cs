using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Belt : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> BeltGun;

    #region 밸트에 총있는지 확인용 변수
    public bool RightGunSet { get;  set; }
    public bool LeftGunSet{ get;  set; }
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        //양쪽에 총있을때 true
        RightGunSet = true;
        LeftGunSet = true;

    }


    #region 벨트에서 총 꺼네기
    //왼쪽 
    public void GrabGun(string side)
    {
        foreach(GameObject Gun in BeltGun)
        {
            if(Gun.tag == side)
            {
                Gun.GetComponent<MeshRenderer>().enabled = false;
                switch (side)
                {//총을 뽑은쪽 false
                    case "Left":
                        LeftGunSet = false;
                        break;
                    case "Right":
                        RightGunSet = false;
                        break;
                }
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
                switch (side)
                {//총을 뽑은쪽 false
                    case "Left":
                        LeftGunSet = true;
                        break;
                    case "Right":
                        RightGunSet = true;
                        break;
                }
                //Gun.SetActive(true);
            }
        }
    }
    #endregion

    #region 벨트에 총이 있는지 없는지 확인용
    public bool isSet(string side)
    {
        if(side=="Left")
        {
            if (LeftGunSet == true)
                return true;
            else
                return false;
        }
        else if (side == "Right")
        {
            if (RightGunSet == true)
                return true;
            else
                return false;
        }
        return false;
    }
    #endregion
}


