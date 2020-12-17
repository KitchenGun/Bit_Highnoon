using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using UnityEngine;
using UnityEngine.Networking;

public class SoundDB
{
    //주소값 변동있을수 있음
    readonly string xmlname = "SoundData";
    readonly string soundad = "";

    public void SoundLoad()
    {
        try
        {
            XmlDocument xmldoc = new XmlDocument();
            xmldoc.Load(xmlname);
            XmlElement root = xmldoc.DocumentElement;

            // 노드 요소들

            XmlNodeList nodes = root.ChildNodes;
        }
        catch (Exception)
        {

        }
    }

    public void GunFireSound()
    {
        TextAsset txtAsset = (TextAsset)Resources.Load("DBXML/" + xmlname);
        XmlDocument xmlDoc = new XmlDocument();
        xmlDoc.LoadXml(txtAsset.text);

        string gunfire="";

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

    public IEnumerator GunFireFile(string sound)
    {
        AudioSource audio = new AudioSource();

        using (UnityWebRequest www = UnityWebRequestMultimedia.GetAudioClip(@"../../Bit_Highnoon/Bit_Highnoon/Assets/DB/Sound/Gun/GunFire/" + sound + ".wav", AudioType.WAV))
        {
            yield return www.Send();

            if (www.isNetworkError)
            {
                Debug.Log("Error");
            }
            else
            {
                AudioClip myClip = DownloadHandlerAudioClip.GetContent(www);
                audio.clip = myClip;
                audio.Play();
            }
        }
    }
}
