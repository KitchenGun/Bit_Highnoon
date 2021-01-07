using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class logicaltest : MonoBehaviour
{
    LogicalDB db = new LogicalDB();

    void Start()
    {
        db.StartXml();
        db.EasyLoseCount();

        Debug.Log(db.EasyRate());
        db.Select();
    }
}
