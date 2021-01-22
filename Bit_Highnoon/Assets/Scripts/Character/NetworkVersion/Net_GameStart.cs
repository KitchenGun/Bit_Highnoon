using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Net_GameStart : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(GameStart());
    }

    private IEnumerator GameStart()
    {
        yield return new WaitForSeconds(5f);

        GameManager.Instance.GameStart();
    }
}
