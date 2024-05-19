using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Guardian_Move : MonoBehaviour
{
    /*
    NavMeshAgent _navMesh;
    public Transform _playerTrs;
    [SerializeField] float _dist;
    [SerializeField] GameObject _damageBox_1;
    [SerializeField] GameObject _damageBox_2;
    [SerializeField] Transform _spawnPos;

    [SerializeField] int _attackCount = 0;
    float _coolTime = 3f;

    bool _isAttack = true;

    bool _isPassOut = false;
    float _passOutTime = 3f;


    CEnemy _cEnemy;

    Shader _rimLightShader;
    Shader _standardShader;

    Renderer[] _renderers;

    EnemyHealth _enemyHealth;
    private void Start()
    {
        _navMesh = GetComponentInParent<NavMeshAgent>();
        _enemyHealth = GetComponent<EnemyHealth>();
        _renderers = GetComponentsInChildren<Renderer>();


        _enemyHealth.SetDefStat(_cEnemy._MaxHp, _cEnemy._Def);
        _navMesh.speed = _cEnemy._MoveSpeed;

        _rimLightShader = Shader.Find("Custom/EnemyRinLight");
        _standardShader = Shader.Find("Standard");

        ChangeShader(_rimLightShader);
        StartCoroutine(CRT_Attack1());
    }
    private void Update()
    {
        if (_playerTrs != null)
            _dist = Vector3.Distance(_playerTrs.position, transform.position);
        Move();
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
    void Attack1()
    {
        GameObject damageBox = Instantiate(_damageBox, transform.position, Quaternion.identity);
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

        _isPassOut = true;
        ChangeShader(_standardShader);
        yield return new WaitForSeconds(_passOutTime);
        _isPassOut = false;
        ChangeShader(_rimLightShader);
        
    }
    IEnumerator CRT_Attack1()
    {
        while (true)
        {
            if (!_isPassOut)
                Attack1();
            yield return new WaitForSeconds(_coolTime);
        }

    }
    public void GetWeakDamage(float damage)
    {
        _enemyHealth.GetDamage(damage);
        PassOut();
    }

    void ChangeShader(Shader shader)
    {
        for (int i = 0; i < _renderers.Length; i++)
        {
            Material[] materials = _renderers[i].materials;
            for (int j = 0; j < materials.Length; j++)
            {
                materials[j].shader = shader;
            }
        }
    }
    */
}
