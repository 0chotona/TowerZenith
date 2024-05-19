using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundActiveSkill : MonoBehaviour
{
    static SoundActiveSkill _instance;
    public static SoundActiveSkill _Instance
    {
        get
        {
            if (_instance == null)
                _instance = FindObjectOfType<SoundActiveSkill>();
            return _instance;
        }
    }

    [Header("태양 폭발"), SerializeField] AudioSource _sunSound;
    [Header("그림자"), SerializeField] AudioSource _invisibleSound;
    [Header("화염구"), SerializeField] AudioSource _fireBallSound;
    [Header("검기"), SerializeField] AudioSource _slashSound;

    
    public void PlayInvisible()
    {
        _invisibleSound.Play();
    }
    public void PlaySun()
    {
        if(!_sunSound.isPlaying)
            _sunSound.Play();
    }
    public void PlayFireBall(bool isPlay)
    {
        if (isPlay)
            _fireBallSound.Play();
        else
            _fireBallSound.Stop();
    }
    public void PlaySlash()
    {
        _slashSound.Play();
    }
}
