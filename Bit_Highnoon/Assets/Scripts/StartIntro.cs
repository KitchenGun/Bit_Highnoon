using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartIntro : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Invoke("InvokeMain", 5f);
    }

    void InvokeMain()
    {
        int idx = SceneManager.GetActiveScene().buildIndex + 1;
        GameManager.Instance.NextToScene(idx);
    }

}
