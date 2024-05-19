using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ArrowDamageBox : MonoBehaviour
{
    Rigidbody _rigid;

    float _shootPower = 50f;

    float _damage;
    float _criticalDamageRate = 2f;

    GameObject _tmpObj;

    public Vector3 _test;
    public bool _isShooted = false;
    private void Awake()
    {
        _isShooted = false;

        _rigid = GetComponent<Rigidbody>();
        _rigid.isKinematic = true;
        _tmpObj = gameObject;
    }
    private void Update()
    {
        _test = transform.forward;
    }
    public void SetDamage(float damage)
    {
        _damage = damage;
    }
    public void ShootArrow(Vector3 dir)
    {
        _rigid.isKinematic = false;
        transform.rotation = Quaternion.LookRotation(dir);
        _rigid.AddForce(dir * _shootPower, ForceMode.Impulse);
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Enemy") && _isShooted)
        {
            EnemyHealth enemyHealth = other.GetComponent<EnemyHealth>();
            enemyHealth.GetDamage(_damage);
            Destroy(_tmpObj);
        }
        if (other.CompareTag("WeakPoint") && _isShooted)
        {
            

            GiveWeakDamage(other);
            Destroy(_tmpObj);
        }
        if (other.gameObject.layer == LayerMask.NameToLayer("Wall"))
        {
            Destroy(_tmpObj);
        }
    }
    void GiveWeakDamage(Collider other)
    {
        IEnemyBehavior enemyBehavior = null;
        EnemyMove enemyMove = other.GetComponentInParent<EnemyMove>();

        switch (enemyMove._monsterType)
        {
            case MONSTERTYPE.Knight:
                Knight_Move knight_Move = other.GetComponentInParent<Knight_Move>();
                knight_Move.GetWeakDamage(_damage * _criticalDamageRate);
                break;
            case MONSTERTYPE.Ghost:
                Ghost_Move ghost_Move = other.GetComponentInParent<Ghost_Move>();
                ghost_Move.GetWeakDamage(_damage * _criticalDamageRate);
                break;
        }
    }
}
