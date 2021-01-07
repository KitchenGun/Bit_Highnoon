using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Xml;
using UnityEngine;

public class LogicalDB : MonoBehaviour
{
    public DataTable UserInfo { get; set; }

    readonly string schema_fname = "userdb.xsd";
    readonly string xmlname = "userdb.xml";

    #region DB 생성및 xml파일 관리

    //논리적 db생성
    public void CreateTable()
    {
        UserInfo = new DataTable("UserInfo");

        //키값
        DataColumn dc_pid = new DataColumn();
        dc_pid.ColumnName = "PID";
        dc_pid.DataType = typeof(int);
        UserInfo.Columns.Add(dc_pid);

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

        //키등록
        DataColumn[] pkeys = new DataColumn[1];
        pkeys[0] = dc_pid;

        UserInfo.PrimaryKey = pkeys;
    }

    //xml 파일저장
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
            UserInfo.ReadXmlSchema(schema_fname);

            if (File.Exists(xmlname))
            {
                UserInfo.ReadXml(xmlname);
            }
        }
    }

    //초기 설정
    public void StartXml()
    {
        if (File.Exists(schema_fname))
        {
            UserInfo = new DataTable("UserInfo");
            UserInfo.ReadXmlSchema(schema_fname);

            if (File.Exists(xmlname))
            {
                UserInfo.ReadXml(xmlname);
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
            DataRow dr = UserInfo.NewRow();
            dr["PID"] = 1;
            dr["Mode"] = "easy";
            dr["EasyWin"] = 0;
            dr["EasyLose"] = 0;
            dr["NormalWin"] = 0;
            dr["NormalLose"] = 0;
            dr["HardWin"] = 0;
            dr["HardLose"] = 0;

            UserInfo.Rows.Add(dr);
            Save();
        }
        catch(Exception)
        {

        }
    }

    //정보 초기화
    public void ResetUser()
    {
        try
        {
            DataRow dr = UserInfo.Rows.Find(1);

            dr["PID"] = 1;
            dr["UserName"] = "Player";
            dr["Mode"] = "easy";
            dr["EasyWin"] = 0;
            dr["EasyLose"] = 0;
            dr["NormalWin"] = 0;
            dr["NormalLose"] = 0;
            dr["HardWin"] = 0;
            dr["HardLose"] = 0;
            Save();
        }
        catch (Exception)
        {

        }
    }

    #endregion

    #region 유저 난이도 설정 변경

    //노멀 난이도
    public void NormalUser()
    {
        try
        {
            StartXml();

            DataRow dr = UserInfo.Rows.Find(1);
            dr["Mode"] = "normal";
            Save();
        }
        catch (Exception)
        {
        }
    }

    //하드 난이도
    public void HardUser()
    {
        try
        {
            StartXml();
            DataRow dr = UserInfo.Rows.Find(1);
            dr["Mode"] = "hard";
            Save();
        }
        catch (Exception)
        {
        }
    }

    #endregion

    #region 전적 카운트

    //승리수(easy)
    public void EasyWinCount()
    {
        try
        {
            StartXml();

            DataRow dr = UserInfo.Rows.Find(1);
            dr["EasyWin"] = int.Parse(dr["EasyWin"].ToString()) + 1;
            Save();
        }
        catch (Exception)
        {
        }
    }

    //패배수(easy)
    public void EasyLoseCount()
    {
        try
        {
            StartXml();
            DataRow dr = UserInfo.Rows.Find(1);
            dr["EasyLose"] = int.Parse(dr["EasyLose"].ToString()) + 1;
            Save();
        }
        catch (Exception)
        {
        }
    }

    //승리수(normal)
    public void NormalWinCount()
    {
        try
        {
            StartXml();
            DataRow dr = UserInfo.Rows.Find(1);
            dr["NormalWin"] = int.Parse(dr["NormalWin"].ToString()) + 1;
            Save();
        }
        catch (Exception)
        {
        }
    }

    //패배수(normal)
    public void NormalLoseCount()
    {
        try
        {
            StartXml();
            DataRow dr = UserInfo.Rows.Find(1);
            dr["NormalLose"] = int.Parse(dr["NormalLose"].ToString()) + 1;
            Save();
        }
        catch (Exception)
        {
        }
    }

    //승리수(hard)
    public void HardWinCount()
    {
        try
        {
            StartXml();
            DataRow dr = UserInfo.Rows.Find(1);
            dr["HardWin"] = int.Parse(dr["HardWin"].ToString()) + 1;
            Save();
        }
        catch (Exception)
        {
        }
    }

    //패배수(hard)
    public void HardLoseCount()
    {
        try
        {
            StartXml();
            DataRow dr = UserInfo.Rows.Find(1);
            dr["HardLose"] = int.Parse(dr["HardLose"].ToString()) + 1;
            Save();
        }
        catch (Exception)
        {
        }
    }

    #endregion

    #region 파싱

    //난이도
    public string Mode()
    {
        try
        {
            StartXml();
            DataRow dr = UserInfo.Rows.Find(1);

            string mode = dr["Mode"].ToString();

            return mode;
        }
        catch (Exception)
        {
            return "";
        }
    }

    //이지 승률
    public int EasyRate()
    {
        try
        {
            StartXml();
            DataRow dr = UserInfo.Rows.Find(1);

            int win = int.Parse(dr["EasyWin"].ToString());
            int lose = int.Parse(dr["EasyLose"].ToString());

            float rate = (win / (win + (float)lose)) * 100;

            return (int)rate;
        }
        catch (Exception)
        {
            return 0;
        }
    }

    //노말 승률
    public int NormalRate()
    {
        try
        {
            StartXml();
            DataRow dr = UserInfo.Rows.Find(1);

            int win = int.Parse(dr["NormalWin"].ToString());
            int lose = int.Parse(dr["NormalLose"].ToString());

            float rate = (win / (win + (float)lose)) * 100;

            return (int)rate;
        }
        catch (Exception)
        {
            return 0;
        }
    }

    //하드 승률
    public int HardRate()
    {
        try
        {
            StartXml();
            DataRow dr = UserInfo.Rows.Find(1);

            int win = int.Parse(dr["HardWin"].ToString());
            int lose = int.Parse(dr["HardLose"].ToString());

            float rate = (win / (win + (float)lose)) * 100;

            return (int)rate;
        }
        catch (Exception)
        {
            return 0;
        }
    }


    public void Select()
    {
        try
        {
            DataRow dr = UserInfo.Rows.Find(1);

            int win = int.Parse(dr["EasyWin"].ToString());

            Debug.Log(win);
        }
        catch (Exception)
        {
        }
    }

    #endregion

}
