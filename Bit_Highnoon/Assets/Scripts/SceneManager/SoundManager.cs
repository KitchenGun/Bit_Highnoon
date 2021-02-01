using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SoundManager : MonoBehaviour
{
    public AudioMixer audioMixer;
    public Slider audioSlider;

    private void Start()
    {
        AudioListener.volume = 1;
    }

    public void AudioControl()
    {
        float sound = audioSlider.value;

        if (sound == -40f)
            audioMixer.SetFloat("Master", -80);
        else
            audioMixer.SetFloat("Master", sound);
    }

    public void VolumeDown()
    {
        if (audioSlider.value <= 0 || audioSlider.value > -40)
        {
            audioSlider.value -= 4.0f;
        }
        else
            return;
    }

    public void VolumeUp()
    {
        if (audioSlider.value >= -40 || audioSlider.value < 0)
        {
            audioSlider.value += 4.0f;
        }
        else
            return;
    }
}
