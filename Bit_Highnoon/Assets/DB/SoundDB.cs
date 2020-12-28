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
    List<AudioClip> list = new List<AudioClip>();
    //주소값 변동있을수 있음
    readonly string xmlname = "SoundData";
    XmlDocument xmlDoc = new XmlDocument();

    public void XmlLoad()
    {
        TextAsset txtAsset = (TextAsset)Resources.Load("DBXML/" + xmlname);
        xmlDoc.LoadXml(txtAsset.text);
    }


    #region 총소리

    //떨어지는소리
    public AudioClip GunDropSound()
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

        //랜덤값 => 추후 값 수정 필요
        int randomdrop = UnityEngine.Random.Range(0, sound.Length);

        return list[randomdrop];
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


    //잡는소리
    public AudioClip GunGripSound()
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

        //랜덤값 => 추후 값 수정 필요
        int randomgrip = UnityEngine.Random.Range(0, sound.Length);

        return list[randomgrip];
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


    //발사소리
    public AudioClip GunFireSound()
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

        //랜덤값 => 추후 값 수정 필요
        int randomfire = UnityEngine.Random.Range(0, sound.Length);

        return list[randomfire];
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


    //장전소리
    public AudioClip GunReloadSound()
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

        //사운드클립 리스트에 입력
        GunReloadList(gunfire);

        return list[0];
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

    #endregion
}