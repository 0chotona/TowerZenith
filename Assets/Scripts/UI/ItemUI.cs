using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ItemUI : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [Header("아이템 타입"), SerializeField] eCATEGORY _category;
    public eCATEGORY _Category => _category;

    CItem _item;
    public CItem _Item => _item;

    Image _image;

    public SlotUI _parentSlot;
    GameObject _infoPanelObj;

    private void Awake()
    {
        _image = GetComponent<Image>();
    }
    public void SetParentSlot(SlotUI slotUI)
    {
        _parentSlot = slotUI;
    }
    public void SetItemSlot(CItem item)
    {
        _item = item;
        if (item != null)
        {
            if (_category == eCATEGORY.None)
            {
                _item = item;
            }
            else if (item._Category.ToString() == _category.ToString())
            {
                _item = item;
            }
            _category = _item._Category;
        }
        else
        {
            _item = null;
            _category = eCATEGORY.None;
        }
    }
    public void SetCatrgory(eCATEGORY category)
    {
        _category = category;
    }
    public void SetIcon(Sprite icon)
    {
        _image.sprite = icon;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (_category != eCATEGORY.None)
        {
            ShowItemInfoUI showItemInfoUI = _infoPanelObj.GetComponent<ShowItemInfoUI>();
            showItemInfoUI.SetInfoText(_item);
            _infoPanelObj.SetActive(true);
        }
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        _infoPanelObj.SetActive(false);
    }
    public void SetInfoPanel(GameObject infoPanelObj)
    {
        _infoPanelObj = infoPanelObj;
    }
}
