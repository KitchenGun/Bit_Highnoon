using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Net_GameStart : MonoBehaviour
{
    private bool istart = false;

    private void Update()
    {
        if (istart == false)
        {
            GameObject[] players = GameObject.FindGameObjectsWithTag("Player");

            if (players.Length >= 2)
            {
                StartCoroutine(GameStart());
                istart = true;
            }
        }
    }

    private IEnumerator GameStart()
    {
        yield return new WaitForSeconds(5f);

        GameManager.Instance.GameStart();
    }
}
