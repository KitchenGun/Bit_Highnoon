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
    //주소값 변동있을수 있음
    readonly string xmlname = "SoundData";

    public void GunFireSound()
    {
        TextAsset txtAsset = (TextAsset)Resources.Load("DBXML/" + xmlname);
        XmlDocument xmlDoc = new XmlDocument();
        xmlDoc.LoadXml(txtAsset.text);

        string gunfire = "";

        XmlNodeList nodes = xmlDoc.SelectNodes("DocumentElement/Gun");
        foreach (XmlNode node in nodes)
        {
            gunfire = node.SelectSingleNode("GunFire").InnerText;
        }

        string[] sound = gunfire.Split(new char[] { ',' });
        int randomfire = UnityEngine.Random.Range(0, sound.Length);

        string gunsound = sound[randomfire];

        GunFireFile(gunsound);            
    }

    public AudioClip GunFireFile(string sound)
    {      
        string path = Application.dataPath + "/DB/Sound/Gun/GunFire/"+ sound+".wav";

        if (File.Exists(path)==true)
        {
            //www클래스 함수를 이용한 방법
            WWW readmusic = new WWW("file://"+path);
            //yield return readmusic;
                     
            //wbpaser를 이용한 방법
            //byte[] wavFile = File.ReadAllBytes(path);
            //audio.clip = OpenWavParser.ByteArrayToAudioClip(wavFile);
        }
        return null;

        //Process.Start(path + sound+".wav");
    }
}