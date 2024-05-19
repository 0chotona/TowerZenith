using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Treasure_Item : MonoBehaviour
{

    [SerializeField] Transform _itemSpawnPos;

    [Header("패널"), SerializeField] GameObject _itemInfoPanel;
    [Header("패널_이름"), SerializeField] TextMeshProUGUI _nameText;
    //[Header("패널_카테고리"), SerializeField] TextMeshProUGUI _categoryText;
    [Header("패널_타입"), SerializeField] TextMeshProUGUI _typeText;
    [Header("패널_스탯"), SerializeField] TextMeshProUGUI[] _statTexts;
    [Header("패널_버프"), SerializeField] TextMeshProUGUI _buffText;


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
        _statTexts[0].text = (item._Att_Power == 0) ? string.Empty : "공격력 : " + item._Att_Power.ToString();
        _statTexts[1].text = (item._Att_Speed == 0) ? string.Empty : "공격 속도 : " + item._Att_Speed.ToString();
        _statTexts[2].text = (item._Def == 0) ? string.Empty : "방어력 : " + item._Def.ToString();

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
