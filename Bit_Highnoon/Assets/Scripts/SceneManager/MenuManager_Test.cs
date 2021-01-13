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
                CloseMenu(LM);
                CloseMenu(TM);
                CloseMenu(CM);
                CloseMenu(RM);
                CloseMenu(EM);
            }
            else if (menuName == "TitleMenu")
            {
                TM.Open();
                CloseMenu(LM);
                CloseMenu(CM);
                CloseMenu(RM);
                CloseMenu(EM);
                CloseMenu(FM);
            }
            else if (menuName == "CreateRoomMenu")
            {
                CM.Open();
                CloseMenu(LM);
                CloseMenu(TM);
                CloseMenu(RM);
                CloseMenu(EM);
                CloseMenu(FM);
            }
            else if (menuName == "RoomMenu")
            {
                RM.Open();
                CloseMenu(LM);
                CloseMenu(TM);
                CloseMenu(CM);
                CloseMenu(EM);
                CloseMenu(FM);
            }
            else if (menuName == "ErrorMenu")
            {
                EM.Open();
                CloseMenu(LM);
                CloseMenu(TM);
                CloseMenu(CM);
                CloseMenu(RM);
                CloseMenu(FM);
            }
        }
        public void CloseMenu(Menu menu)
        {
            menu.Close();
        }
    }

}
