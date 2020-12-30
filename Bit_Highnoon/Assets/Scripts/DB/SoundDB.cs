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
    public List<AudioClip> list = new List<AudioClip>();
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
            list.Add(OpenWavParser.ByteArrayToAudioClip(wavFile));
        }
    }

    public void SoundUpdate(int idx)
    {
        list.Clear();

        switch(idx)
        {
            case 0: break;
            case 1: break;
            case 2: break;
            case 3:
                SoundLoad("AI", "GameStartEnd");
                SoundLoad("AI", "Easy");
                SoundLoad("Gun", "EnemyFire");
                break;
            case 4:
                SoundLoad("AI", "GameStartEnd");  //0 ~ 1
                SoundLoad("AI", "Normal");          //2 ~ 5
                SoundLoad("Gun", "EnemyFire");      //6 ~ 9
                break;
            case 5:
                SoundLoad("AI", "GameStartEnd");
                SoundLoad("AI", "Hard");
                SoundLoad("Gun", "EnemyFire");
                break;
            case 7: break;
        }
    }
    #endregion
}