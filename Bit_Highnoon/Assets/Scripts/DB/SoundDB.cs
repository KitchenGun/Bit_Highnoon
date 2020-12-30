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
    string gunfolder = "/SFX/Gun/";     //총소리

    public void XmlLoad()
    {
        TextAsset txtAsset = (TextAsset)Resources.Load("DBXML/" + xmlname);
        xmlDoc.LoadXml(txtAsset.text);
    }

    #region 오디오 소스 간결화
    public void SoundLoad(string select_nodes, string single_node)
    {
        //xml 불러오기
        XmlLoad();

        //파싱
        string gundrop = "";
        XmlNodeList nodes = xmlDoc.SelectNodes("DocumentElement/" + select_nodes);
        foreach (XmlNode node in nodes)
        {
            gundrop = node.SelectSingleNode(single_node).InnerText;
        }

        //파싱값 배열에 입력
        //string[] sound = gundrop.Split(new char[] { ',' });
        string[] sound = gundrop.Split(',');

        //사운드클립 리스트에 입력
        for (int i = 0; i < sound.Length; i++)
        {
            AddList(sound[i], select_nodes, single_node);
        }
    }
    public void AddList(string fileName, string select_nodes, string single_node)
    {
        string filePath = Application.dataPath + "/SFX/" + 
            select_nodes + "/" + single_node + "/" + fileName + ".wav";
        if (File.Exists(filePath) == true)
        {
            byte[] wavFile = File.ReadAllBytes(filePath);
            list.Add(OpenWavParser.ByteArrayToAudioClip(wavFile));
        }
    }
    public AudioClip LoadAudioClip()
    {
        random = UnityEngine.Random.Range(0, 3);

        return list[random];
    }
    #endregion

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
        string filePath = Application.dataPath + gunfolder + "DropGun/" + fileName + ".wav";
        if (File.Exists(filePath) == true)
        {
            byte[] wavFile = File.ReadAllBytes(filePath);
            list.Add(OpenWavParser.ByteArrayToAudioClip(wavFile));
        }
    }
    public AudioClip GunDrop()
    {
        random = UnityEngine.Random.Range(0, 3);

        return list[random];
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
        string filePath = Application.dataPath + gunfolder + "GripGun/" + fileName + ".wav";
        if (File.Exists(filePath) == true)
        {
            byte[] wavFile = File.ReadAllBytes(filePath);
            list.Add(OpenWavParser.ByteArrayToAudioClip(wavFile));
        }
    }
    public AudioClip GunGrip()
    {
        random = UnityEngine.Random.Range(3, 5);

        return list[random];
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
        string filePath = Application.dataPath + gunfolder + "GunFire/" + fileName + ".wav";
        if (File.Exists(filePath) == true)
        {
            byte[] wavFile = File.ReadAllBytes(filePath);
            list.Add(OpenWavParser.ByteArrayToAudioClip(wavFile));
        }
    }
    public AudioClip GunFire()
    {
        random = UnityEngine.Random.Range(5, 8);

        return list[random];
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
        string filePath = Application.dataPath + gunfolder + "EnemyFire/" + fileName + ".wav";
        if (File.Exists(filePath) == true)
        {
            byte[] wavFile = File.ReadAllBytes(filePath);
            list.Add(OpenWavParser.ByteArrayToAudioClip(wavFile));
        }
    }
    public AudioClip GunFire3d()
    {
        random = UnityEngine.Random.Range(8, 12);

        return list[random];
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
        string filePath = Application.dataPath + gunfolder + "EnemyFireNearby/" + fileName + ".wav";
        if (File.Exists(filePath) == true)
        {
            byte[] wavFile = File.ReadAllBytes(filePath);
            list.Add(OpenWavParser.ByteArrayToAudioClip(wavFile));
        }
    }
    public AudioClip GunFireWhiz()
    {
        random = UnityEngine.Random.Range(12, 18);

        return list[random];
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
        string filePath = Application.dataPath + gunfolder + "Reload/" + fileName + ".wav";
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

    //리스트에 추가
    public void GunSoundList()
    {
        //1
        GunDropSound();
        //2
        GunGripSound();
        //3
        GunFireSound();
        //4
        GunFire3dSound();
        //5
        GunFireWhizSound();
        //6
        GunReloadSound();
    }

    #endregion
}