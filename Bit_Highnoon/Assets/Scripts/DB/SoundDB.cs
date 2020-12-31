using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using UnityEngine;
using System.Diagnostics;
using UnityEngine.Networking;

public class SoundDB : MonoBehaviour
{
    public Dictionary<string, AudioClip> list = new Dictionary<string, AudioClip>();
    private int random;

    //주소값 변동있을수 있음
    readonly string xmlname = "SoundData";
    XmlDocument xmlDoc = new XmlDocument();

    //사운드 주소값
    //string gunfolder = "/SFX/Gun/";     //총소리

    public void XmlLoad()
    {
        TextAsset txtAsset = (TextAsset)Resources.Load("DBXML/" + xmlname);
        xmlDoc.LoadXml(txtAsset.text);
    }

    #region 오디오 소스 간결화
    private void SoundLoad(string Folder1, string Folder2)
    {
        //xml 불러오기
        XmlLoad();

        //파싱
        string gundrop = "";
        XmlNodeList nodes = xmlDoc.SelectNodes("DocumentElement/" + Folder1);
        foreach (XmlNode node in nodes)
        {
            gundrop = node.SelectSingleNode(Folder2).InnerText;
        }

        //파싱값 배열에 입력
        //string[] sound = gundrop.Split(new char[] { ',' });
        string[] sound = gundrop.Split(',');

        //사운드클립 리스트에 입력
        for (int i = 0; i < sound.Length; i++)
        {
            AddList(sound[i], Folder1, Folder2);
        }
    }
    
    private void AddList(string fileName, string Folder1, string Folder2)
    {
        string filePath = Application.dataPath + "/SFX/" +
            Folder1 + "/" + Folder2 + "/" + fileName + ".wav";
        if (File.Exists(filePath) == true)
        {
            byte[] wavFile = File.ReadAllBytes(filePath);
            list.Add(fileName, OpenWavParser.ByteArrayToAudioClip(wavFile));
        }
    }

    public void SoundUpdate(int idx)
    {
        list.Clear();

        string[] Folder1 = null;
        string[] Folder2 = null;

        #region 모든 씬에 들어가는 소리
        Folder1 = ("Gun,Gun,Gun,Gun,User").Split(',');
        Folder2 = ("DropGun,GripGun,GunFire,Reload,Walk").Split(',');
        #endregion

        switch (idx)
        {
            case 0: break;
            case 1: break;
            case 2: break;
            case 3:
                Folder1 = ("AI,AI,Gun").Split(',');
                Folder2 = ("GameStartEnd,Easy,EnemyFire").Split(',');
                break;
            case 4:
                Folder1 = ("AI,AI,Gun").Split(',');
                Folder2 = ("GameStartEnd,Normal,EnemyFire").Split(',');
                break;
            case 5:
                Folder1 = ("AI,AI,Gun").Split(',');
                Folder2 = ("GameStartEnd,Hard,EnemyFire").Split(',');
                break;
            case 7: break;
        }

        if(Folder1 != null)
        {
            for (int i = 0; i < Folder1.Length; i++)
                SoundLoad(Folder1[i], Folder2[i]);
        }
    }
    #endregion
}