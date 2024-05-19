using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundEnemy : MonoBehaviour
{
    [SerializeField] AudioSource _impactAudio;
    [SerializeField] AudioSource _voiceAudio;
    /*
    [Header("보스 걷기"), SerializeField] AudioClip _bossWalkSound;
    [Header("보스 점프"), SerializeField] AudioClip _bossJumpSound;
    [Header("보스 착지"), SerializeField] AudioClip _bossLandSound;
    [Header("보스 기본 공격"), SerializeField] AudioClip _bossAttackSound;
    [Header("보스 화염 공격"), SerializeField] AudioClip _bossFlameSound;
    */

    public void PlayImpactSound()
    {
        _impactAudio.Play();
    }
}
