using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextScene : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            int idx = SceneManager.GetActiveScene().buildIndex + 1;
            GameManager.Instance.NextToScene(idx);
        }

        if(Input.GetKeyDown(KeyCode.Space))
        {
            GameManager.Instance.ExitGame();
        }
    }
}
