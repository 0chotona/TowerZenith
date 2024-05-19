using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Attack_OneHandSword : MonoBehaviour, IAttackable
{
    [SerializeField] GameObject _damageBox;
    public Anim_OneHandSword _animEvent;
    

    public void Attack()
    {
        _animEvent.UpdateCombo();
        
    }
    public void Anim_Attack()
    {
        bool inActive = (_animEvent._activeIndex == 0) ? false : true;
        _damageBox.SetActive(inActive);
    }
    public void SetAttackSpeed(float attSpeed)
    {
        _animEvent.SetAttackSpeed(attSpeed);
    }
    public void ControlIndex()
    {
        //_curCombo = _animEvent._curCombo;
        //_animEvent._inputCombo = _inputCombo;
        //if(_curCombo == 0) { _inputCombo = 0; }
    }

}
