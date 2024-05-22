using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class AttackController
{
    IAttackable _attackMethod;
    public void SetAttackMethod(IAttackable attackMethod)
    {
        _attackMethod = attackMethod;
    }
    public IAttackable GetAttackMethod()
    {
        return _attackMethod;
    }
    public void PerformAttack()
    {
        if (_attackMethod != null)
            _attackMethod.Attack();
        else
            Debug.Log("어택 메소드 없음");
    }
}
public class PlayerAttack : MonoBehaviour
{
    AttackController _attController;

    IAttackable[] _weapons;

    Attack_OneHandSword _oneHandAtt;
    Attack_TwoHandSword _twoHandAtt;
    Attack_Bow _bowAtt;

    [SerializeField] DamageBox _damageBox;
    [SerializeField] PlayerHealth _playerHealth;


    //List<IAttackable> _attackables;

    int _index;

    float _damage = 20;
    public float _Damage => _damage;
    float _knockBackRate = 1f;

    float _attackSpeed = 10;

    bool _isAiming = false;
    bool _isBow = false;

    public int _weaphonIndex = 1;


    [SerializeField] CameraMove _camMove;

    public bool _isAttack = false;
    public bool _isFinalAttack = false;

    private void Awake()
    {
        _attController = new AttackController();

        _oneHandAtt = GetComponent<Attack_OneHandSword>(); //무기 팩토리 (무기를 제공해주는 클래스) 리스트 사용,
        _twoHandAtt = GetComponent<Attack_TwoHandSword>();
        _bowAtt = GetComponent<Attack_Bow>();

        //_attackables = new List<IAttackable>() { _oneHandAtt, _spearAtt ,_bowAtt};

        _attController.SetAttackMethod(_oneHandAtt);

        _damageBox.SetDamage(_damage);
        _damageBox.SetKnockBack(_knockBackRate);
    }
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            _attController.PerformAttack();
        }
        switch(_attController.GetAttackMethod())
        {
            case Attack_OneHandSword:
                _isAttack = false;
                _oneHandAtt.Anim_Attack();
                _oneHandAtt._animEvent.OneHandAnim();
                _isAttack = _oneHandAtt._animEvent._isAttack;
                _isFinalAttack = _oneHandAtt._animEvent._isFinalAttack;

                _damageBox.SetDamage(_damage);
                //_playerHealth.SetShieldDef(_def);
                
                break;
            case Attack_TwoHandSword:
                _isAttack = false;
                _twoHandAtt.Anim_Attack();
                _twoHandAtt._animEvent.TwoHandAnim();
                _isAttack = _twoHandAtt._animEvent._isAttack;
                _isFinalAttack = _twoHandAtt._animEvent._isFinalAttack;

                _damageBox.SetDamage(_damage);
                //_playerHealth.SetShieldDef(0);
                break;
            case Attack_Bow:
                _isAttack = false;
                _isAiming = Input.GetMouseButton(1);
                
                _bowAtt.SetIsAiming(_isAiming);
                _camMove.SetIsAiming(_isAiming);

                _bowAtt.SetDamage(_damage);
                //_playerHealth.SetShieldDef(0);
                //_bowAtt._animEvent.SetIsAiming(Input.GetMouseButton(1));
                break;
        }
        /*
        _isBow = (_weaphonIndex == 3);
        _bowAtt.SetIsBow (_isBow);
        */
        //ChangeWeaphon();
        
    }
    public void ResetAllCombo()
    {
        _oneHandAtt._animEvent.ResetCombo();
        _twoHandAtt._animEvent.ResetCombo();
    }
    public void UpdateWeaphonType(int  weaphonType)
    {
        _weaphonIndex = weaphonType;
        
        switch (weaphonType)
        {
            case 1:
                _attController.SetAttackMethod(_oneHandAtt);
                break;
            case 2:
                _attController.SetAttackMethod(_twoHandAtt);
                break;
            case 3:
                _attController.SetAttackMethod(_bowAtt);
                
                break;
        }
        _isBow = (_weaphonIndex == 3);
        _bowAtt.SetIsBow(_isBow);
        PlayerAnimation.Instance.SetWeaphonAnim(weaphonType);
    }

    public void SetAttackStat(float damage, float attSpeed)
    {
        _damage = damage;
        _attackSpeed = attSpeed;

        if (_weaphonIndex == 1)
            _oneHandAtt.SetAttackSpeed(_attackSpeed);
        else if (_weaphonIndex == 2)
            _twoHandAtt.SetAttackSpeed(_attackSpeed);


    }
    public void SetItemStat(float damage, float attSpeed)
    {
        _damage = damage;
        _attackSpeed = attSpeed;

        if(_weaphonIndex == 1)
            _oneHandAtt.SetAttackSpeed(_attackSpeed);
        else if(_weaphonIndex == 2)
            _twoHandAtt.SetAttackSpeed(_attackSpeed);
    }
    public void SetItemStat(float damage)
    {
        _damage = damage;
    }
    
}

