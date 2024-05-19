using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlashBox : MonoBehaviour
{
    Rigidbody _rigid;
    [Header("이동 속도"),SerializeField] float _shootPower = 30f;

    float _damage = 10f;

    GameObject _tmpObj;

    private void Awake()
    {
        _rigid = GetComponent<Rigidbody>();
        

        _tmpObj = gameObject;
    }
    public void SetDamage(float damage)
    {
        _damage = damage;
    }
    public void Shoot(Vector3 dir)
    {
        _rigid.AddForce(_shootPower * dir);
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Enemy"))
        {
            EnemyHealth enemyHealth = other.GetComponent<EnemyHealth>();
            enemyHealth.GetDamage(_damage);
        }
        if(other.gameObject.layer == LayerMask.NameToLayer("Wall") || other.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            Destroy(_tmpObj);
        }
    }
}
