using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InGameUI : MonoBehaviour
{
    static InGameUI _instance;
    public static InGameUI _Instance
    {
        get
        {
            if (_instance == null)
                _instance = FindObjectOfType<InGameUI>();
            return _instance;
        }
    }


    [Header("미니 맵"), SerializeField] GameObject _miniMapObj;

    [Header("인벤토리 창"), SerializeField] GameObject _inventoryObj;
    [Header("인벤토리 Off 버튼"), SerializeField] Button _inventoryButton;

    [Header("스탯 텍스트"), SerializeField] TextMeshProUGUI[] _statTexts;


    [Header("다음 스테이지 이동"), SerializeField] GameObject _pressPanel;

    [Header("테스트 버튼"), SerializeField] Button _testButton;
    [Header("테스트 패널"), SerializeField] GameObject _testPanel;

    public float _hp { get; set; }
    public float _att { get; set; }
    public float _attSpeed { get; set; }
    public float _def { get; set; }
    public float _criticalRate { get; set; }
    public float _criticalDmg { get; set; }
    public float _moveSpeed { get; set; }
    public float _jumpPower { get; set; }

    float[] _stats;

    public bool _onInven = false;
    
    private void Awake()
    {
        _inventoryButton.onClick.AddListener(() => InventoryOff());

        _stats = new float[_statTexts.Length];

        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

        _pressPanel.SetActive(false);

        _testButton.onClick.AddListener(() => SetTestButton());

        _testPanel.SetActive(false);
    }
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.V))
        {
            _onInven = !_onInven;
            
            _inventoryObj.SetActive(_onInven);
            SetCursorOnOff();


        }
        if (Input.GetKeyDown(KeyCode.M))
        {
            _miniMapObj.SetActive(!_miniMapObj.activeSelf);
        }
    }
    public void UpdateStatPanel()
    {
        _statTexts[0].text = _hp.ToString();
        _statTexts[1].text = _att.ToString();
        _statTexts[2].text = _attSpeed.ToString();
        _statTexts[3].text = _def.ToString();
        _statTexts[4].text = _criticalRate.ToString();
        _statTexts[5].text = _criticalDmg.ToString();
        _statTexts[6].text = _moveSpeed.ToString();
        _statTexts[7].text = _jumpPower.ToString();
    }
    public void SetInventoryActive(bool isActive)
    {
        _inventoryObj.SetActive(isActive);
    }
    
    void InventoryOff()
    {
        Time.timeScale = 1;
        _inventoryObj.SetActive(false);
        
    }
    void SetCursorOnOff()
    {
        if (_onInven)
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
        else
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }
    }

    public void SetNextStageActive(bool isActive)
    {
        _pressPanel.SetActive(isActive);
    }
    public void SetTestButton()
    {
        _testPanel.SetActive(!_testPanel.activeSelf);
    }
}
