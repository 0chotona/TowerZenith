using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowIceSpear : MonoBehaviour
{
    [Header("얼음창 프리펩"),SerializeField] GameObject _iceSpearObj;
    [Header("창 스폰 부모"), SerializeField] Transform _spawnTrs;

    [SerializeField] CameraMove _camMove;

    GameObject _spawnedSpear;

    Animator _anim;
    public void SpawnSpear()
    {
        _spawnedSpear = Instantiate(_iceSpearObj, _spawnTrs.position, _spawnTrs.rotation,_spawnTrs);
        _camMove.SetIsAimingSpear(true);
    }
    public void AimingSpear()
    {
       
    }
    public void PlayThrowAnim()
    {
        _camMove.SetIsAimingSpear(false);
        _anim.SetTrigger("IsThrow");
    }
    public void Anim_ShootSpear()
    {
        _spawnedSpear.transform.SetParent(null);
        IceSpearBox iceSpearBox = _spawnedSpear.GetComponent<IceSpearBox>();
        iceSpearBox.Shoot(_spawnTrs.forward);
    }
}
