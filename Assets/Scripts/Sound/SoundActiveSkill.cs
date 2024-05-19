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

    [Header("�¾� ����"), SerializeField] AudioSource _sunSound;
    [Header("�׸���"), SerializeField] AudioSource _invisibleSound;
    [Header("ȭ����"), SerializeField] AudioSource _fireBallSound;
    [Header("�˱�"), SerializeField] AudioSource _slashSound;

    
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
