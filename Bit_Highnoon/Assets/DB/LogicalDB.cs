using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Xml;
using UnityEngine;

public class LogicalDB
{
    public DataTable UserInfo { get; set; }

    readonly string schema_fname = "userdb.xsd";
    readonly string xmlname = "userdb.xml";

    #region DB 생성및 xml파일 관리
    //논리적 db생성
    public void CreateTable()
    {
        UserInfo = new DataTable("UserInfo");

        //유저 이름
        DataColumn dc_name = new DataColumn("UserName", typeof(string));
        dc_name.AllowDBNull = false;
        UserInfo.Columns.Add(dc_name);

        //클리어 진도
        DataColumn dc_mode = new DataColumn("Mode", typeof(string));
        dc_mode.AllowDBNull = false;
        UserInfo.Columns.Add(dc_mode);

        //승리수
        DataColumn dc_easywin = new DataColumn("EasyWin", typeof(int));
        dc_easywin.AllowDBNull = false;
        UserInfo.Columns.Add(dc_easywin);

        //패배수
        DataColumn dc_easylose = new DataColumn("EasyLose", typeof(int));
        dc_easylose.AllowDBNull = false;
        UserInfo.Columns.Add(dc_easylose);

        //승리수
        DataColumn dc_normalwin = new DataColumn("NormalWin", typeof(int));
        dc_normalwin.AllowDBNull = false;
        UserInfo.Columns.Add(dc_normalwin);

        //패배수
        DataColumn dc_normallose = new DataColumn("NormalLose", typeof(int));
        dc_normallose.AllowDBNull = false;
        UserInfo.Columns.Add(dc_normallose);

        //승리수
        DataColumn dc_hardwin = new DataColumn("HardWin", typeof(int));
        dc_hardwin.AllowDBNull = false;
        UserInfo.Columns.Add(dc_hardwin);

        //패배수
        DataColumn dc_hardlose = new DataColumn("HardLose", typeof(int));
        dc_hardlose.AllowDBNull = false;
        UserInfo.Columns.Add(dc_hardlose);
    }

    //xml 파일생성
    public void Save()
    {
        //테이블의 구조(컬럼 정보)
        UserInfo.WriteXmlSchema(schema_fname, true);

        //테이블의 데이터(로우데이터)
        UserInfo.WriteXml(xmlname, true);
    }

    //xml 불러오기
    public void Load()
    {
        if (File.Exists(schema_fname))
        {
            UserInfo = new DataTable("UserInfo");

            //테이블의 컬럼 생성
            UserInfo.ReadXmlSchema(schema_fname);
            if (File.Exists(xmlname))
            {
                UserInfo.ReadXml(xmlname);
            }
        }
    }

    #endregion

    #region 유저 난이도 설정 변경
    
    //노멀 난이도
    public void NormalUser(string name)
    {
        try
        {
            DataRow dr = UserInfo.Rows.Find(name);
            dr["Mode"] = "normal";
        }
        catch (Exception)
        {
        }
    }

    //하드 난이도
    public void HardUser(string name)
    {
        try
        {
            DataRow dr = UserInfo.Rows.Find(name);
            dr["Mode"] = "hard";
        }
        catch (Exception)
        {
        }
    }

    #endregion

    #region 전적 카운트

    //승리수
    public void EasyWinCount(string name)
    {
        try
        {
            DataRow dr = UserInfo.Rows.Find(name);
            dr["EasyWin"] = int.Parse(dr["EasyWin"].ToString()) + 1;
        }
        catch (Exception)
        {
        }
    }

    //패배수
    public void EasyLoseCount(string name)
    {
        try
        {
            DataRow dr = UserInfo.Rows.Find(name);
            dr["EasyLose"] = int.Parse(dr["EasyLose"].ToString()) + 1;
        }
        catch (Exception)
        {
        }
    }

    #endregion

    #region 파싱

    public void Select(string name)
    {
        try
        {
            DataRow dr = UserInfo.Rows.Find(name);
            string mode = dr["Mode"].ToString();
            int win = int.Parse(dr["EasyWin"].ToString());
            int lose = int.Parse(dr["EasyLose"].ToString());
            Debug.Log(mode);
            Debug.Log(win);
            Debug.Log(lose);
        }
        catch (Exception)
        {
        }
    }

    #endregion

}
