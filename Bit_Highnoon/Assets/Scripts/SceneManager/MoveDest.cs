using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveDest : MonoBehaviour
{
    [SerializeField] float m_force = 0f;
    [SerializeField] Vector3 m_offset = Vector3.zero;

    public void OnTriggerEnter(Collider other)
    {
        GameObject obj = other.gameObject;
        Rigidbody t_rigids = GetComponent<Rigidbody>();

        if (obj.gameObject.name == "Plane")
        {
            t_rigids.AddExplosionForce(m_force, transform.position + m_offset, 10f);
        }
    }
}
