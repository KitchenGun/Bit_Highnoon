using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Revolver : MonoBehaviour
{
    int Max_bullet=6;
    public int cur_bullet { get; private set; }
    public bool FireState { get; private set; }
    public void setbullet(int bullet)
    {
     
        if(bullet>6)
        {
            Debug.LogError("잘못된 총알수");
        }
        else if(bullet<=0)
        {
            Debug.LogError("잘못된 총알수");
        }
        else
        {
           this.cur_bullet = bullet;
        }

    }
    public void setFireState(bool state)
    {
        FireState = state;
    }

    private void Awake()
    {
       //초기화
        setbullet(Max_bullet);
        setFireState(true);
    }
}
