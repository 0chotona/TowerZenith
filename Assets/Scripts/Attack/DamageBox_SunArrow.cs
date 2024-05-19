using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DamageBox_SunArrow : MonoBehaviour
{

    Rigidbody _rigid;
    [Header("이동 속도"), SerializeField] float _shootPower = 1000f;
    [SerializeField] GameObject _sunExplosion;

    GameObject _tmpObj;

    float _damage;
    float _durTime;
    
    private void Awake()
    {
        _rigid = GetComponent<Rigidbody>();

        _tmpObj = gameObject;
    }
    public void Shoot(Vector3 dir)
    {
        _rigid.AddForce(_shootPower * dir);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy") || other.gameObject.layer == LayerMask.NameToLayer("Wall") || other.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            GameObject explose = Instantiate(_sunExplosion, transform.position, Quaternion.identity);
            SunExplosion sunExplosion = explose.GetComponent<SunExplosion>();
            sunExplosion._stunDur = _durTime;
            sunExplosion._stunDamage = _damage;
            SoundActiveSkill._Instance.PlaySun();
            Destroy(explose, 1.2f);
            Destroy(_tmpObj, 1.5f);
        }
    }
    public void SetStat(float degree,float durTime)
    {
        _damage = degree;
        _durTime = durTime;
    }
}
