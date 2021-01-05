using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlClient;
using UnityEngine;

public class dlltest2 : MonoBehaviour
{
    public string ConMsg;

    // Start is called before the first frame update
    void Start()
    {
        ConMsg = @"Data Source=DESKTOP-R8F9OUG\SQLEXPRESS;Initial Catalog=DeadEye;User ID=ksw;Password=123";
        try
        {
            SqlConnection sc = new SqlConnection(ConMsg);
            sc.Open();
            sc.Close();
        }
        catch(Exception)
        {

        }
        
    }

}
