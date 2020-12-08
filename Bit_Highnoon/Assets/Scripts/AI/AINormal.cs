using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AINormal : AIParent
{
    protected override void Start()
    {
        base.Start();

        idletime = 3;   //대기 시간

        deadtime = 15;  //플레이어가 죽는 시간
    }
}
