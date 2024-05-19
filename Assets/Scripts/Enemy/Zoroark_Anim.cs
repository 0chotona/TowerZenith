using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zoroark_Anim : EnemyAnimation
{
    [SerializeField] GameObject _damageBox;
    [Header("불꽃"), SerializeField] GameObject _damageBall;
    [Header("점프 박스"), SerializeField] GameObject _jumpDamageBox;
    GameObject _destroyEffect;
    Animator _anim;

    [SerializeField] float _min = 0;
    [SerializeField] float _max = 0.5f;
    bool _isRun = false;

    Zoroark_Effect _effectController;

    [SerializeField] Transform _bulletSpawnPos;

    [SerializeField] SoundBoss _sound;

    GameObject _tmpObj;
    int _fireCount = 9;
    int _attCount = 0;
    public int _AttCount => _attCount;
    public int _nextIndex = 0;

    int _curCombo = 0;

    private void Awake()
    {
        _isAttack = false;
        _anim = GetComponent<Animator>();
        _effectController = GetComponent<Zoroark_Effect>();
    }
    public override IEnumerator CRT_AttackAnim()
    {
        SetAttack();
        _isAttack = true;
        yield return new WaitForSeconds(2f);
        _isAttack = false;

    }

    public override void SetAttack()
    {
        SetMoveAnimation(false);
        _anim.SetInteger("AttIndex", _attIndex);
    }
    public override void SetMoveAnimation(bool isMove)
    {
        _anim.SetBool("IsMove", isMove);
    }
    public void ActiveDamageBox(int active)
    {
        EnemyAttack enemyAttack = _damageBox.GetComponent<EnemyAttack>();
        enemyAttack.SetDamage(_damage * 1.5f);
        bool isActive = (active == 1) ? true : false;
        _damageBox.SetActive(isActive);
        if (isActive)
        {
            _effectController.SetAttackSlash(_curCombo);
            _sound.PlayAttackSound();
        }
    }
    public void SetCurCombo(int combo)
    {
        _curCombo = combo;
    }
    public void ActiveJumpDamageBox(int active)
    {
        EnemyAttack enemyAttack = _jumpDamageBox.GetComponent<EnemyAttack>();
        enemyAttack.SetDamage(_damage * 2f);
        bool isActive = (active == 1) ? true : false;
        _jumpDamageBox.SetActive(isActive);
        
            
    }
    public void SetIsAttack(int attack)
    {
        
        _isAttack = (attack == 1) ? true : false;
        if (!_isAttack)
        {
            _attIndex = 0;
            _anim.SetInteger("AttIndex", _attIndex);
            //SetMoveAnimation(false);
        }
    }
    public void SetAttCount()
    {
        _attCount++;
        if (_attCount > 3)
            _attCount = 1;
    }
    public void Attack2()
    {
        StartCoroutine(CRT_Attack2());
    }
    IEnumerator CRT_Attack2()
    {
        _sound.PlayFlameSound();
        for (int i = 0; i < _fireCount; i++)
        {
            float randomAngle = Random.Range(_min, _max);


            Quaternion rotation = Quaternion.Euler(0f, randomAngle, 0f);
            Vector3 spawnDirection = _bulletSpawnPos.forward;
            spawnDirection.y = randomAngle;
            GameObject bullet = Instantiate(_damageBall, _bulletSpawnPos.position, Quaternion.identity);
            Zoroark_Attack damageBallScript = bullet.GetComponent<Zoroark_Attack>();
            damageBallScript._dmg = _damage;
            damageBallScript.Shoot(spawnDirection);
            yield return new WaitForSeconds(0.3f);
        }
    }
    public override void SetFrozedAnimation(float durTime)
    {
        if (_anim.speed != 0f)
            StartCoroutine(CRT_GetFroze(durTime));
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
        Instantiate(_destroyEffect,transform.position,Quaternion.identity);
        Destroy(_tmpObj);
    }
}