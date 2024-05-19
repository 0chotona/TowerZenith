using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] int _maxHp = 100;
    [SerializeField] float _def = 10;

    [Header("Hp ¹Ù"), SerializeField] Slider _hpSlider;
    [Header("ÀÌ¸§"), SerializeField] TextMeshProUGUI _nameText;

    [Header("ÇÇ°Ý ÀÌÆåÆ®"), SerializeField] ParticleSystem _hitEffect;
    [Header("ÆÄ±« ÀÌÆåÆ®"), SerializeField] GameObject _destroyEffect;

    [SerializeField] EnemyAnimation _anim;

    [SerializeField] SoundEnemy _sound;

    public string _name { get; set; }
    int _curHp = 100;
    Collider _collider;

    GameObject _tmpObj;

    float _burnDamage = 2f;
    bool _isBurning = false;

    bool _isDead = false;
    public bool _IsDead => _isDead;

    public bool _hasBurn { set; get; }
    private void Awake()
    {
        _collider = GetComponent<Collider>();
        _tmpObj = gameObject;
        _hitEffect.Stop();


    }
    public void GetDamage(float damage)
    {
        int finalDamage = (int)(damage * 100 / (100 + _def));
        _curHp -= finalDamage;

        Debug.Log(finalDamage);
        _hitEffect.Play();
        
        _sound.PlayImpactSound();
        _hpSlider.value = _curHp;

        if (_hasBurn && !_isBurning)
            StartCoroutine(CRT_Burn());
        if (_curHp < 0)
            Dead();
    }
    public void SetDefStat(int hp, int def)
    {
        _maxHp = hp;
        _def = def;

        _hpSlider.gameObject.SetActive(true);
        if(_nameText != null)
            _nameText.text = _name;
        _curHp = _maxHp;
        _hpSlider.maxValue = _maxHp;
        _hpSlider.value = _curHp;
    }
    IEnumerator CRT_Burn()
    {
        _isBurning = true;
        for(int i = 0; i < 5; i++)
        {
            GetDamage(_burnDamage);
            yield return new WaitForSeconds(1f);
        }
        _isBurning = false;
        
    }
    public void Dead()
    {
        if(_hpSlider != null)
            Destroy(_hpSlider.transform.gameObject);
        _collider.enabled = false;
        _isDead = true;
        _tmpObj = (transform.parent != null) ? transform.parent.gameObject : gameObject;
        if (_anim != null)
            _anim.SetDead(_destroyEffect,_tmpObj);
        else
        {
            Instantiate(_destroyEffect, transform.position, Quaternion.identity);
            
            Destroy(_tmpObj);
        }

        
        
    }
    
}
