using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SoundManager : MonoBehaviour
{
    public AudioMixer audioMixer;
    public Slider volumeSlider;
    public Slider musicSlider;

    private float v_sound = 0;
    private float m_sound = 0;

    #region 사운드 조정
    //SFX 사운드 조정
    public void v_AudioControl()
    {
        v_sound = volumeSlider.value;

        if (v_sound == -20f)
            audioMixer.SetFloat("SFX", -80);
        else
            audioMixer.SetFloat("SFX", v_sound);
    }

    //BGM 사운드 조정
    public void m_AudioControl()
    {
        m_sound = musicSlider.value;

        if (m_sound == -20f)
            audioMixer.SetFloat("BGM", -80);
        else
            audioMixer.SetFloat("BGM", m_sound);
    }
    #endregion

    #region SFX 슬라이더 바 조정
    public void VolumeDown()
    {
        if (v_sound <= 0 && v_sound > -20)
        {
            v_sound -= 2f;
            volumeSlider.value = v_sound;
            v_AudioControl();
        }
        else
            return;
    }

    public void VolumeUp()
    {
        if (v_sound >= -20 && v_sound < 0)
        {
            v_sound += 2f;
            volumeSlider.value = v_sound;
            v_AudioControl();
        }
        else
            return;
    }
    #endregion

    #region BGM 슬라이더 바 조정
    public void MusicDown()
    {
        if (m_sound <= 0 && m_sound > -20)
        {
            m_sound -= 2f;
            musicSlider.value = m_sound;
            m_AudioControl();
        }
        else
            return;
    }

    public void MusicUp()
    {
        if (m_sound >= -20 && m_sound < 0)
        {
            m_sound += 2f;
            musicSlider.value = m_sound;
            m_AudioControl();
        }
        else
            return;
    }
    #endregion

    #region 옵션창 숨기기
    public void HideOption()
    {
        GameObject.Find("OptionPicket").transform.GetChild(0).gameObject.SetActive(false);
    }
    #endregion
}
