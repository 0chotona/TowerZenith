using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knight_Anim : EnemyAnimation
{

    [SerializeField] GameObject _damageBox;
    GameObject _destroyEffect;
    GameObject _tmpObj;
    Animator _anim;
    private void Awake()
    {
        _anim = GetComponent<Animator>();
        //_anim.speed = 1.0f;언거 구현
    }

    public override void SetAttack()
    {
        if (!_isAttack)
        {
            _anim.SetTrigger("IsAttack");
            StartCoroutine(CRT_AttackAnim());
        }

    }
    public override void SetMoveAnimation(bool isMove)
    {
        _anim.SetBool("IsMove", isMove);
    }
    public void SetStun()
    {
        _anim.SetTrigger("FallDown");
    }
    public override IEnumerator CRT_AttackAnim()
    {
        _isAttack = true;
        yield return new WaitForSeconds(2f);
        _isAttack = false;
    }
    public void ActiveDamageBox(int active)
    {
        bool isActive = (active == 1) ? true : false;
        _damageBox.SetActive(isActive);
    }
    public void SetIsAttack(int attack)
    {
        _isAttack = (attack == 1) ? true : false;
    }

    public override void SetFrozedAnimation(float durTime)
    {
        if(_anim.speed != 0f)
            StartCoroutine(CRT_GetFroze(5));
    }
    IEnumerator CRT_GetFroze(float durTime)
    {
        _anim.speed = 0f;
        yield return new WaitForSeconds(durTime);
        _anim.speed = 1f;
    }

    public override void SetDead(GameObject destroyEffect, GameObject enemyObj)
    {
        _destroyEffect = destroyEffect;
        _tmpObj = enemyObj;
        _anim.SetTrigger("IsDead");
    }
    public void DestroyEffect()
    {
        Instantiate(_destroyEffect, transform.position, Quaternion.identity);
        Destroy(_tmpObj);
    }
}
