using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CEnemy
{
    string _name;
    public string _Name => _name;

    int _maxHp;
    public int _MaxHp => _maxHp;

    float _att;
    public float _Att => _att;

    int _def;
    public int _Def => _def;

    float _moveSpeed;
    public float _MoveSpeed => _moveSpeed;
    public CEnemy(string name, int maxHp, float att, int def, float moveSpeed)
    {
        _name = name;
        _maxHp = maxHp;
        _att = att;
        _def = def;
        _moveSpeed = moveSpeed;
    }
    
}
