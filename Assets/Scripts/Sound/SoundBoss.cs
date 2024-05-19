using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundBoss : MonoBehaviour
{

    [SerializeField] AudioSource _walkAudio;
    [SerializeField] AudioSource _jumpAudio;
    [SerializeField] AudioSource _landAudio;
    [SerializeField] AudioSource _attackAudio;
    [SerializeField] AudioSource _flameAudio;

    public bool _IsPlaying => _walkAudio.isPlaying;

    public void PlayWalkSound(bool isWalk)
    {
        if (isWalk)
            _walkAudio.Play();
        else
            _walkAudio.Stop();
    }
    public void PlayJumpSound()
    {
        _jumpAudio.Play();
    }
    public void PlayLandSound()
    {
        _landAudio.Play();
    }
    public void PlayAttackSound()
    {
        _attackAudio.Play();
    }
    public void PlayFlameSound()
    {
        _flameAudio.Play();
    }
}
