using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEngine;

public class EffectController : MonoBehaviour
{
    [SerializeField] Transform _slashEffect;

    ParticleSystem _slashParticle;

    private void Awake()
    {
        _slashParticle = _slashEffect.GetComponent<ParticleSystem>();
        _slashParticle.Stop();
    }
    public void SetOneHandSlash(int combo)
    {
        switch(combo)
        {
            case 1:
                SetEffectRotation(new Vector3(0f, 0f, 45f));
                SetEffectScale(new Vector3(-1f, 1f, 1f));
                break;
            case 2:
                SetEffectRotation(new Vector3(0f, 0f, 45f));
                SetEffectScale(new Vector3(1f, 1f, 1f));
                break;
            case 3:
                SetEffectRotation(new Vector3(0f, 0f, 45f));
                SetEffectScale(new Vector3(-1f, 1f, 1f));
                break;
            case 4:
                SetEffectRotation(new Vector3(0f, 0f, -45f));
                SetEffectScale(new Vector3(1f, 1f, 1f));
                break;
        }
        _slashParticle.Play();
    }
    public void SetTwoHandSlash(int combo)
    {
        switch (combo)
        {
            case 1:
                SetEffectRotation(new Vector3(0f, 0f, 45f));
                SetEffectScale(new Vector3(-1.5f, 1.5f, 1.5f));
                break;
            case 2:
                SetEffectRotation(new Vector3(0f, 0f, -45f));
                SetEffectScale(new Vector3(1.5f, 1.5f, 1.5f));
                break;
            case 3:
                SetEffectRotation(new Vector3(0f, 0f, -45f));
                SetEffectScale(new Vector3(1.5f, 1.5f, 1.5f));
                break;
        }
        _slashParticle.Play();
    }
    void SetEffectRotation(Vector3 rot)
    {
        _slashEffect.localRotation = Quaternion.Euler(rot);
    }
    void SetEffectScale(Vector3 scale)
    {
        _slashEffect.localScale = scale;
    }
}
