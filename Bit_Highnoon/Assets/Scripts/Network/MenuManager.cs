using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class MenuManager : MonoBehaviour
{
    public static MenuManager Instance;
    [SerializeField] Menu[] menus;
    private void Awake()
    {
        Instance = this;
    }

    #region 버튼 클릭됬을때
    public void Hit(GameObject button)
    {
        Button btn = button.gameObject.GetComponent<Button>();

        if (button.gameObject == this.gameObject.transform.GetChild(1).GetChild(0).gameObject)
        {
            btn.onClick.Invoke();
        }
        else if (button.gameObject == this.gameObject.transform.GetChild(1).GetChild(1).gameObject)
        {
            btn.onClick.Invoke();
        }
        else if (button.gameObject == this.gameObject.transform.GetChild(2).GetChild(1).gameObject)
        {
            btn.onClick.Invoke();
        }
        else if (button.gameObject == this.gameObject.transform.GetChild(3).GetChild(1).gameObject)
        {
            btn.onClick.Invoke();
        }
        else if (button.gameObject == this.gameObject.transform.GetChild(3).GetChild(2).gameObject)
        {
            btn.onClick.Invoke();
        }
        else if (button.gameObject == this.gameObject.transform.GetChild(4).GetChild(1).gameObject)
        {
            btn.onClick.Invoke();
        }
        else if (button.gameObject == this.gameObject.transform.GetChild(5).GetChild(2).gameObject)
        {
            btn.onClick.Invoke();
        }
    }
    #endregion

    #region 메뉴 열기
    public void OpenMenu(string menuName)
    {
        for(int i=0; i<menus.Length; i++)
        {
            if(menus[i].menuName == menuName)
            {
                menus[i].Open();
            }
            else if(menus[i].open)
            {
                CloseMenu(menus[i]);
            }
        }
    }
    public void OpenMenu(Menu menu)
    {
        for (int i = 0; i < menus.Length; i++)
        {
            if (menus[i].open)
            {
                CloseMenu(menus[i]);
            }
        }
        menu.Open();
    }
    #endregion

    #region 메뉴 닫기
    public void CloseMenu(Menu menu)
    {
        menu.Close();
    }
    #endregion 
}
