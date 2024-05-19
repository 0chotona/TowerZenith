using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zoroark_Effect : MonoBehaviour
{
    [SerializeField] Transform _slashEffect;
    [SerializeField] Transform _jumpEffect;

    ParticleSystem _slashParticle;
    ParticleSystem _jumpParticle;

    private void Awake()
    {
        _slashParticle = _slashEffect.GetComponent<ParticleSystem>();
        _jumpParticle = _jumpEffect.GetComponent<ParticleSystem>();

        _jumpEffect.SetParent(null);

        _slashParticle.Stop();
        _jumpParticle.Stop();
    }
    public void SetAttackSlash(int combo)
    {
        switch (combo)
        {
            case 1:
                SetEffectRotation(new Vector3(0f, 0f, -45f));
                SetEffectScale(new Vector3(3f, 3f, 3f));
                break;
            case 2:
                SetEffectRotation(new Vector3(0f, 0f, 45f));
                SetEffectScale(new Vector3(-3f, 3f, 3f));
                break;
        }
        _slashParticle.Play();
    }
    public void SetJumpSlash(Vector3 position)
    {
        _jumpEffect.position = position;
        _jumpParticle.Play();
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
