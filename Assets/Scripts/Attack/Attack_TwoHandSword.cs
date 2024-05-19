using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack_TwoHandSword : MonoBehaviour, IAttackable
{
    [SerializeField] GameObject _damageBox;
    public Anim_TwoHandSword _animEvent;


    int _inputCombo = 0;
    int _curCombo = 0;

    public void Attack()
    {
        _animEvent.UpdateCombo();
    }
    public void SetAttackSpeed(float attSpeed)
    {
        _animEvent.SetAttackSpeed(attSpeed);
    }
    public void Anim_Attack()
    {
        bool inActive = (_animEvent._activeIndex == 0) ? false : true;
        _damageBox.SetActive(inActive);
    }
}
