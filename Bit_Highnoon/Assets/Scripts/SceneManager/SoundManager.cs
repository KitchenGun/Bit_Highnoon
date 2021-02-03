using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SoundManager : MonoBehaviour
{
    public Slider volumeSlider;
    public Slider musicSlider;

    private float v_sound = 0;
    private float m_sound = 0;

    public void Awake()
    {
        v_sound = GameManager.Instance.getVSound();
        m_sound = GameManager.Instance.getMSound();
        
        volumeSlider.value = v_sound;
        musicSlider.value = m_sound;
    }

    #region SFX 슬라이더 바 조정
    public void VolumeDown()
    {
        v_sound = GameManager.Instance.getVSound();
        if (v_sound <= 0 && v_sound > -20)
        {
            v_sound -= 2f;
            volumeSlider.value = v_sound;
            GameManager.Instance.v_AudioControl(v_sound);
        }
        else
            return;
    }

    public void VolumeUp()
    {
        v_sound = GameManager.Instance.getVSound();
        if (v_sound >= -20 && v_sound < 0)
        {
            v_sound += 2f;
            volumeSlider.value = v_sound;
            GameManager.Instance.v_AudioControl(v_sound);
        }
        else
            return;
    }
    #endregion

    #region BGM 슬라이더 바 조정
    public void MusicDown()
    {
        m_sound = GameManager.Instance.getMSound();
        if (m_sound <= 0 && m_sound > -20)
        {
            m_sound -= 2f;
            musicSlider.value = m_sound;
            GameManager.Instance.m_AudioControl(m_sound);
        }
        else
            return;
    }

    public void MusicUp()
    {
        m_sound = GameManager.Instance.getMSound();
        if (m_sound >= -20 && m_sound < 0)
        {
            m_sound += 2f;
            musicSlider.value = m_sound;
            GameManager.Instance.m_AudioControl(m_sound);
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
