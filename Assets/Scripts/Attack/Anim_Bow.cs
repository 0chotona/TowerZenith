using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Anim_Bow : MonoBehaviour
{
    Animator _anim;

    [SerializeField] SoundPlayer _soundPlayer;

    private void Awake()
    {
        _anim = GetComponent<Animator>();
    }
    public void SetIsAiming(bool isAiming)
    {
        _anim.SetBool("IsAiming", isAiming);
    }
    public void SetShoot()
    {
        _anim.SetTrigger("Shoot");
        _soundPlayer.PlayShoot();
    }
    public void SetIsBow(bool isBow)
    {
        _anim.SetBool("IsBow", isBow);
    }
    public void PlayAimSound()
    {
        _soundPlayer.PlayDraw();
    }
}
