using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ItembundleBaseData", menuName = "ItembundleBaseData", order = int.MaxValue)]

public class ItemBundleBaseData : ScriptableObject
{
    

    [SerializeField] Sprite[] _itemIcons;
    public Sprite[] _ItemIcons => _itemIcons;

    [SerializeField]
    [TextArea(12, 20)]

    string _data;
    public string _Data => _data;
}
