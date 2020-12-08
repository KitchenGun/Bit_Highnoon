using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.IO;
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

        //유저 번호
        DataColumn dc_userid = new DataColumn();
        dc_userid.ColumnName = "UserNumber";
        dc_userid.DataType = typeof(int);
        dc_userid.AutoIncrement = true;
        dc_userid.AutoIncrementSeed = 1;
        dc_userid.AutoIncrementStep = 1;
        UserInfo.Columns.Add(dc_userid);

        //유저 이름
        DataColumn dc_name = new DataColumn("UserName", typeof(string));
        dc_name.AllowDBNull = false;
        UserInfo.Columns.Add(dc_name);

        //유저 색상
        DataColumn dc_color = new DataColumn("UserColor", typeof(string));
        dc_color.AllowDBNull = false;
        UserInfo.Columns.Add(dc_color);

        //클리어 진도
        DataColumn dc_mode = new DataColumn("Mode", typeof(string));
        dc_mode.AllowDBNull = false;
        UserInfo.Columns.Add(dc_mode);

        //승리수
        DataColumn dc_win = new DataColumn("Win", typeof(int));
        dc_win.AllowDBNull = false;
        UserInfo.Columns.Add(dc_win);

        //패배수
        DataColumn dc_lose = new DataColumn("Lose", typeof(int));
        dc_lose.AllowDBNull = false;
        UserInfo.Columns.Add(dc_lose);

        //primary key등록
        DataColumn[] pkeys = new DataColumn[1];
        pkeys[0] = dc_userid;
        UserInfo.PrimaryKey = pkeys;
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

    #region 유저 추가 및 삭제

    //유저 추가
    public void InsertUser(string name, string color)
    {
        try
        {
            DataRow dr = UserInfo.NewRow();
            dr["UserName"] = name;
            dr["UserColor"] = color;
            dr["Mode"] = "easy";
            dr["Win"] = 0;
            dr["Lose"] = 0;

            UserInfo.Rows.Add(dr);
        }
        catch (Exception)
        {
        }
    }

    //유저 삭제
    public void DeleteUser(int usernum)
    {
        try
        {
            DataRow dr = UserInfo.Rows.Find(usernum);
            UserInfo.Rows.Remove(dr);
        }
        catch (Exception)
        {
        }
    }

    #endregion

    #region 유저 난이도 설정 변경
    
    //노멀 난이도
    public void NormalUser(int usernum)
    {
        try
        {
            DataRow dr = UserInfo.Rows.Find(usernum);
            dr["Mode"] = "normal";
        }
        catch (Exception)
        {
        }
    }

    //하드 난이도
    public void HardUser(int usernum)
    {
        try
        {
            DataRow dr = UserInfo.Rows.Find(usernum);
            dr["Mode"] = "hard";
        }
        catch (Exception)
        {
        }
    }

    #endregion

}
