using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Treasure_Item : MonoBehaviour
{

    [SerializeField] Transform _itemSpawnPos;

    [Header("�г�"), SerializeField] GameObject _itemInfoPanel;
    [Header("�г�_�̸�"), SerializeField] TextMeshProUGUI _nameText;
    //[Header("�г�_ī�װ�"), SerializeField] TextMeshProUGUI _categoryText;
    [Header("�г�_Ÿ��"), SerializeField] TextMeshProUGUI _typeText;
    [Header("�г�_����"), SerializeField] TextMeshProUGUI[] _statTexts;
    [Header("�г�_����"), SerializeField] TextMeshProUGUI _buffText;


    bool _isGot = false;

    CItem _item;
    Inventory _inventory;
    GameObject _itemModel;

    SoundUI _soundUI;

    public void SetItem(GameObject itemObj,CItem item,Inventory inventory)
    {
        _item = item;
        _inventory = inventory;

        _itemModel = Instantiate(itemObj, _itemSpawnPos.position, Quaternion.identity,_itemSpawnPos);
        _itemModel.SetActive(true);
        SetItemInfo(item);
        _itemInfoPanel.SetActive(false);
    }
    public void SetSoundUI(SoundUI soundUI)
    {
        _soundUI = soundUI;
    }
    
    void SetItemInfo(CItem item)
    {
        _nameText.text = item._Name;
        _typeText.text = item._Type.ToString();
        _statTexts[0].text = (item._Att_Power == 0) ? string.Empty : "���ݷ� : " + item._Att_Power.ToString();
        _statTexts[1].text = (item._Att_Speed == 0) ? string.Empty : "���� �ӵ� : " + item._Att_Speed.ToString();
        _statTexts[2].text = (item._Def == 0) ? string.Empty : "���� : " + item._Def.ToString();

    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _itemInfoPanel.SetActive(true);
            
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            if (Input.GetKeyDown(KeyCode.F) && !_isGot)
            {
                _inventory.GetItem(_item);
                Destroy(_itemModel);
                Destroy(_itemInfoPanel);
                Collider collider = GetComponent<Collider>();
                collider.enabled = false;
                _isGot = true;
                _soundUI.PlayGet();
            }

            Vector3 dirToOther = other.transform.position - _itemInfoPanel.transform.position;
            Quaternion lookRotation = Quaternion.LookRotation(-dirToOther.normalized);
            _itemInfoPanel.transform.rotation = Quaternion.Euler(0f, lookRotation.eulerAngles.y, 0f);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _itemInfoPanel.SetActive(false);
        }
    }

}
