using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlClient;
using UnityEngine;

public class MSSQLTest : MonoBehaviour
{
    // Start is called before the first frame update
    public string ConMsg { get; private set; }

    private SqlConnection scon = new SqlConnection();

    public void DBInfo()
    {
        //mssql아이디 정보 수정!!!
        ConMsg = @"Data Source=DESKTOP-R8F9OUG\SQLEXPRESS;Initial Catalog=DeadEye;User ID=ksw;Password=123";
        scon = new SqlConnection(ConMsg);
    }
    void Start()
    {
        try
        {
            if(Connect() == true)
            {
                DisConnect();
                Debug.Log("연결성공");
            }
            else
            {
                DisConnect();
                Debug.Log("연결실패");
            }
        }
        catch(Exception)
        {
            Debug.Log("연결 실패");
        }
    }

    public Boolean Connect()
    {
        try
        {
            DBInfo();

            scon.Open();
            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }
    public Boolean DisConnect()
    {
        try
        {
            DBInfo();

            scon.Close();
            return false;
        }
        catch (Exception)
        {
            return true;
        }
    }

}
