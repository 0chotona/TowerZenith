using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageBox_SpinBall : MonoBehaviour
{
    float _angle = 0;
    float _startAngle = 0;
    float _radius = 2f;

    float _damage = 10f;

    float _speed = 400;

    public Transform _playerTrs;

    private void Update()
    {
        MoveFireBall();
    }
    void MoveFireBall()
    {
        _angle += Time.deltaTime * _speed;
        if (_angle < 360)
        {
            var rad = Mathf.Deg2Rad * _angle;
            var x = _radius * Mathf.Sin(rad);
            var z = _radius * Mathf.Cos(rad);

            transform.position = _playerTrs.parent.position + new Vector3(x, 0, z);
        }
        else
        {
            _angle = 0;
        }
    }
    public void SetStartAngle(float angle)
    {
        _startAngle = angle;
        _angle = _startAngle;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            EnemyHealth enemyHealth = other.GetComponent<EnemyHealth>();
            enemyHealth.GetDamage(_damage);
        }
    }
    public void UpdateDamage(int damage) { _damage = damage; }
    public void SetTarget(Transform target)
    {
        _playerTrs = target;
    }
}
