using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    float _dmg;
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            PlayerHealth playerHealth = other.GetComponent<PlayerHealth>();
            playerHealth.GetDamage(_dmg);
        }
    }
    public void SetDamage(float damage)
    {
        _dmg = damage;
    }
    public void SetActive(bool isActive)
    {
        gameObject.SetActive(isActive);
    }
}
