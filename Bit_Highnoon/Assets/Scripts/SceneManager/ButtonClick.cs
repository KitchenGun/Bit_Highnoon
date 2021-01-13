using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;


public class ButtonClick : MonoBehaviour
{
    public void Hit()
    {
        if (this.gameObject.name == "Find")
        {
            MenuManager.Instance.OpenMenu("FindRoomMenu");
        }
        else if (this.gameObject.name == "Create")
        {
            MenuManager.Instance.OpenMenu("CreateRoomMenu");
        }
        else if (this.gameObject.name == "CreateRoom")
        {
            NetworkManager.Instance.CreateRoom();
        }
        else if (this.gameObject.name == "LeaveRoom")
        {
            NetworkManager.Instance.LeaveRoom();
        }
        else if (this.gameObject.name == "StartRoom")
        {
            NetworkManager.Instance.StartGame();
        }
        else if (this.gameObject.name == "OK")
        {
            MenuManager.Instance.OpenMenu("TitleMenu");
        }
        else if (this.gameObject.name == "Back")
        {
            MenuManager.Instance.OpenMenu("TitleMenu");
        }
    }
}
