using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIEasy : AIParent
{
    protected override void Start()
    {
        base.Start();

        idletime = 5;   //대기시간
            
        deadtime = 20;  //플레이어가 죽는 시간
    }
}
