using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BuffController : MonoBehaviour
{
    [SerializeField] BuffData _buffData;
    [SerializeField] PlayerHealth _playerHealth;
    [SerializeField] PlayerMove _playerMove;

    [SerializeField] PlayerStatus _status;

    Dictionary<BUFFTYPE, Buff> _buffs = new Dictionary<BUFFTYPE, Buff>();

    List<Buff> _gotBuffs = new List<Buff>();

    [SerializeField] DamageBox _damageBox;

    [SerializeField] SpinAttack _spinAttack;

    private void Start()
    {
        _buffs = _buffData._Buffs;
    }
    public void SetBuff(BUFFTYPE buffType)
    {
        
        switch (buffType)
        {
            case BUFFTYPE.None:
                break;
            case BUFFTYPE.Burn:
                _status.SetBurnAttack();
                break;
            case BUFFTYPE.Frost_Critical:
                _status.SetFrozeCritical(_buffs[buffType]._Rate);
                break;
            case BUFFTYPE.Critical_Rate:
                _status.SetCriticalRate(_buffs[buffType]._Degree);
                //_damageBox.SetCritical(_buffs[buffType]._Degree, 0);
                break;
            case BUFFTYPE.Deffence_Increase:
                _status.SetDef(_buffs[buffType]._Degree);
                //_playerHealth.SetBuffDef(_buffs[buffType]._Degree);
                break;
            case BUFFTYPE.Drain:
                _status.SetDrainAttack(_buffs[buffType]._Degree);
                break;
            case BUFFTYPE.Offensive_Increase:
                //_damageBox.SetBuffAtt(_buffs[buffType]._Degree);
                _status.SetAtt(_buffs[buffType]._Degree);
                break;
            case BUFFTYPE.True_Damage:
                _status.SetTrueDamage(_buffs[buffType]._Degree);
                //_damageBox.SetTrueDamage(_buffs[buffType]._Degree);
                break;
            case BUFFTYPE.Stun:
                _status.SetStunAttack(_buffs[buffType]._Percent, _buffs[buffType]._DurTime);
                //_damageBox.SetStunAttack(_buffs[buffType]._Percent, _buffs[buffType]._Degree);
                break;
            case BUFFTYPE.Critical_Damage:
                //_damageBox.SetCritical(0, _buffs[buffType]._Degree);
                _status.SetCriticalDmg(_buffs[buffType]._Degree);
                break;
            case BUFFTYPE.Spawn_Fireball:
                _spinAttack.SpawnFireBall();
                break;
            case BUFFTYPE.Get_Speed_And_Jump:
                _status.SetMoveSpeed(_buffs[buffType]._Degree);
                _status.SetJumpPower(_buffs[buffType]._Degree);
                //_playerMove.SetSpeedAndJump(_buffs[buffType]._Degree, _buffs[buffType]._Degree);
                break;
            case BUFFTYPE.Frost: 
                _status.SetFrozeAttack(_buffs[buffType]._Percent, _buffs[buffType]._DurTime);
                break;
        }
        _status.AddSynergyStat();
    }
    public void ResetBuff()
    {
       

        _playerHealth.SetBuffDef(0);

        
        _spinAttack.ResetFireBall();
        _playerMove.SetSpeedAndJump(0, 0);
        _damageBox.ResetBuff();
        
    }
}
