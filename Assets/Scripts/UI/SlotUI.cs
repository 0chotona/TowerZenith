using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum eSLOTTYPE
{
    One_Hand,
    Shield,
    Two_Hand,
    Bow,
    Artifact,
    None
}
public class SlotUI : MonoBehaviour
{
    bool _hasItem;
    public bool _HasItem => _hasItem;
    [Header("½½·Ô Å¸ÀÔ"),SerializeField] eSLOTTYPE _slotType;
    public eSLOTTYPE _SlotType => _slotType;

    public ItemUI _itemUI;
    

    Image _image;
    private void Awake()
    {
        _image = GetComponent<Image>();
        _itemUI.SetParentSlot(GetComponent<SlotUI>());
        //_item = new CItem();
        //_item._Category = eCATEGORY.Artifact;
    }

}
