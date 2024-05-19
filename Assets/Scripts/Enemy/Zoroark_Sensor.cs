using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zoroark_Sensor : MonoBehaviour
{
    Zoroark_Anim _zoroark_Anim;
    int _count = 0;
    public int _Count => _count;
    int _maxCount = 3;

    bool _isNear = false;
    public bool _IsNear => _isNear;
    private void Awake()
    {
        _zoroark_Anim = GetComponent<Zoroark_Anim>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            _isNear = true;
            if (!_zoroark_Anim._isAttack)
                _count++;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _isNear = false;
        }
    }
}
