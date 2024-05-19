using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class Zoroark_Move : MonoBehaviour, IEnemyBehavior
{
    NavMeshAgent _navMesh;
    public Transform _playerTrs;
    [SerializeField] float _dist = 10f;
    [SerializeField] float _farDist = 20;

    
    float _lockOnDist = 5f;

    float _coolTime = 4f;

    public bool _lockOn = true;
    public bool _canAttack = true;
    public bool _isAttack2 = false;

    EnemyAnimation _anim;

    CEnemy _cEnemy;

    EnemyHealth _enemyHealth;

    Zoroark_Sensor _sensor;

    public float _jumpHeight = 20;
    public float _moveDur = 1.5f;
    public float _jumpDur = 1f;

    int _attIndex = 1;
    public int _attCount = 0;
    int _maxCount = 3;

    bool _isFar = false;

    Vector3 _initialPos;
    Vector3 _targetPos;

    Zoroark_Effect _effectController;

    CameraMove _camMove;
    [SerializeField] SoundBoss _sound;
    private void Start()
    {
        _anim = GetComponentInChildren<EnemyAnimation>();
        _navMesh = GetComponent<NavMeshAgent>();
        _enemyHealth = GetComponent<EnemyHealth>();
        _effectController = GetComponentInChildren<Zoroark_Effect>();

        _enemyHealth._name = _cEnemy._Name;
        _enemyHealth.SetDefStat(_cEnemy._MaxHp, _cEnemy._Def);

        _anim._damage = _cEnemy._Att;

        _navMesh.speed = _cEnemy._MoveSpeed;

        _sensor = GetComponentInChildren<Zoroark_Sensor>();
        //StartCoroutine(CRT_Attack());
        //StartCoroutine(CRT_Attack());
    }
    private void Update()
    {
        //Move();
        
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
        _dist = Vector3.Distance(_playerTrs.position, transform.position);
        _isFar = (_dist < _farDist) ? false : true;

        if (_attCount < 3 && _canAttack && !(_anim._attIndex == 3) && !_anim._isAttack)
        {
            _navMesh.enabled = true;
            if (_navMesh != null)
            {
                _navMesh.isStopped = false;
                _navMesh.SetDestination(_playerTrs.position);
                _anim.SetMoveAnimation(true);
                if(!_sound._IsPlaying)
                    _sound.PlayWalkSound(true);
            }

        }
        else
        {
            _sound.PlayWalkSound(false);
            _navMesh.enabled = false;
        }
        //_navMesh.isStopped = true;


        if (!_canAttack)
        {
            Vector3 dirToPlayer = (_playerTrs.position - transform.position).normalized;
            dirToPlayer.y = 0;
            Quaternion targetRot = Quaternion.LookRotation(dirToPlayer);
            
            float rotateSpeed = 180;
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRot, rotateSpeed * Time.deltaTime);
        }
        if(_attCount < 3 && _canAttack && _isFar && !_anim._isAttack)
        {
            StartCoroutine(CRT_Attack()); 
            _canAttack = false;
            _anim._attIndex = 3;
            Attack();
        }
        if(_attCount > 2 && _canAttack && !_anim._isAttack)
        {
            StartCoroutine(CRT_Attack()); 
            _canAttack = false;
            _attCount = 0;
            _anim._attIndex = 2;
            Attack();
        }
        if(_attCount < 3 && _sensor._IsNear && !_anim._isAttack && _canAttack)
        {
            StartCoroutine(CRT_Attack());
            _canAttack = false;
            _attCount++;
            _anim._attIndex = 1;
            Attack();
        }
        
        

    }
    void Attack()
    {
        //StartCoroutine(CRT_Attack());
        
        _anim.SetAttack();


        if (_anim._attIndex == 3)
            StartCoroutine(MoveAndJump());
    }
    private IEnumerator MoveAndJump()
    {
        //_navMesh.isStopped = true;
        _navMesh.enabled = false;
        _initialPos = transform.position;
        _targetPos = _playerTrs.position;
        _effectController.SetJumpSlash(_targetPos);
        _sound.PlayJumpSound();
        yield return new WaitForSeconds(0.3f);
        


        float elapsedTime = 0f;
        while (elapsedTime < _moveDur)
        {
            float t = elapsedTime / _moveDur;
            if(elapsedTime < _moveDur / 2)
            {
                _targetPos.y += Time.deltaTime * (_jumpHeight / _moveDur);
            }
            else
            {
                _targetPos.y -= Time.deltaTime * (_jumpHeight / _moveDur);
            }
            
            transform.position = Vector3.Lerp(_initialPos, _targetPos, t);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        _camMove.TriggerCameraShake(1f, 0.1f);
        _sound.PlayLandSound();
        transform.position = _targetPos;

        _navMesh.enabled = true;
        //_navMesh.isStopped = false;
    }

    IEnumerator CRT_Attack()
    {
        yield return new WaitForSeconds(_coolTime);
        _canAttack = true;
        _isAttack2 = false;
    }
    public void GetWeakDamage(float damage)
    {
        _enemyHealth.GetDamage(damage);
    }
    public void SetCameraMove(CameraMove camMove)
    {
        _camMove = camMove;
    }
}
