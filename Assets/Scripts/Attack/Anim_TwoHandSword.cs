using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Anim_TwoHandSword : MonoBehaviour
{
    Animator _anim;

    public int _activeIndex;
    public int _curCombo;
    public int _inputCombo;

    bool _isPress = false;
    public bool _isFinalAttack = false;
    public bool _isAttack = false;

    EffectController _effectController;

    float _standardAttSpeed = 1f;

    [SerializeField] SoundPlayer _soundPlayer;
    [SerializeField] AudioSource _voiceAudio;
    private void Awake()
    {
        _anim = GetComponent<Animator>();
        _effectController = GetComponent<EffectController>();
    }
    public void UpdateCombo()
    {
        _isPress = true;
        switch (_curCombo)
        {
            case 0: _inputCombo = 1; break;
            case 1: _inputCombo = 2; break;
            case 2: _inputCombo = 3; break;
            case 3: _inputCombo = 0; break;
        }
    }
    public void ActiveTwoHandDamageBox(int activeIndex)
    {
        _activeIndex = activeIndex;
        if (activeIndex == 1)
        {
            _effectController.SetTwoHandSlash(_curCombo);
        }
    }
    public void SetTwoHandCombo(int combo)
    {
        _curCombo = combo;
        _isAttack = true;

        _isPress = false;
        StopAllCoroutines();
        StartCoroutine(CRT_ComboTime());
    }
    public void PlaySwingSound()
    {
        _soundPlayer.PlaySwing();
    }
    public void TwoHandAnim()
    {
        _anim.SetInteger("TwoHandCombo", _inputCombo);
        _anim.SetInteger("CurCombo", _curCombo);
    }
    public void SetAttackSpeed(float attSpeed)
    {
        _anim.SetFloat("AttackSpeed", _standardAttSpeed * attSpeed / 10f);
    }
    IEnumerator CRT_ComboTime()
    {
        yield return new WaitForSeconds(1f);
        if (!_isPress || _curCombo > 2)
        {
            _isAttack = false;
            _inputCombo = 0;
            _curCombo = 0;
        }
        else
            _isPress = false;
    }
    public void SetDash(int isFinalAttack)
    {
        _isFinalAttack = (isFinalAttack == 0) ? false : true;
    }
    public void ResetCombo() { _inputCombo = 0; }
    public void PlaySpirit()
    {
        _voiceAudio.Play();
    }
}
