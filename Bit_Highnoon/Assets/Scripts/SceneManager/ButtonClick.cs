using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;


public class ButtonClick : MonoBehaviour
{public void Hit()
    {
        if (this.gameObject == GameObject.Find("Canvas").transform.GetChild(1).GetChild(0).gameObject)
        {
            MenuManager.Instance.OpenMenu("FindRoomMenu");
        }
        else if (this.gameObject == GameObject.Find("Canvas").transform.GetChild(1).GetChild(1).gameObject)
        {
            MenuManager.Instance.OpenMenu("CreateRoomMenu");
        }
        else if (this.gameObject == GameObject.Find("Canvas").transform.GetChild(2).GetChild(1).gameObject)
        {
            NetworkManager.Instance.CreateRoom();
        }
        else if (this.gameObject == GameObject.Find("Canvas").transform.GetChild(3).GetChild(1).gameObject)
        {
            NetworkManager.Instance.LeaveRoom();
        }
        else if (this.gameObject == GameObject.Find("Canvas").transform.GetChild(3).GetChild(2).gameObject)
        {
            NetworkManager.Instance.StartGame();
        }
        else if (this.gameObject == GameObject.Find("Canvas").transform.GetChild(4).GetChild(1).gameObject)
        {
            MenuManager.Instance.OpenMenu("TitleMenu");
        }
        else if (this.gameObject == GameObject.Find("Canvas").transform.GetChild(5).GetChild(2).gameObject)
        {
            MenuManager.Instance.OpenMenu("TitleMenu");
        }
    }
    
}
