using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyAnimation : MonoBehaviour
{

    public bool _isAttack = false;
    public int _attIndex = 0;
    public float _damage;
    public bool _isFrozed = false;

    public abstract void SetAttack();
    public abstract void SetMoveAnimation(bool isMove);
    public abstract void SetFrozedAnimation(float durTime);
    public abstract void SetDead(GameObject destroyEffect,GameObject enemyObj);

    public abstract IEnumerator CRT_AttackAnim();
    
    
}
