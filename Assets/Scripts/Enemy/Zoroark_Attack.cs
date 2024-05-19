using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zoroark_Attack : MonoBehaviour
{
    public float _dmg { get; set; }

    Rigidbody _rigid;
    float _shootPower = 1000f;

    GameObject _tmpObj;
    private void Awake()
    {
        _rigid = GetComponent<Rigidbody>();
        _tmpObj = gameObject;

        Destroy(_tmpObj,5f);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerHealth playerHealth = other.GetComponent<PlayerHealth>();
            playerHealth.GetDamage(_dmg);
        }
        
    }
    public void Shoot(Vector3 dir)
    {
        _rigid.AddForce(dir * _shootPower);
    }
}
