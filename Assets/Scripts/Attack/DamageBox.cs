using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DamageBox : MonoBehaviour
{
    public float _buffDamage = 0f;
    float _damage;
    Vector3 _scale;
    float _criticalBaseRate = 10f;
    public float _criticalIncreaseRate = 0f;
    float _criticalBaseDamage = 1.5f;
    public float _criticalIncreaseDamage = 0f;

    float _knockbackRate;

    Vector3 _baseScale = new Vector3(2, 0.1f, 2f);

    public bool _hasBurn = false;
    public bool _hasFroze = false;
    public bool _hasFrozeCritical = false;

    public float _frozeCriticalRate = 0;

    public float _frozePercent;
    public float _frozeDur;

    public bool _hasDrain = false;
    float _drainDegree = 0f;

    float _trueDamage = 0f;
    float _rndTrueDamage = 0f;
    bool _hasTrueDamageSynergy = false;
    float _trueDamagePercent = 0f;
    float _trueDamageDegree = 0f;

    float _stunPercent = 0f;
    float _stunDurTime = 0f;

    

    [SerializeField] PlayerHealth _playerHealth;
    private void Awake()
    {
        transform.localScale = _baseScale;
    }
    public void SetDamage(float damage)
    {
        _damage = damage;
    }
    public void SetScale(Vector3 scale)
    {
        _scale = scale;
        transform.localScale = _scale;
    }
    public void SetCritical(float rate, float damage)
    {
        _criticalIncreaseRate += rate;
        _criticalIncreaseDamage += damage;
    }
    public void SetKnockBack(float knockbackRate)
    {
        _knockbackRate = knockbackRate;
    }
    public void ResetBuff()
    {
        _hasBurn = false;
        _hasFrozeCritical = false;
        _frozeCriticalRate = 0;

        _criticalIncreaseRate = 0;
        _criticalIncreaseDamage = 0;

        _hasDrain = true;

        _buffDamage = 0;


        _trueDamage = 0;
        _hasTrueDamageSynergy = false;

        _stunPercent = 0;
        _stunDurTime = 0;

        _frozePercent = 0;
        _frozeDur = 0;
        _hasFroze = false;

        
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Enemy"))
        {
            EnemyHealth enemyHealth = other.GetComponent<EnemyHealth>();

            if (_hasBurn)
                enemyHealth._hasBurn = true;

            EnemyMove enemyMove = other.GetComponent<EnemyMove>();
            enemyMove.GetKnockBack(_knockbackRate);

            EnemyAnimation enemyAnim = other.GetComponentInChildren<EnemyAnimation>();

            float damage = _damage + _buffDamage;
            bool shouldFreeze = false;

            

            if(enemyMove._isFrozed)
            {
                if(_hasFrozeCritical)
                {
                    
                    damage *= _frozeCriticalRate;
                    enemyMove._isFrozed = false;
                    enemyAnim._isFrozed = false;
                    shouldFreeze = true;
                }
            }
            else if (IsLottery((_criticalBaseRate + _criticalIncreaseRate) / 100))
            {
                damage *= (_criticalBaseDamage + (_criticalIncreaseDamage / 100));
                if (_hasDrain)
                    _playerHealth.GetHeal(_drainDegree);
            }

            if (_hasFroze && !shouldFreeze)
            {
                float rnd = Random.value;
                if (rnd < _frozePercent * 0.01)
                {
                    Debug.Log("¾ó·ÁÁü");
                    if (!enemyMove._isFrozed)
                    {
                        enemyMove.GetFroze(_frozeDur);
                        enemyAnim.SetFrozedAnimation(_frozeDur);
                    }
                }
            }

            if(_hasTrueDamageSynergy)
                _rndTrueDamage = IsLottery(_trueDamagePercent / 100) ? _trueDamageDegree : 0;

            enemyHealth.GetDamage(damage + _trueDamage + _rndTrueDamage);
            
            
            if (_stunPercent > 0)
            {
                if(IsLottery(_stunPercent / 100))
                {
                    if (enemyMove != null)
                        enemyMove.GetStun(_stunDurTime);
                    if (enemyAnim != null)
                        enemyAnim.SetFrozedAnimation(_stunDurTime);
                }
            }

            
        }
    }
    public void SetDrainAttack(float degree)
    {
        _hasDrain = true;
        _drainDegree = degree;
    }
    public void SetFrozeCritical(float rate)
    {
        _frozeCriticalRate = rate;
    }
    public void SetBuffAtt(float damage)
    {
        _buffDamage += damage;
    }
    public void SetTrueDamage(float damage)
    {
        _trueDamage += damage;
    }
    public void SetStunAttack(float percent, float durTime)
    {
        
        _stunPercent = percent;
        _stunDurTime = durTime;
    }
    public void SetFrozeAttack(float percent, float durTime)
    {
        _hasFroze = true;
        _frozePercent = percent;
        _frozeDur = durTime;
    }
    bool IsLottery(float value)
    {
        float rnd = Random.value;
        if (rnd < value)
            return true;
        else
            return false;
    }

    public void Synergy_TrueDamage(float percent, float degree)
    {
        _hasTrueDamageSynergy = true;
        _trueDamagePercent = percent;
        _trueDamageDegree = degree;
    }

    
}
