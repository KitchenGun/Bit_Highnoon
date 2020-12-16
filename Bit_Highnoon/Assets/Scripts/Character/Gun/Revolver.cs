using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Revolver : MonoBehaviour
{
    private bool NewGun = true;
    int Max_bullet=6;
    public int cur_bullet { get; private set; }
    public bool FireState { get; private set; }
    private void Start()
    {
        if (this.gameObject.name != "Gun(Clone)")
        {
            setbullet(Max_bullet, NewGun);
            setFireState(true);
        }
    }
    public void setbullet(int bullet,bool isNewGun)
    {
        NewGun = isNewGun;
        if (NewGun)
        {
            cur_bullet = 6;
        }
        if(bullet>6)
        {
            Debug.LogError("잘못된 총알수");
            cur_bullet = 0;
        }
        else if(bullet<0)
        {
            Debug.LogError("잘못된 총알수");
            cur_bullet = 0;
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

}
