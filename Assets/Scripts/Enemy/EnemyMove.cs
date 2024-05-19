using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public interface IEnemyBehavior
{
    void Initialize(Transform playerTransform, EnemyAnimation enemyAnimation);
    void Move();
    void GetKnockBack(float knockbackRate);
}

public class EnemyMove : MonoBehaviour
{
    public Transform _playerTrs;
    PlayerInvisible _playerInvisible;
    [SerializeField] float _dist;
    float _coolTime = 2f;

    public bool _lockOn = true;
    bool _isAttack = true;

    EnemyAnimation _anim;
    NavMeshAgent _navMesh;
    IEnemyBehavior _enemyBehavior;

    public MONSTERTYPE _monsterType;

    public bool _isFrozed = false;
    bool _isStun = false;

    public bool _isSticked = false;

    GameObject _iceSpearObj;

    EnemyHealth _health;

    private void Start()
    {
        _anim = GetComponentInChildren<EnemyAnimation>();
        _navMesh = GetComponent<NavMeshAgent>();

        _playerInvisible = _playerTrs.GetComponent<PlayerInvisible>();

        _health = GetComponent<EnemyHealth>();
    }

    private void Update()
    {
        if (_playerTrs != null)
            _dist = Vector3.Distance(_playerTrs.position, transform.position);

        if(!_isFrozed && !_isStun && !_playerInvisible._IsInvisible && !_isSticked && !_health._IsDead)
            _enemyBehavior.Move();
        
        if(_isSticked)
        {
            transform.position = new Vector3(_iceSpearObj.transform.position.x, transform.position.y, _iceSpearObj.transform.position.z);
        }
        
    }

    public void GetKnockBack(float knockbackRate)
    {
        _enemyBehavior.GetKnockBack(knockbackRate);
    }

    public void SetPlayerTrs(Transform playerTrs)
    {
        _playerTrs = playerTrs;
    }
    public void GetFroze(float durTime)
    {
        StartCoroutine(CRT_GetFroze(durTime));
    }
    public void GetStun(float durTime)
    {
        StartCoroutine(CRT_GetStun(durTime));
    }
    public void StopFroze()
    {
        StopCoroutine(CRT_GetFroze(0));
    }
    IEnumerator CRT_GetStun(float durTime)
    {
        _isStun = true;
        yield return new WaitForSeconds(durTime);
        _isStun = false;
    }
    IEnumerator CRT_GetFroze(float durTime)
    {
        
        _isFrozed = true;
        yield return new WaitForSeconds(durTime);
        _isFrozed = false;
    }
    public void SetEnemyBehavior(IEnemyBehavior enemyBehavior)
    {
        _enemyBehavior = enemyBehavior;
        _enemyBehavior.Initialize(_playerTrs, _anim);
    }
    
    private void OnTriggerEnter(Collider other)
    {
        
        if(other.CompareTag("IceSpear"))
        {
            _iceSpearObj = other.gameObject;
            _isSticked = true;
            
        }

        if(other.gameObject.layer == LayerMask.NameToLayer("Wall") && _isSticked)
        {
            _isSticked = false;
            _iceSpearObj = null;
        }
        
    }
    
}