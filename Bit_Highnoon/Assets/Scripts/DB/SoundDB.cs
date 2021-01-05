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
    private Dictionary<string, AudioClip> audioList = new Dictionary<string, AudioClip>();

    public Dictionary<string, AudioClip> AudioList 
    { 
        get { return audioList; } 
    }

    //주소값 변동있을수 있음
    readonly string xmlname = "SoundData";
    XmlDocument xmlDoc = new XmlDocument();

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
            audioList.Add(fileName, OpenWavParser.ByteArrayToAudioClip(wavFile));
        }
    }

    public void SoundUpdate(int idx)
    {
        audioList.Clear();

        //Dictionary<string, string> folder = new Dictionary<string, string>();

        List<string> folder1 = new List<string>();
        List<string> folder2 = new List<string>();

        #region 모든 씬에 들어가는 소리
        folder1.AddRange(("Gun,Gun,Gun,Gun,User").Split(','));
        folder2.AddRange(("DropGun,GripGun,GunFire,Reload,Walk").Split(','));
        #endregion

        switch (idx)
        {
            case 0: break;
            case 1:
                folder1.AddRange(("Effect").Split(','));
                folder2.AddRange(("glass").Split(','));
                break;
            case 2:
                folder1.AddRange(("Effect").Split(','));
                folder2.AddRange(("glass").Split(','));
                break;
            case 3:
                folder1.AddRange(("AI,Gun,Bgm").Split(','));
                folder2.AddRange(("Easy,EnemyFire,Battle").Split(','));
                break;
            case 4:
                folder1.AddRange(("AI,Gun,Bgm").Split(','));
                folder2.AddRange(("Normal,EnemyFire,Battle").Split(','));
                break;
            case 5:
                folder1.AddRange(("AI,Gun,Bgm").Split(','));
                folder2.AddRange(("Hard,EnemyFire,Battle").Split(','));
                break;
            case 6: break;
        }

        if(folder1 != null && folder1.Count == folder2.Count)
        {
            for (int i = 0; i < folder1.Count; i++)
                SoundLoad(folder1[i], folder2[i]);
        }
    }
    #endregion
}