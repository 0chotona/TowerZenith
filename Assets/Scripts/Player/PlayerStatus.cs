using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatus : MonoBehaviour
{
    [SerializeField] PlayerMove _playerMove;
    [SerializeField] PlayerHealth _playerHealth;
    [SerializeField] PlayerAttack _playerAttack;

    [SerializeField] InputController _inputController;
    [SerializeField] DamageBox _damageBox;


    float _baseHp = 200f;
    float _baseAtt = 20f;
    float _baseAttSpeed = 0f;
    float _baseDef = 15f;
    float _baseCriticalRate = 10f;
    float _baseCriticalDmg = 120f;
    float _baseMoveSpeed = 6f;
    float _baseJumpPower = 5f;

    public float _weaphonAtt;
    public float _weaphonAttSpeed;
    public float _weaphonDef;

    public float _hp;
    public float _att;
    public float _attSpeed;
    public float _def;
    public float _criticalRate;
    public float _criticalDmg;
    public float _moveSpeed;
    public float _jumpPower;
    

    public bool _hasBurn = false;
    public bool _hasFrozeCritical = false;
    public float _frozeCriticalRate = 0f;

    public bool _hasDrain = false;
    public float _drainDegree = 0f;

    public bool _hasFroze = false;
    public float _frozePercent = 0f;
    public float _frozeDur = 0f;

    public bool _hasStun = false;

    public float _trueDmg = 0f;

    public float _stunPercent = 0f;
    public float _stunDurTime = 0f;
    private void Awake()
    {
        ResetBuffStat();
        _attSpeed = 0f;
    }

    public void SetAtt(float att)
    {
        _att += att;
    }
    public void SetAttSpeed(float attSpeed) { _attSpeed += attSpeed; }
    public void SetDef(float def) 
    { 
        _def += def; 
    }
    public void SetCriticalRate(float rate) { _criticalRate += rate; }
    public void SetCriticalDmg(float dmg) { _criticalDmg += dmg; }
    public void SetMoveSpeed(float speed) 
    { 
        _moveSpeed += (_baseMoveSpeed * speed * 0.01f);
        _inputController._maxSpeed = _moveSpeed;
    }
    public void SetJumpPower(float power) 
    { 
        _jumpPower += (_baseJumpPower * power * 0.01f);
        _playerMove._jumpPower = _jumpPower;
    }
    public void ResetBuffStat()
    {
        _hp = _baseHp;
        _att = _baseAtt;
        _attSpeed = _baseAttSpeed;
        _def = _baseDef;
        _criticalDmg = _baseCriticalDmg;
        _criticalRate = _baseCriticalRate;

        _moveSpeed = _baseMoveSpeed;
        _jumpPower = _baseJumpPower;
        _inputController._maxSpeed = _moveSpeed;
        _playerMove._jumpPower = _jumpPower;

        _hasBurn = false;
        _hasFrozeCritical = false;
        _frozeCriticalRate = 0f;

        _hasDrain = false;
        _drainDegree = 0f;

        _hasFroze = false;
        _frozePercent = 0f;
        _frozeDur = 0f;

        _trueDmg = 0f;

        _stunPercent = 0f;
        _stunDurTime = 0f;

        _damageBox.ResetBuff();
    }
    public void ResetWeaphonStat()
    {
        /*
        _att -= _weaphonAtt;
        _attSpeed -= _weaphonAttSpeed;
        _def -= _weaphonDef;
        */
        _weaphonAtt = 0f;
        _weaphonAttSpeed = 0f;
        _weaphonDef = 0f;
    }
    public void SetWeaphonStat(float att, float attSpeed)
    {
        _weaphonAtt = att;
        _weaphonAttSpeed = attSpeed;
    }
    public void SetWeaphonStat(float att)
    {
        _weaphonAtt = att;
    }
    public void SetShieldStat(float def)
    {
        _weaphonDef = def;

    }
    public void AddWeaphonStat()
    {
        _att += _weaphonAtt;
        _attSpeed += _weaphonAttSpeed;
        _def += _weaphonDef;

        _playerAttack.SetAttackStat(_att, _attSpeed);
        _playerHealth.SetShieldDef(_def);

        UpdateUIStat();
        InGameUI._Instance.UpdateStatPanel();

    }
    public void AddSynergyStat()
    {
        UpdateUIStat();
        InGameUI._Instance.UpdateStatPanel();
    }
    void UpdateUIStat()
    {
        InGameUI._Instance._hp = _hp;
        InGameUI._Instance._att = _att;
        InGameUI._Instance._attSpeed = _attSpeed;
        InGameUI._Instance._def = _def;
        InGameUI._Instance._criticalRate = _criticalRate;
        InGameUI._Instance._criticalDmg = _criticalDmg;
        InGameUI._Instance._moveSpeed = _moveSpeed;
        InGameUI._Instance._jumpPower = _jumpPower;
    }
    public void SetDrainAttack(float degree)
    {
        _hasDrain = true;
        _drainDegree = degree;

        _damageBox._hasDrain = true;
        _damageBox.SetDrainAttack(_drainDegree);
    }
    public void SetStunAttack(float stunPercent, float stunDur)
    {
        _hasStun = true;
        _stunPercent = stunPercent;
        _stunDurTime = stunDur;

        _damageBox.SetStunAttack(_stunPercent, _stunDurTime);
    }
    public void SetFrozeAttack(float frozePercent, float frozeDur)
    {
        _hasFroze = true;
        _frozePercent = frozePercent;
        _frozeDur = frozeDur;

        _damageBox._hasFroze = true;

        _damageBox.SetFrozeAttack(_frozePercent, _frozeDur);
    }
    public void SetFrozeCritical(float criticalRate)
    {
        _hasFrozeCritical = true;
        _frozeCriticalRate = criticalRate;

        _damageBox._hasFrozeCritical = true;
        _damageBox.SetFrozeCritical(_frozeCriticalRate);
    }
    public void SetBurnAttack()
    {
        _damageBox._hasBurn = true;
    }
    public void SetTrueDamage(float damage)
    {
        _damageBox.SetTrueDamage(damage);
    }
}
