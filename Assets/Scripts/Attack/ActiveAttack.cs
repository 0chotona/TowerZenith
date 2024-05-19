using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ActiveAttack : MonoBehaviour
{
    eSYNERGYTYPE _synergyType;

    [SerializeField] PlayerInvisible _playerInvisible;
    List<GameObject> _nearEnemies = new List<GameObject>();
    [SerializeField] ThrowIceSpear _throwIceSpear;


    float _stunDamage = 0f;
    float _stunDur = 0f;
    float _stunCool = 0f;
    bool _canStun = true;

    float _sneakDur = 0f;
    float _sneakCool = 0f;
    bool _canSneak = true;

    bool _canThrowIce;
    float _iceDamage = 0f;//얼음창 던지는거 구현, 인스펙터 할당 필요
    float _iceCool = 0f;

    [SerializeField] Attack_Bow _attack_Bow;


    private void Awake()
    {
        _synergyType = eSYNERGYTYPE.None;
    }
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Q))
        {
            if(_synergyType != eSYNERGYTYPE.None)
            {
                Action_DownActive(_synergyType);
            }
        }
        if(Input.GetKey(KeyCode.Q))
        {
            if (_synergyType != eSYNERGYTYPE.None)
            {
                Action_StayActive(_synergyType);
            }
        }
        if (Input.GetKeyUp(KeyCode.Q))
        {
            if (_synergyType != eSYNERGYTYPE.None)
            {
                Action_UpActive(_synergyType);
            }
        }
    }
    public void SetActiveSynergy(eSYNERGYTYPE type)
    {
        _synergyType = type;
    }
    public void ResetActiveSyenrgy()
    {
        _synergyType = eSYNERGYTYPE.None;
    }
    void Action_DownActive(eSYNERGYTYPE type)
    {
        switch(type)
        {
            case eSYNERGYTYPE.None:
                break;
            case eSYNERGYTYPE.Stun:
                if(_canStun)
                {
                    StartCoroutine(CRT_Stun());
                    _attack_Bow._isShootFire = true;
                    _attack_Bow.SetSunStat(_stunDamage, _stunDur);
                }
                //액티브
                break;
            case eSYNERGYTYPE.Sneak:
                if(_canSneak)
                {
                    StartCoroutine(CRT_Sneak());
                    _playerInvisible.Sneak(_sneakDur);
                    SoundActiveSkill._Instance.PlayInvisible();
                }
                break;
                /*
            case eSYNERGYTYPE.Throw_Ice:
                if(_canThrowIce)
                {
                    _throwIceSpear.SpawnSpear();
                }
                break;
                */
        }
    }
    void Action_StayActive(eSYNERGYTYPE type)
    {
        switch (type)
        {
            case eSYNERGYTYPE.None:
                break;
            case eSYNERGYTYPE.Throw_Ice:
                if (_canThrowIce)
                {
                    _throwIceSpear.AimingSpear();
                }
                break;
        }
    }
    void Action_UpActive(eSYNERGYTYPE type)
    {
        switch (type)
        {
            case eSYNERGYTYPE.None:
                break;
                /*
            case eSYNERGYTYPE.Throw_Ice:
                if (_canThrowIce)
                {
                    StartCoroutine(CRT_Throw_Ice());
                    _throwIceSpear.PlayThrowAnim();
                }
                break;
                */
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Enemy"))
        {
            _nearEnemies.Add(other.gameObject);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            _nearEnemies.Remove(other.gameObject);
        }
    }
    public void SetStun(float degree, float durTime, float coolTime)
    {
        _stunDamage = degree;
        _stunDur = durTime;
        _stunCool = coolTime;
    }
    IEnumerator CRT_Stun()
    {
        _canStun = false;
        yield return new WaitForSeconds(_stunCool);
        _canStun = true;
    }
    public void SetSneak(float durTime, float coolTime)
    {
        _sneakDur = durTime;
        _sneakCool = coolTime;
    }
    IEnumerator CRT_Sneak()
    {
        _canSneak = false;
        yield return new WaitForSeconds(_sneakCool);
        _canSneak = true;
    }
    public void SetThrowIce(float degree, float coolTime)
    {
        _iceDamage = degree;
        _iceCool = coolTime;
    }
    IEnumerator CRT_Throw_Ice()
    {
        _canThrowIce = false;
        yield return new WaitForSeconds(_iceCool);
        _canThrowIce = true;
    }
}
