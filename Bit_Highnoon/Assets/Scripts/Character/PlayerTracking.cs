using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTracking : MonoBehaviour
{
    [SerializeField]
    private GameObject Head;
    private GameObject Body;

   

    // Start is called before the first frame update
    void Start()
    {
        Body = this.gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        Body.transform.position = Head.transform.position - new Vector3(0, 1f, 0);
        Debug.Log(Head.transform.position);
    }
}
