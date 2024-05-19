using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinAttack : MonoBehaviour
{

    [SerializeField] GameObject _fireBallObj;
    DamageBox_SpinBall _spinBall;

    List<GameObject> _spawnedBalls = new List<GameObject>();

    public bool _isSynergy = false;
    public void SpawnFireBall()
    {
        int count = _isSynergy ? 3 : 1;
        float angle = 360 / count;
        for (int i = 0; i < count; i++)
        {
            GameObject damageBox = Instantiate(_fireBallObj);

            _spawnedBalls.Add(damageBox);
            _spinBall = damageBox.GetComponent<DamageBox_SpinBall>();

            _spinBall.SetStartAngle(angle * i);
            _spinBall.SetTarget(transform);
        }
        SoundActiveSkill._Instance.PlayFireBall(true);
    }
    public void ResetFireBall()
    {
        if(_spawnedBalls.Count > 0 )
        {
            foreach (GameObject spawnedBall in _spawnedBalls)
                Destroy(spawnedBall);
        }
        _spawnedBalls.Clear();
        SoundActiveSkill._Instance.PlayFireBall(false);
    }
}
