using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Net_GameStart : MonoBehaviour
{
    private bool istart = false;
    private bool isready;

    private void Update()
    {
        if (istart == false)
        {
            GameObject[] players = GameObject.FindGameObjectsWithTag("Player");

            if (players.Length >= 2)
            {
                foreach (GameObject player in players)
                {
                    if (player.transform.GetChild(5).GetChild(3).GetComponent<Net_Ready>().IsReady == true)    //<=== 수정    //player.준비가 완료되었다는 변수 == true
                    {
                        isready = true;
                    }
                    else
                    {
                        isready = false;
                        break;
                    }
                }

                if (isready == true)
                {
                    foreach (GameObject player in players)
                        player.transform.GetChild(5).GetChild(3).GetComponent<Net_Ready>().SendMessage("Guide");

                    StartCoroutine(GameStart());
                    istart = true;
                }
                else
                {
                    foreach (GameObject player in players)
                        player.transform.GetChild(5).GetChild(3).GetComponent<Net_Ready>().SendMessage("Wait_Ready");
                }
            }
            else
            {
                foreach (GameObject player in players)
                    player.transform.GetChild(5).GetChild(3).GetComponent<Net_Ready>().SendMessage("Wait_Comein");
            }
        }
    }

    private IEnumerator GameStart()
    {
        yield return new WaitForSeconds(5f);

        GameManager.Instance.GameStart();
    }
}
