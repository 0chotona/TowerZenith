using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class Ghost_Move : MonoBehaviour, IEnemyBehavior
{
    NavMeshAgent _navMesh;
    public Transform _playerTrs;
    [SerializeField] float _dist;
    [SerializeField] GameObject _damageBox;
    [SerializeField] Transform _spawnPos;
    float _coolTime = 3f;

    bool _isAttack = true;

    bool _isPassOut = false;
    float _passOutTime = 3f;

    float _initialOffset;

    CEnemy _cEnemy;

    float _passOutSpeed = 3f;

    EnemyHealth _enemyHealth;
    private void Start()
    {
        _navMesh = GetComponentInParent<NavMeshAgent>();
        _enemyHealth = GetComponent<EnemyHealth>();


        _enemyHealth.SetDefStat(_cEnemy._MaxHp, _cEnemy._Def);
        _navMesh.speed = _cEnemy._MoveSpeed;

        _initialOffset = _navMesh.baseOffset;
        StartCoroutine(CRT_Attack());
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
    }
    public void GetKnockBack(float knockbackRate)
    {

        //Vector3 targetPos = (transform.position - _playerTrs.position).normalized * knockbackRate;
        //targetPos.y = transform.position.y;
        //transform.position = targetPos;
    }
    public void SetPlayerTrs(Transform playerTrs)
    {
        _playerTrs = playerTrs;
    }
    public void Move()
    {

        if (!_isPassOut)
            _navMesh.SetDestination(_playerTrs.position);



        
    }
    void Attack()
    {
        GameObject damageBox = Instantiate(_damageBox, _spawnPos.position, Quaternion.identity);
        Ghost_Attack ghostAttack = damageBox.GetComponent<Ghost_Attack>();
        Vector3 shootDir = (_playerTrs.position - _spawnPos.position).normalized;
        ghostAttack.Shoot(shootDir);
        ghostAttack._dmg = _cEnemy._Att;
    }
    public void PassOut()
    {
        StartCoroutine(CRT_PassOut());
    }
    IEnumerator CRT_PassOut()
    {
        float targetOffset = 0f;
        float elapsedTime = 0f;
        float initialOffset = _navMesh.baseOffset;

        while (elapsedTime < 3f)
        {
            _navMesh.baseOffset = Mathf.Lerp(initialOffset, targetOffset, elapsedTime / 3f);
            elapsedTime += Time.deltaTime * _passOutSpeed;
            yield return null;
        }
        _navMesh.baseOffset = targetOffset;

        yield return new WaitForSeconds(_passOutTime);

        // Raising the base offset
        elapsedTime = 0f; // Reset elapsed time
        //targetOffset = _initialOffset; // Set target offset back to the initial value

        while (elapsedTime < 3f)
        {
            _navMesh.baseOffset = Mathf.Lerp(targetOffset, initialOffset, elapsedTime / 3f);
            elapsedTime += Time.deltaTime * _passOutSpeed;
            yield return null;
        }
        _navMesh.baseOffset = _initialOffset;
    }
    IEnumerator CRT_Attack()
    {
        while (true)
        {
            if(!_isPassOut)
                Attack();
            yield return new WaitForSeconds(_coolTime);
        }

    }
    public void GetWeakDamage(float damage)
    {
        _enemyHealth.GetDamage(damage);
        PassOut();
    }
}
