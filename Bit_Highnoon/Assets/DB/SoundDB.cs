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
    //주소값 변동있을수 있음
    readonly string xmlname = "SoundData";
    XmlDocument xmlDoc = new XmlDocument();

    public void XmlLoad()
    {
        TextAsset txtAsset = (TextAsset)Resources.Load("DBXML/" + xmlname);
        xmlDoc.LoadXml(txtAsset.text);
    }


    #region 총소리

    //순서 맞춰야됨

    // 1번 떨어지는소리  0~2
    public void GunDropSound()
    {
        //xml 불러오기
        XmlLoad();

        //파싱
        string gundrop = "";
        XmlNodeList nodes = xmlDoc.SelectNodes("DocumentElement/Gun");
        foreach (XmlNode node in nodes)
        {
            gundrop = node.SelectSingleNode("DropGun").InnerText;
        }

        //파싱값 배열에 입력
        string[] sound = gundrop.Split(new char[] { ',' });

        //사운드클립 리스트에 입력
        for (int i = 0; i < sound.Length; i++)
        {
            GunDropList(sound[i]);
        }     
    }
    public void GunDropList(string fileName)
    {
        string filePath = Application.dataPath + "/DB/Sound/Gun/DropGun/" + fileName + ".wav";
        if (File.Exists(filePath) == true)
        {
            byte[] wavFile = File.ReadAllBytes(filePath);
            list.Add(OpenWavParser.ByteArrayToAudioClip(wavFile));
        }
    }
    public AudioClip GunDrop()
    {
        int randomdrop = UnityEngine.Random.Range(0, 3);

        return list[randomdrop];
    }


    // 2번 잡는소리  3~4
    public void GunGripSound()
    {
        //xml 불러오기
        XmlLoad();

        //파싱
        string gungrip = "";
        XmlNodeList nodes = xmlDoc.SelectNodes("DocumentElement/Gun");
        foreach (XmlNode node in nodes)
        {
            gungrip = node.SelectSingleNode("GripGun").InnerText;
        }

        //파싱값 배열에 입력
        string[] sound = gungrip.Split(new char[] { ',' });

        //사운드클립 리스트에 입력
        for (int i = 0; i < sound.Length; i++)
        {
            GunGripList(sound[i]);
        }    
    }
    public void GunGripList(string fileName)
    {
        string filePath = Application.dataPath + "/DB/Sound/Gun/GripGun/" + fileName + ".wav";
        if (File.Exists(filePath) == true)
        {
            byte[] wavFile = File.ReadAllBytes(filePath);
            list.Add(OpenWavParser.ByteArrayToAudioClip(wavFile));
        }
    }
    public AudioClip GunGrip()
    {
        int randomgrip = UnityEngine.Random.Range(3, 5);

        return list[randomgrip];
    }


    // 3번 발사소리 5~7
    public void GunFireSound()
    {
        //xml 불러오기
        XmlLoad();

        //파싱
        string gunfire = "";
        XmlNodeList nodes = xmlDoc.SelectNodes("DocumentElement/Gun");
        foreach (XmlNode node in nodes)
        {
            gunfire = node.SelectSingleNode("GunFire").InnerText;
        }

        //파싱값 배열에 입력
        string[] sound = gunfire.Split(new char[] { ',' });

        //사운드클립 리스트에 입력
        for(int i=0; i<sound.Length; i++)
        {
            GunFireList(sound[i]);
        }
    }
    public void GunFireList(string fileName)
    {
        string filePath = Application.dataPath + "/DB/Sound/Gun/GunFire/" + fileName + ".wav";
        if (File.Exists(filePath) == true)
        {
            byte[] wavFile = File.ReadAllBytes(filePath);
            list.Add(OpenWavParser.ByteArrayToAudioClip(wavFile));
        }
    }
    public AudioClip GunFire()
    {
        int randomfire = UnityEngine.Random.Range(5, 8);

        return list[randomfire];
    }

    // 4번 적 발사소리 8~11
    public void GunFire3dSound()
    {
        //xml 불러오기
        XmlLoad();

        //파싱
        string gunfire = "";
        XmlNodeList nodes = xmlDoc.SelectNodes("DocumentElement/Gun");
        foreach (XmlNode node in nodes)
        {
            gunfire = node.SelectSingleNode("EnemyFire").InnerText;
        }

        //파싱값 배열에 입력
        string[] sound = gunfire.Split(new char[] { ',' });

        //사운드클립 리스트에 입력
        for (int i = 0; i < sound.Length; i++)
        {
            GunFire3dList(sound[i]);
        }
    }
    public void GunFire3dList(string fileName)
    {
        string filePath = Application.dataPath + "/DB/Sound/Gun/EnemyFire/" + fileName + ".wav";
        if (File.Exists(filePath) == true)
        {
            byte[] wavFile = File.ReadAllBytes(filePath);
            list.Add(OpenWavParser.ByteArrayToAudioClip(wavFile));
        }
    }
    public AudioClip GunFire3d()
    {
        int randomfire = UnityEngine.Random.Range(8, 12);

        return list[randomfire];
    }

    // 5번 적 발사소리(빗나갔을경우) 12~17
    public void GunFireWhizSound()
    {
        //xml 불러오기
        XmlLoad();

        //파싱
        string gunfire = "";
        XmlNodeList nodes = xmlDoc.SelectNodes("DocumentElement/Gun");
        foreach (XmlNode node in nodes)
        {
            gunfire = node.SelectSingleNode("EnemyFireNearby").InnerText;
        }

        //파싱값 배열에 입력
        string[] sound = gunfire.Split(new char[] { ',' });

        //사운드클립 리스트에 입력
        for (int i = 0; i < sound.Length; i++)
        {
            GunFireWhizList(sound[i]);
        }
    }
    public void GunFireWhizList(string fileName)
    {
        string filePath = Application.dataPath + "/DB/Sound/Gun/EnemyFireNearby/" + fileName + ".wav";
        if (File.Exists(filePath) == true)
        {
            byte[] wavFile = File.ReadAllBytes(filePath);
            list.Add(OpenWavParser.ByteArrayToAudioClip(wavFile));
        }
    }
    public AudioClip GunFireWhiz()
    {
        int randomfire = UnityEngine.Random.Range(12, 18);

        return list[randomfire];
    }


    // 6번 장전소리 18, 19 (스틱다운 스틱업 구분)
    public void GunReloadSound()
    {
        //xml 불러오기
        XmlLoad();

        //파싱
        string gunfire = "";
        XmlNodeList nodes = xmlDoc.SelectNodes("DocumentElement/Gun");
        foreach (XmlNode node in nodes)
        {
            gunfire = node.SelectSingleNode("Reload").InnerText;
        }

        //파싱값 배열에 입력
        string[] sound = gunfire.Split(new char[] { ',' });

        //사운드클립 리스트에 입력
        for (int i = 0; i < sound.Length; i++)
        {
            GunReloadList(sound[i]);
        }
    }
    public void GunReloadList(string fileName)
    {
        string filePath = Application.dataPath + "/DB/Sound/Gun/Reload/" + fileName + ".wav";
        if (File.Exists(filePath) == true)
        {
            byte[] wavFile = File.ReadAllBytes(filePath);
            list.Add(OpenWavParser.ByteArrayToAudioClip(wavFile));
        }
    }
    public AudioClip Reload1()
    {
        return list[18];
    }
    public AudioClip Reload2()
    {
        return list[19];
    }

    #endregion
}