using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlClient;
using UnityEngine;

public class dlltest : MonoBehaviour
{
    public Boolean DBLogin { get; set; }
    public string ConMsg { get; private set; }

    SqlConnection scon;

    void Start()
    {
        DBInfo();
        UserInsert("ksw");
    }
    
    #region DB연결 및 연결해제

    public void DBInfo()
    {
        DBLogin = false;
        ConMsg = @"Data Source =DESKTOP-R8F9OUG\SQLEXPRESS;Initial Catalog=DeadEye;User ID=ksw;Password=123;";
        scon = new SqlConnection(ConMsg);
    }

    //DB연결
    public bool Connect()
    {
        try
        {
            scon.Open();
            DBLogin = true;
            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }

    //DB연결해제
    public bool DisConnect()
    {
        try
        {
            scon.Close();
            DBLogin = false;
            return false;
        }
        catch (Exception)
        {
            return true;
        }
    }

    #endregion

    #region 기능함수

    //이름 => 번호
    public int NameToNumber(string name)
    {
        try
        {
            Connect();
            int usernumber = 0;

            string comtext = string.Format("Select UserID From UserIndex where UserName = '{0}'", name);
            SqlCommand command = new SqlCommand(comtext, scon);

            //select
            SqlDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                usernumber = int.Parse(reader[0].ToString());
            }

            reader.Close();
            command.Dispose();
            DisConnect();


            return usernumber;

        }
        catch (Exception)
        {
            DisConnect();
            return -1;
        }
    }

    //멀티유저추가
    public bool UserInsert(string name)
    {
        try
        {
            if (NameToNumber(name) == 0)
            {
                //유저 추가
                Connect();
                string comtxt = string.Format("insert into UserIndex (UserName) values ('{0}')",
                    name);

                SqlCommand scom = new SqlCommand(comtxt, scon);
                scom.ExecuteNonQuery();
                scom.Dispose();

                //유저 번호 검색(확인용)
                int usernumber = 0;
                string comtext = string.Format("Select UserID From UserIndex where UserName = '{0}'", name);
                SqlCommand command = new SqlCommand(comtext, scon);
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    usernumber = int.Parse(reader[0].ToString());
                }
                reader.Close();
                command.Dispose();
                Debug.Log(usernumber + "번 유저 : DB추가 성공");
                //

                DisConnect();

                return true;
            }
            else if (NameToNumber(name) == -1)
            {
                Debug.Log("오류발생");
                return false;
            }
            else
            {
                Debug.Log("중복된 이름");
                return false;
            }

        }
        catch (Exception)
        {
            return false;
        }
    }

    //유저 로그인
    public bool UserLogin(string name)
    {
        try
        {
            if (NameToNumber(name) == 0)
            {
                return false;
            }
            else
            {
                //유저 번호 검색(테스트용)
                Connect();
                int usernumber = 0;
                string comtext = string.Format("Select UserID From UserIndex where UserName = '{0}'", name);
                SqlCommand command = new SqlCommand(comtext, scon);
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    usernumber = int.Parse(reader[0].ToString());
                }
                reader.Close();
                command.Dispose();
                DisConnect();
                Debug.Log(usernumber + "번 유저 : 로그인 성공");
                //

                return true;
            }

        }
        catch (Exception)
        {
            return false;
        }
    }

    #endregion
}
