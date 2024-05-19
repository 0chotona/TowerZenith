using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SunExplosion : MonoBehaviour
{
    float _damage = 10f;
    List<GameObject> _nearEnemies = new List<GameObject>();

    Collider _collider;

    public float _stunDur;
    public float _stunDamage;
    private void Awake()
    {
        _collider = GetComponent<Collider>();
        _collider.enabled = true;
        StartCoroutine(CRT_OffColldier());
    }
    public void SetDamage(float damage)
    {
        _damage = damage;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            EnemyMove enemyMove = other.GetComponent<EnemyMove>();
            enemyMove.GetStun(_stunDur);
            Knight_Anim knight_Anim = other.GetComponentInChildren<Knight_Anim>();
            if (knight_Anim != null)
                knight_Anim.SetStun();
            EnemyHealth enemyHealth = other.GetComponent<EnemyHealth>();
            enemyHealth.GetDamage(_damage);
        }
        
    }
    IEnumerator CRT_OffColldier()
    {
        yield return new WaitForSeconds(0.05f);
        _collider.enabled = false;
    }
}
