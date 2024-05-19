using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundEnemy : MonoBehaviour
{
    [SerializeField] AudioSource _impactAudio;
    [SerializeField] AudioSource _voiceAudio;
    /*
    [Header("���� �ȱ�"), SerializeField] AudioClip _bossWalkSound;
    [Header("���� ����"), SerializeField] AudioClip _bossJumpSound;
    [Header("���� ����"), SerializeField] AudioClip _bossLandSound;
    [Header("���� �⺻ ����"), SerializeField] AudioClip _bossAttackSound;
    [Header("���� ȭ�� ����"), SerializeField] AudioClip _bossFlameSound;
    */

    public void PlayImpactSound()
    {
        _impactAudio.Play();
    }
}
