using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Knight_Move : MonoBehaviour, IEnemyBehavior
{
    NavMeshAgent _navMesh;
    public Transform _playerTrs;
    [SerializeField] float _dist;
    float _coolTime = 3f;

    public bool _lockOn = true;
    bool _isAttack = true;

    EnemyAnimation _anim;

    CEnemy _cEnemy;

    EnemyAttack _enemyAttack;
    EnemyHealth _enemyHealth;

    private void Start()
    {
        _anim = GetComponentInChildren<EnemyAnimation>();
        _navMesh = GetComponent<NavMeshAgent>();
        _enemyAttack = GetComponentInChildren<EnemyAttack>();
        _enemyHealth = GetComponent<EnemyHealth>();

        //_cEnemy = new CEnemy("πÊ≈¡ ±‚ªÁ", 100, 20, 10, 3.5f);

        _enemyHealth.SetDefStat(_cEnemy._MaxHp, _cEnemy._Def);
        _navMesh.speed = _cEnemy._MoveSpeed;
        _enemyAttack.SetDamage(_cEnemy._Att);

        _enemyAttack.SetActive(false);
        
        //StartCoroutine(CRT_Attack());
    }
    private void Update()
    {
        if (_playerTrs != null)
            _dist = Vector3.Distance(_playerTrs.position, transform.position);
        //Move();
        //Rotate();


    }
    public void SetStat(CEnemy stat)
    {
        _cEnemy = stat;
    }
    public void Initialize(Transform playerTransform, EnemyAnimation enemyAnimation)
    {
        _playerTrs = playerTransform;
        _anim = enemyAnimation;
    }
    public void GetKnockBack(float knockbackRate)
    {
        if(!_anim._isAttack)
        {
            Vector3 targetPos = (transform.position - _playerTrs.position).normalized * knockbackRate;
            targetPos.y = transform.position.y;
            transform.position += targetPos;

        }
        
    }
    public void SetPlayerTrs(Transform playerTrs)
    {
        _playerTrs = playerTrs;
    }
    public void Move()
    {
        _lockOn = (_dist < 2f) ? true : false;

        if (!_lockOn && !_anim._isAttack)
            _navMesh.SetDestination(_playerTrs.position);


        if (_lockOn && !_isAttack)
        {
            StartCoroutine(CRT_Attack());
            _isAttack = true;
        }

        if (!_lockOn)
        {
            StopCoroutine(CRT_Attack());
            _isAttack = false;
        }

        if (_lockOn && !_anim._isAttack)
        {
            Vector3 dirToPlayer = (_playerTrs.position - transform.position).normalized;

            Quaternion targetRot = Quaternion.LookRotation(dirToPlayer);
            float rotateSpeed = 180; 
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRot, rotateSpeed * Time.deltaTime);

        }
        _anim.SetMoveAnimation(!_lockOn);
    }
    void Attack()
    {
        _anim.SetAttack();
        //transform.rotation = Quaternion.LookRotation(_playerTrs.position);
    }
    IEnumerator CRT_Attack()
    {
        while (_lockOn)
        {

            Attack();
            yield return new WaitForSeconds(_coolTime);
        }

    }
    public void GetWeakDamage(float damage)
    {
        _enemyHealth.GetDamage(damage);
    }
}
