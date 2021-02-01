using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SoundManager : MonoBehaviour
{
    public AudioMixer audioMixer;
    public Slider v_audioSlider;
    public Slider m_audioSlider;

    private void Awake()
    {
        //사운드 -20으로 초기화
        v_AudioControl();
        m_AudioControl();
    }

    #region 사운드 조정
    //SFX 사운드 조정
    public void v_AudioControl()
    {
        float sound = v_audioSlider.value;

        if (sound == -40f)
            audioMixer.SetFloat("SFX", -80);
        else
            audioMixer.SetFloat("SFX", sound);
    }

    //BGM 사운드 조정
    public void m_AudioControl()
    {
        float sound = m_audioSlider.value;

        if (sound == -40f)
            audioMixer.SetFloat("BGM", -80);
        else
            audioMixer.SetFloat("BGM", sound);
    }
    #endregion

    #region SFX 슬라이더 바 조정
    public void VolumeDown()
    {
        if (v_audioSlider.value <= 0 && v_audioSlider.value > -40)
        {
            v_audioSlider.value -= 4f;
        }
        else
            return;
    }

    public void VolumeUp()
    {
        if (v_audioSlider.value >= -40 && v_audioSlider.value < 0)
        {
            v_audioSlider.value += 4f;
        }
        else
            return;
    }
    #endregion

    #region BGM 슬라이더 바 조정
    public void MusicDown()
    {
        if (m_audioSlider.value <= 0 && m_audioSlider.value > -40)
        {
            m_audioSlider.value -= 4f;
        }
        else
            return;
    }

    public void MusicUp()
    {
        if (m_audioSlider.value >= -40 && m_audioSlider.value < 0)
        {
            m_audioSlider.value += 4f;
        }
        else
            return;
    }
    #endregion
}
