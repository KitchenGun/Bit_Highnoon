using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindMillRotate : MonoBehaviour
{
    public float turnSpeed = 10;
    // Update is called once per frame
    void Update()
    {
        transform.Rotate(Vector3.forward, turnSpeed * Time.deltaTime);
    }
}
