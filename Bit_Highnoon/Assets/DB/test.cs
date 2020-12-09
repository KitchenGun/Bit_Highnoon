using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test : MonoBehaviour
{
    LogicalDB db = new LogicalDB();
    // Start is called before the first frame update
    void Start()
    {
        db.CreateTable();

        db.Load();
        db.InsertUser("aaa", "red");
        db.InsertUser("bbb", "blue");
        db.NormalUser(1);
        db.WinCount(1);
        db.Save();
    }

}
