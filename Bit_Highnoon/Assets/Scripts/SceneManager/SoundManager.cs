using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SoundManager : MonoBehaviour
{
    public AudioMixer audioMixer;
    public Slider audioSlider;

    public void AudioControl()
    {
        float sound = audioSlider.value;

        if (sound == -40f)
            audioMixer.SetFloat("Master", -80);
        else
            audioMixer.SetFloat("Master", sound);
    }
}
