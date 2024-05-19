using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TestUI : MonoBehaviour
{
    [SerializeField] Inventory _inventory;

    [SerializeField] TMP_InputField _inputID;
    [SerializeField] Button _getItemButton;

    [SerializeField] WeaphonData _weaponData;



    private void Awake()
    {
        _getItemButton.onClick.AddListener(() => Click_GetItem());
    }
    void Click_GetItem()
    {
        Dictionary<string, CItem> itemDatas = _weaponData._ItemDatas;
        CItem item = null;
        foreach (var pair in itemDatas)
        {
            if (pair.Value._ID.ToString() == _inputID.text)
            {
                item = pair.Value;
                break;
            }
        }
        _inventory.GetItem(item);
        _inputID.text = string.Empty;
    }
}
