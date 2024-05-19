using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundPlayer : MonoBehaviour
{
    [Header("걷는 소리"), SerializeField] AudioClip _walkSound;
    [Header("기합 소리"), SerializeField] AudioClip _spiritSound;
    [Header("휘두르는 소리"), SerializeField] AudioClip _swingSound;
    [Header("활 시위 소리"), SerializeField] AudioClip _drawSound;
    [Header("활 발사 소리"), SerializeField] AudioClip _shootSound;

    [SerializeField] AudioSource _source;

    bool _isWalking = false;
    public bool _isPlaying => _source.isPlaying;

    private void Awake()
    {
        _source = GetComponent<AudioSource>();
        _source.Stop();
    }
    private void Update()
    {
        /*
        if (Input.GetKeyDown(KeyCode.Alpha1))
            PlayWalk(false);
        if (Input.GetKeyDown(KeyCode.Alpha2))
            PlaySpirit();
        if (Input.GetKeyDown(KeyCode.Alpha3))
            PlaySwing();
        if (Input.GetKeyDown(KeyCode.Alpha4))
            PlayDraw();
        if (Input.GetKeyDown(KeyCode.Alpha5))
            PlayShoot();
        */
        
    }
    public void PlayWalk(bool isPlay)
    {
        _isWalking = true;
        _source.loop = isPlay;
        if (isPlay)
            _source.Play();
        else if(!_isWalking)
            _source.Stop();
    }
    public void PlaySpirit()
    {
        _isWalking = false;
        _source.loop = false;
        _source.Stop();
        _source.PlayOneShot(_spiritSound);
    }
    public void PlaySwing()
    {
        _isWalking = false;
        _source.loop = false;
        _source.Stop();
        _source.PlayOneShot(_swingSound);
    }
    public void PlayDraw()
    {
        _isWalking = false;
        _source.loop = false;
        _source.Stop();
        _source.PlayOneShot(_drawSound);

    }
    public void PlayShoot()
    {
        _isWalking = false;
        _source.loop = false;
        _source.Stop();
        _source.PlayOneShot(_shootSound);
    }
    public void StopSource()
    {
        _source.Stop();
    }
}
