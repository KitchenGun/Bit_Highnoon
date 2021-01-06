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
         ConMsg = @"Data Source = DESKTOP-R8F9OUG\SQLEXPRESS; 
     user id = ksw;
     password = 123;
     Initial Catalog = DeadEye;";
        SimpleQuery("SELECT * FROM UserInfo WHERE UserID = 1");
    }

    public string SimpleQuery(string _query)
    {
        using (SqlConnection dbCon = new SqlConnection(ConMsg))
        {
            SqlCommand cmd = new SqlCommand(_query, dbCon);
            try
            {
                dbCon.Open();
                string _returnQuery = (string)cmd.ExecuteScalar();
                return _returnQuery;
            }
            catch (SqlException _exception)
            {
                Debug.LogWarning(_exception.ToString());
                return null;
            }
        }
    }

}
