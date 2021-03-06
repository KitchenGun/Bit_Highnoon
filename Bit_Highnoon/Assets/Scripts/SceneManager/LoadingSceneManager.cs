﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Photon.Pun;

public class LoadingSceneManager : MonoBehaviour
{
    public Slider slider;
    private int SceneIndex;
    private float time;

    // Start is called before the first frame update
    void Start()
    {
        SceneIndex = GameManager.Instance.NextSceneIndexCall();
        StartCoroutine(LoadAsynSceneCoroutine());
    }

    IEnumerator LoadAsynSceneCoroutine()
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(SceneIndex);
        operation.allowSceneActivation = false;

        while (!operation.isDone)
        {
            time = +Time.time;
            slider.value = time / 5f;

            if(time > 5)
            {
                operation.allowSceneActivation = true;
            }
            yield return null;
        }
    }
}
