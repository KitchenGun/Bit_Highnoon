using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Xml;
using UnityEngine;

public class MultiDB : MonoBehaviour
{
    public DataTable Multidb { get; set; }

    readonly string schema_fname = "multidb.xsd";
    readonly string xmlname = "multidb.xml";

    #region DB 생성및 xml파일 관리

    //논리적 db생성
    public void CreateTable()
    {
        Multidb = new DataTable("Multidb");

        //키값
        DataColumn dc_pid = new DataColumn();
        dc_pid.ColumnName = "PID";
        dc_pid.DataType = typeof(int);
        Multidb.Columns.Add(dc_pid);

        //닉네임
        DataColumn dc_name = new DataColumn("Name", typeof(string));
        dc_name.AllowDBNull = false;
        Multidb.Columns.Add(dc_name);

        //머리
        DataColumn dc_hat = new DataColumn("Hat", typeof(string));
        dc_hat.AllowDBNull = false;
        Multidb.Columns.Add(dc_hat);

        //바디
        DataColumn dc_body = new DataColumn("Body", typeof(int));
        dc_body.AllowDBNull = false;
        Multidb.Columns.Add(dc_body);


        //키등록
        DataColumn[] pkeys = new DataColumn[1];
        pkeys[0] = dc_pid;

        Multidb.PrimaryKey = pkeys;
    }

    //xml 파일저장
    public void Save()
    {
        //테이블의 구조(컬럼 정보)
        Multidb.WriteXmlSchema(schema_fname, true);

        //테이블의 데이터(로우데이터)
        Multidb.WriteXml(xmlname, true);
    }

    //xml 불러오기
    public void Load()
    {
        if (File.Exists(schema_fname))
        {
            Multidb = new DataTable("Multidb");
            Multidb.ReadXmlSchema(schema_fname);

            if (File.Exists(xmlname))
            {
                Multidb.ReadXml(xmlname);
            }
        }
    }

    //초기 설정
    public void StartXml()
    {
        if (File.Exists(schema_fname))
        {
            Multidb = new DataTable("Multidb");
            Multidb.ReadXmlSchema(schema_fname);

            if (File.Exists(xmlname))
            {
                Multidb.ReadXml(xmlname);
            }
        }
        else
        {
            CreateTable();
            SetUser();
            Load();
        }
    }

    #endregion

    #region 유저 정보 세팅, 초기화

    //처음 정보 세팅
    public void SetUser()
    {
        try
        {
            DataRow dr = Multidb.NewRow();
            dr["PID"] = 1;
            dr["Name"] = null;
            dr["Hat"] = "Cowboyhat";
            dr["Body"] = "White";

            Multidb.Rows.Add(dr);
            Save();
        }
        catch (Exception)
        {

        }
    }

    //정보 초기화
    public void ResetUser()
    {
        try
        {
            DataRow dr = Multidb.Rows.Find(1);

            dr["PID"] = 1;
            dr["Name"] = null;
            dr["Hat"] = "Cowboyhat";
            dr["Body"] = "White";

            Save();
        }
        catch (Exception)
        {

        }
    }

    #endregion

    #region 값 가져오기

    //이름 가저오기
    public string LoadName()
    {
        try
        {
            StartXml();

            DataRow dr = Multidb.Rows.Find(1);

            return dr["Name"].ToString();
        }
        catch (Exception)
        {
            return null;
        }
    }

    //머리 가저오기
    public string LoadHat()
    {
        try
        {
            StartXml();

            DataRow dr = Multidb.Rows.Find(1);

            return dr["Hat"].ToString();
        }
        catch (Exception)
        {
            return null;
        }
    }

    //바디 가져오기
    public string LoadBody()
    {
        try
        {
            StartXml();

            DataRow dr = Multidb.Rows.Find(1);

            return dr["Body"].ToString();
        }
        catch (Exception)
        {
            return null;
        }
    }

    #endregion

    #region 값 저장하기

    //이름 저장하기
    public void SaveName(string namestr)
    {
        try
        {
            StartXml();

            DataRow dr = Multidb.Rows.Find(1);
            dr["Name"] = namestr;
            Save();
        }
        catch (Exception)
        {
        }
    }

    //머리 저장하기
    public void SaveHat(string hatstr)
    {
        try
        {
            StartXml();

            DataRow dr = Multidb.Rows.Find(1);
            dr["Hat"] = hatstr;
            Save();
        }
        catch (Exception)
        {
        }
    }

    //바디 저장하기
    public void SaveBody(string bodystr)
    {
        try
        {
            StartXml();
            DataRow dr = Multidb.Rows.Find(1);
            dr["Body"] = bodystr;
            Save();
        }
        catch (Exception)
        {
        }
    }
    #endregion

}
