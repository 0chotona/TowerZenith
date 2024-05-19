using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] int _maxHp = 100; // �ִ� ü��
    [SerializeField] float _def = 0; // �� ����
    float _baseDef = 10f; // �⺻ ����
    float _curHp; // ���� ü��

    Collider _collider;

    [SerializeField] Slider _hpSlider;

    [Header("�ǰ� ����Ʈ"), SerializeField] ParticleSystem _hitEffect;

    float _increaseDef = 0;

    private void Awake()
    {
        _collider = GetComponent<Collider>();
        _curHp = _maxHp;
        _hpSlider.maxValue = _maxHp;
        _hpSlider.value = _curHp;
    }
    public void GetDamage(float damage)
    {
        _hitEffect.Play();
        int finalDamage = (int)(damage * 100 / (100 + _def + _increaseDef));
        _curHp -= finalDamage;
        _hpSlider.value = _curHp;
        if (_curHp < 0)
            Dead();
    }
    public void SetShieldDef(float def)
    {
        _def = _baseDef + 10;
    }
    public void SetBuffDef(float def)
    {
        _increaseDef += def;
    }
    public void GetHeal(float hp)
    {
        _curHp += hp;
    }
    public void Dead()
    {
        _collider.enabled = false;
        Debug.Log("�÷��̾� ����");
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("DiePoint"))
        {
            GetDamage(15f);
            transform.position = new Vector3(other.transform.position.x, 3f, other.transform.position.z);
        }
    }
}
