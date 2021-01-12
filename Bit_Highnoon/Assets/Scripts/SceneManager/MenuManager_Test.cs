using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager_Test : MonoBehaviour
{
    public class MenuManager : MonoBehaviour
    {
        public static MenuManager Instance;

        private Menu LM = GameObject.Find("Loading").GetComponent<Menu>();
        private Menu TM = GameObject.Find("TitleMenu").GetComponent<Menu>();
        private Menu CM = GameObject.Find("CreateRoomMenu").GetComponent<Menu>();
        private Menu RM = GameObject.Find("RoomMenu").GetComponent<Menu>();
        private Menu EM = GameObject.Find("ErrorMenu").GetComponent<Menu>();
        private Menu FM = GameObject.Find("FindRoomMenu").GetComponent<Menu>();

        [SerializeField] Menu[] menus;
            
        private void Awake()
        {
            Instance = this;
        }
        public void OpenMenu(string menuName)
        {
            
                if (menuName == "FindRoomMenu")
                {
                    FM.Open();
                }
                else if (FM.open)
                {
                    CloseMenu(FM);
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
        public void CloseMenu(Menu menu)
        {
            menu.Close();
        }
    }

}
