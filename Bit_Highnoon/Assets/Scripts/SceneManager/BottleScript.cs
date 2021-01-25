using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class BottleScript : MonoBehaviour
{
    [SerializeField] GameObject m_goPrefab = null;
    [SerializeField] float m_force = 0f;
    [SerializeField] Vector3 m_offset = Vector3.zero;

    //병 깨짐 효과
    public void Hit()
    {
        GameObject t_clone = Instantiate(m_goPrefab, transform.position, Quaternion.identity);
        Rigidbody[] t_rigids = t_clone.GetComponentsInChildren<Rigidbody>();
        for(int i = 0; i < t_rigids.Length; i++)
        {
            t_rigids[i].AddExplosionForce(m_force, transform.position + m_offset, 10f);
        }
        gameObject.SetActive(false);
        Invoke("ChangeScene", 1);
    }

    public void ChangeScene()
    {
        if (SceneManager.GetActiveScene().buildIndex == 6)
            GameManager.Instance.IdBottleHit(this.gameObject);
        else 
            GameManager.Instance.MoveScene(this.gameObject);


    }
}
