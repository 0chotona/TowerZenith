using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Anim_OneHandSword : MonoBehaviour
{
    Animator _anim;

    public int _activeIndex;
    public int _curCombo;
    public int _inputCombo;

    public bool _isPress = false;
    public bool _isFinalAttack = false;
    public bool _isAttack = false;

    EffectController _effectController;

    float _standardAttSpeed = 1f;

    bool _hasSlash = false;
    float _slashDamage = 0f;

    [Header("슬래쉬 프리펩"), SerializeField] GameObject _slashObj;
    [Header("슬래쉬 스폰"), SerializeField] Transform _slashSpawnTrs;


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
            case 3: _inputCombo = 4; break;
            //case 4: _inputCombo = 0; break;
        }
    }
    public void ActiveOneHandDamageBox(int activeIndex)
    {
        _activeIndex = activeIndex;
        if (activeIndex == 1)
        {
            _effectController.SetOneHandSlash(_curCombo);
            if (_hasSlash) //csv에 쿨타임,데미지(drgree) 추가, 쿨타임 적용(5초마다 슬래쉬 공격 가능)
                Shoot_Slash();


        }
    }
    public void PlaySwingSound()
    {
        _soundPlayer.PlaySwing();
    }
    public void SetOneHandCombo(int combo)
    {
        _curCombo = combo;
        _isAttack = true;

        _isPress = false;
        StopAllCoroutines();
        StartCoroutine(CRT_ComboTime());
    }
    public void OneHandAnim()
    {
        _anim.SetInteger("OneHandCombo", _inputCombo);
        _anim.SetInteger("CurCombo", _curCombo);
    }
    public void SetAttackSpeed(float attSpeed)
    {
        _anim.SetFloat("AttackSpeed", _standardAttSpeed * attSpeed / 10f); 
    }
    IEnumerator CRT_ComboTime()
    {
        yield return new WaitForSeconds(0.7f);
        if (!_isPress || _curCombo > 3)
        {
            
            _inputCombo = 0;
            _curCombo = 0;
            _isAttack = false;
            

        }
    }
    public void SetDash(int isFinalAttack)
    {
        _isFinalAttack = (isFinalAttack == 0) ? false : true;
    }
    public void ResetCombo() { _inputCombo = 0; }

    public void ResetBuff()
    {
        _hasSlash = false;
        _slashDamage = 0f;
    }
    public void SetSlash(float degree)
    {
        _hasSlash = true;
        _slashDamage = degree;
    }
    void Shoot_Slash()
    {
        GameObject slash = Instantiate(_slashObj, _slashSpawnTrs.position, _slashSpawnTrs.rotation);
        SlashBox slashBox = slash.GetComponent<SlashBox>();
        slashBox.SetDamage(_slashDamage);
        slashBox.Shoot(_slashSpawnTrs.forward);
        SoundActiveSkill._Instance.PlaySlash();
        Debug.Log(_slashSpawnTrs.forward);
    }
    public void PlaySpirit()
    {
        _voiceAudio.Play();
    }
}
