using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;


public class ButtonClick : MonoBehaviour
{
    public GameObject hitObject;

    public void Hit()
    {
        
        MenuManager mm = GameObject.Find("Canvas").GetComponent<MenuManager>();
        NetworkManager nm = GameObject.Find("Canvas").GetComponent<NetworkManager>();

        if (this.gameObject.name == "Find")
        {
            hitObject.GetComponent<MenuManager>().OpenMenu("FindRoomMenu");
        }
        else if (this.gameObject.name == "Create")
        {
            mm.OpenMenu("CreateRoomMenu");
        }
        else if (this.gameObject.name == "CreateRoom")
        {
            nm.CreateRoom();
        }
        else if (this.gameObject.name == "LeaveRoom")
        {
            nm.LeaveRoom();
        }
        else if (this.gameObject.name == "StartRoom")
        {
            nm.StartGame();
        }
        else if (this.gameObject.name == "OK")
        {
            mm.OpenMenu("TitleMenu");
        }
        else if (this.gameObject.name == "Back")
        {
            mm.OpenMenu("TitleMenu");
        }
    }
}
