using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoadingSceneManager : MonoBehaviour
{
    public Slider slider;
    private int SceneIndex;
    private float time;

    // Start is called before the first frame update
    void Start()
    {
        SceneIndex = GameManager.Instance.PreSceneIndexCall() + 1;
        StartCoroutine(LoadAsynSceneCoroutine());
    }

    IEnumerator LoadAsynSceneCoroutine()
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(SceneIndex);
        operation.allowSceneActivation = false;

        while (!operation.isDone)
        {
            time = +Time.time;
            slider.value = time / 10f;

            if(time > 10)
            {
                operation.allowSceneActivation = true;
            }
            yield return null;
        }
    }
}
