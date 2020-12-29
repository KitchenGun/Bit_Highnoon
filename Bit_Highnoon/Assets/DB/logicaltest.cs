using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class logicaltest : MonoBehaviour
{
    LogicalDB db = new LogicalDB();

    void Start()
    {
        db.StartXml();
        db.EasyLoseCount(1);
        db.EasyWinCount(1);
        db.NormalLoseCount(1);
        db.NormalWinCount(1);
        db.NormalWinCount(1);
        db.HardLoseCount(1);
        db.HardWinCount(1);
        db.NormalUser(1);
        db.Select(1);

        db.ResetUser(1);
    }
}
