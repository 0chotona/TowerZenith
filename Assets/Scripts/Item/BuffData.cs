using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum BUFFTYPE
{
    None,
    Burn,
    Frost_Critical,
    Critical_Rate,
    Deffence_Increase,
    Drain,
    Offensive_Increase,
    True_Damage,
    Stun,
    Critical_Damage,
    Spawn_Fireball,
    Get_Speed_And_Jump,
    Frost
}
public class Buff
{
    BUFFTYPE _buffType;
    public BUFFTYPE _BuffType => _buffType;

    public int _ID { get; set; }
    public string _Buff_Name { get; set; }
    public float _Degree { get; set; }

    public float _Rate { get; set; }

    public float _Percent { get; set; }

    public float _DurTime { get; set; }

    public float _CoolTime { get; set; }

    public int _ItemID { get; set; }
    public Buff(BUFFTYPE buffType, float degree, float rate, float percent, float durTime, float coolTime)
    {
        _buffType = buffType;
        _Degree = degree;
        _Rate = rate;
        _Percent = percent;
        _DurTime = durTime;
        _CoolTime = coolTime;
    }
    public Buff(BUFFTYPE buffType)
    {
        _buffType = buffType;
    }
}
public class BuffData : MonoBehaviour
{
    Dictionary<BUFFTYPE, Buff> _buffs = new Dictionary<BUFFTYPE, Buff>();
    public Dictionary<BUFFTYPE,Buff> _Buffs => _buffs;


    List<string> _buffList = null;
    private void Awake()
    {
        /*
        foreach(BUFFTYPE buffType in Enum.GetValues(typeof(BUFFTYPE)))
            SetBuff(buffType);
        */
        _buffs.Add(BUFFTYPE.None,new Buff(BUFFTYPE.None));
    }
    public void AddBuffToList(Buff buff)
    {
        if(buff._BuffType != BUFFTYPE.None)
            _buffs.Add(buff._BuffType, buff);
    }
    
    
}
