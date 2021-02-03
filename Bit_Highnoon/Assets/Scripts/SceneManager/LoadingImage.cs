using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadingImage : MonoBehaviour
{
    [SerializeField]
    private Sprite[] LoadingImages;

    private void Start()
    {
        StartCoroutine(Loading());
    }

    private IEnumerator Loading()
    {
        int i = 0;
        do
        {
            if (i < 5)
            {
                this.gameObject.GetComponent<Image>().sprite = LoadingImages[i];
                i++;
            }
            else
            {
                this.gameObject.GetComponent<Image>().sprite = LoadingImages[i];
                i =0;
            }
            yield return new WaitForSeconds(0.3f);
        }
        while (true);
    }

}
