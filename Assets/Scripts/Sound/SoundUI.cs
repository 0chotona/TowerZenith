using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundUI : MonoBehaviour
{
    AudioSource _source;

    [Header("√¢ ø≠±‚ "), SerializeField] AudioClip _clip_Window;
    [Header("æ∆¿Ã≈€ ¿Â¬¯ "), SerializeField] AudioClip _clip_Equip;
    [Header("æ∆¿Ã≈€ »πµÊ"), SerializeField] AudioClip _clip_Get;

    private void Awake()
    {
        _source = GetComponent<AudioSource>();
    }
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.M))
        {
            _source.PlayOneShot(_clip_Window);
        }
        if (Input.GetKeyDown(KeyCode.V))
        {
            _source.PlayOneShot(_clip_Window);
        }
    }
    public void PlayEquip()
    {
        _source.PlayOneShot(_clip_Equip);
    }
    public void PlayGet()
    {
        _source.PlayOneShot(_clip_Get);
    }
}
