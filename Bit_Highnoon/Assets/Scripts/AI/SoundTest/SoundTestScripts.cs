using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class SoundTestScripts : MonoBehaviour
{
    AudioSource source;
    //InputField inputFile;
    List<AudioClip> list = new List<AudioClip>(); 

    // Use this for initialization
    void Start()
    {
        source = gameObject.GetComponent<AudioSource>();
        //inputFile = transform.Find("InputField").GetComponent<InputField>();
        LoadCustomFile("fire1");

        source.clip = list[0];

        Play();
    }

    // Player interfaces:
    public void Play()
    {
        source.Play();
    }
    public void Pause()
    {
        source.Pause();
    }
    public void Stop()
    {
        source.Stop();
    }

    // File control:
    public void DeleteClip()
    {
        source.clip = null;
        File.Delete(Path.Combine(Application.persistentDataPath, "MyFile.wav"));
    }
    public void SaveClip()
    {
        byte[] wavFile = OpenWavParser.AudioClipToByteArray(source.clip);
        File.WriteAllBytes(Path.Combine(Application.persistentDataPath, "MyFile.wav"), wavFile);
    }

    public void LoadDefaultFile()
    {
        string filePath = Path.Combine(Application.persistentDataPath, "MyFile.wav");
        if (File.Exists(filePath))
        {
            byte[] wavFile = File.ReadAllBytes(filePath);
            source.clip = OpenWavParser.ByteArrayToAudioClip(wavFile);
            //loadDisplay.text = "Samples: " + source.clip.samples.ToString();
        }
        else
        {
            //loadDisplay.text = "File not found";
        }
    }

    public AudioClip LoadCustomFile(string fileName)
    {
        string filePath = Application.dataPath + "/DB/Sound/Gun/GunFire/" + fileName + ".wav";
        if (File.Exists(filePath) == true)
        {
            byte[] wavFile = File.ReadAllBytes(filePath);
            list.Add(OpenWavParser.ByteArrayToAudioClip(wavFile));
            return OpenWavParser.ByteArrayToAudioClip(wavFile);
            //loadDisplay.text = "Samples: " + source.clip.samples.ToString();
        }
        else
        {
            //loadDisplay.text = "File not found";
            return null;
        }
    }
}
