using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SynergyController : MonoBehaviour
{
    public List<int> _gottedItemID = new List<int>();

    [Header("시너지 데이터"), SerializeField] SynergyData _synergyData;

    Dictionary<eSYNERGYTYPE, CSynergy> _synergies = new Dictionary<eSYNERGYTYPE, CSynergy>();
    public int[] _synergyCounts;

    [SerializeField] PlayerHealth _playerHealth;
    [SerializeField] PlayerMove _playerMove;
    [SerializeField] PlayerStatus _status;
    [SerializeField] SpinAttack _spinAttack;

    [SerializeField] ActiveAttack _activeAttack;

    [SerializeField] DamageBox _damageBox;

    [SerializeField] Anim_OneHandSword _animOneHand;


    private void Start()
    {
        
        _synergies = _synergyData._Synergies;
        _synergyCounts = new int[_synergies.Count];
    }
    public void SetSynergyItem(List<int> gottedItemID)
    {
        _gottedItemID = gottedItemID;
        ResetCount();
        SetSynergyCount();
    }
    void SetSynergyCount()
    {
        foreach (var synergy in _synergies.Values)
        {
            foreach(var id in synergy._SynergyItems)
            {
                if (_gottedItemID.Contains(id))
                {
                    int index = Array.IndexOf(_synergies.Values.ToArray(), synergy);

                    if (index >= 0 && index < _synergyCounts.Length)
                    {
                        _synergyCounts[index]++;
                    }
                }
            }
            
        }
        int i = 0;
        foreach (var synergy in _synergies.Values)
        {
            if (synergy._ItemCount == _synergyCounts[i])
            {
                SetSynergy(synergy._synergyType);
            }
            i++;
        }
    }
    void ResetCount()
    {
        for(int i = 0; i < _synergyCounts.Length; i++)
            _synergyCounts[i] = 0;

        _spinAttack._isSynergy = false;
    }
    void SetSynergy(eSYNERGYTYPE synergyType) //한번 실행
    {
        switch(synergyType)
        {
            case eSYNERGYTYPE.Offensive_Increase:
                _status.SetAtt(_synergies[synergyType]._Degree);
                _status.SetDef(_synergies[synergyType]._Degree);
                break;
            case eSYNERGYTYPE.True_Damage:
                _damageBox.Synergy_TrueDamage(_synergies[synergyType]._Percent, _synergies[synergyType]._Degree);
                break;
            case eSYNERGYTYPE.Stun:
                _activeAttack.SetStun(_synergies[synergyType]._Degree, _synergies[synergyType]._DurTime, _synergies[synergyType]._CoolTime);
                //액티브
                break;
            case eSYNERGYTYPE.Sneak:
                _activeAttack.SetSneak(_synergies[synergyType]._DurTime, _synergies[synergyType]._CoolTime);
                //액티브
                break;
            case eSYNERGYTYPE.Spawn_Fireball:
                _spinAttack._isSynergy = true;
                _spinAttack.ResetFireBall();
                _spinAttack.SpawnFireBall();
                break;
            case eSYNERGYTYPE.Get_Speed_And_Jump:

                _status.SetMoveSpeed(_synergies[synergyType]._Degree);
                _status.SetJumpPower(_synergies[synergyType]._Degree);
                break;
            case eSYNERGYTYPE.Throw_Ice:
                //액티브
                break;
            case eSYNERGYTYPE.Shoot_Slash:
                _animOneHand.SetSlash(_synergies[synergyType]._Degree);
                break;
        }
        Debug.Log(synergyType.ToString());

        _activeAttack.ResetActiveSyenrgy();
        _activeAttack.SetActiveSynergy(synergyType);
        _status.AddSynergyStat();
    }
}
