using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack_Bow : MonoBehaviour, IAttackable
{
    [SerializeField] GameObject _damageBox;
    [SerializeField] Transform _arrowPivot;
    public Anim_Bow _animEvent;

    
    [SerializeField] CameraMove _camMove;
    [SerializeField] GameObject _sunArrow;

    [SerializeField] Transform _arrowSpawnTrs;


    GameObject _arrowObj;

    GameObject _spawnedSunArrow;

    float _damage = 0;

    public bool _isShootFire = false;

    float _stunDamage = 0f;
    float _stunDur = 0f;

    private void Update()
    {
        if(Input.GetMouseButtonDown(1))
        {
            if(!_isShootFire)
                _arrowObj = Instantiate(_damageBox, _arrowPivot.position, _arrowPivot.rotation, _arrowPivot);
            else
                _spawnedSunArrow = Instantiate(_sunArrow, _arrowSpawnTrs.position, _arrowSpawnTrs.rotation, _arrowSpawnTrs);
        }
        if(Input.GetMouseButtonUp(1))
        {
            if(!_isShootFire)
                Destroy(_arrowObj);
            else
                Destroy(_spawnedSunArrow);
        }
        
    }
    bool _isAiming = false;
    
    public void Attack()
    {
        if(_isAiming && !_isShootFire)
        {
            ArrowDamageBox damageBox = _arrowObj.GetComponentInChildren<ArrowDamageBox>();

            _arrowObj.transform.SetParent(null);
            _animEvent.SetShoot();

            Vector3 ray = Camera.main.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 0.0f));

            RaycastHit hitInfo;

            int layerMask = 1 << LayerMask.NameToLayer("Player");
            if (Physics.Raycast(ray, Camera.main.transform.forward, out hitInfo, Mathf.Infinity, ~layerMask))
            {
                Vector3 dir = (hitInfo.point - _arrowObj.transform.position).normalized;

                damageBox._isShooted = true;
                damageBox.ShootArrow(dir);
                damageBox.SetDamage(_damage);
            }


            


            _arrowObj = Instantiate(_damageBox, _arrowPivot.position, _arrowPivot.rotation, _arrowPivot);
        }
        if(_isShootFire)
        {
            _isShootFire = false;
            _animEvent.SetShoot();

            
            _spawnedSunArrow.transform.SetParent(null);
            DamageBox_SunArrow damageBox_SunArrow = _spawnedSunArrow.GetComponent<DamageBox_SunArrow>();
            Vector3 ray = Camera.main.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 0.0f));

            RaycastHit hitInfo;

            int layerMask = 1 << LayerMask.NameToLayer("Player");
            if (Physics.Raycast(ray, Camera.main.transform.forward, out hitInfo, Mathf.Infinity, ~layerMask))
            {
                Vector3 dir = (hitInfo.point - _spawnedSunArrow.transform.position).normalized;

                damageBox_SunArrow.Shoot(dir);
                damageBox_SunArrow.SetStat(_stunDamage, _stunDur);
            }
            
        }
    }
    public void SetIsAiming(bool isAiming)
    {
        _isAiming = isAiming;
        _animEvent.SetIsAiming(isAiming);

    }
    public void SetIsBow(bool isBow)
    {
        _animEvent.SetIsBow(isBow);
        _camMove.SetIsBow(isBow);
    }
    public void SetDamage(float damage)
    {
        _damage = damage;
    }
    public void SetSunStat(float degree, float durTime)
    {
        _stunDamage = degree;
        _stunDur = durTime;
    }
}
