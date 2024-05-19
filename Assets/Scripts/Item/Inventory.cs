using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class Inventory : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    CItem[] _curWeaphon = new CItem[6];

    /*
    [Header("한손 무기 아이콘"), SerializeField] List<Sprite> _oneHandIcons;
    [Header("두손 무기 아이콘"), SerializeField] List<Sprite> _twoHandicons;
    [Header("방패 아이콘"), SerializeField] List<Sprite> _shieldIcons;
    [Header("활 아이콘"), SerializeField] List<Sprite> _bowIcons;
    */

    [Header("아이템 아이콘"), SerializeField] List<Sprite> _itemIcons;

    [SerializeField] WeaphonData _weaphonData;

    Dictionary<string, CItem> _itemDatas;

    [SerializeField] WeaphonFactory _weaponFactory;

    GameObject _draggedItem;
    ItemUI _draggedItemUI;

    Transform _beginDragSlotTrs;
    SlotUI _beginDragSlotUI;

    Transform _beginDragItemTrs;
    Vector3 _beginDragItemPos;
    Vector3 _beginCursorPos;

    [Header("착용 중 무기"), SerializeField] ItemUI[] _equipedItemUI;
    [Header("비착용 중 무기"), SerializeField] ItemUI[] _gotItemUI;

    [Header("아이템 정보 창"), SerializeField] GameObject _infoPanel;


    int _ignoreLayer = 7;
    int _originalLayer = 5;

    int _oneHandIndex = 0;
    int _shieldIndex = 1;
    int _twoHandIndex = 2;
    int _bowIndex = 3;

    List<CItem> _equipedItems = new List<CItem>();
    List<CItem> _gotItems = new List<CItem>();

    [SerializeField] SoundUI _sound;

    private void Start()
    {
        _itemDatas = _weaphonData._ItemDatas;


        _curWeaphon[_oneHandIndex] = _itemDatas["기본 검"];
        _curWeaphon[_twoHandIndex] = _itemDatas["카타나"];
        _curWeaphon[_shieldIndex] = _itemDatas["나무 방패"];
        _curWeaphon[_bowIndex] = _itemDatas["나무 활"];


        _weaponFactory.SetCurWeaphons(_curWeaphon);
        _weaponFactory.AwakeSetting();

        for (int i = 0; i < _curWeaphon.Length; i++)
        {
            if (_curWeaphon[i] != null)
            {
                _equipedItemUI[i].SetItemSlot(_curWeaphon[i]);
                SetSlotIcon(_equipedItemUI[i]);
            }

        }
        foreach (ItemUI obj in _gotItemUI)
        {
            obj.gameObject.layer = _originalLayer;
            obj.SetInfoPanel(_infoPanel);
        }

        foreach (ItemUI obj in _equipedItemUI)
        {
            obj.gameObject.layer = _originalLayer;
            obj.SetInfoPanel(_infoPanel);
        }

        SetItemList();
        InGameUI._Instance.SetInventoryActive(false);
    }
    public void OnBeginDrag(PointerEventData eventData)
    {

        GameObject clickedObj = eventData.pointerCurrentRaycast.gameObject;


        if (clickedObj != null)
        {
            foreach (ItemUI obj in _gotItemUI)
            {
                obj.gameObject.layer = _ignoreLayer;
            }
            foreach (ItemUI obj in _equipedItemUI)
                obj.gameObject.layer = _ignoreLayer;

            ItemUI itemUI = clickedObj.GetComponent<ItemUI>();
            if (itemUI != null && itemUI._Category != eCATEGORY.None)
            {
                _draggedItemUI = itemUI;
                _draggedItem = clickedObj;
                _beginDragItemTrs = _draggedItem.transform;
                _beginDragItemPos = _beginDragItemTrs.position;
                _beginCursorPos = Input.mousePosition;

                _beginDragSlotTrs = _beginDragItemTrs.parent.transform; //드래그 시작한 슬롯
                _beginDragSlotUI = _draggedItemUI._parentSlot;
            }
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (_draggedItem == null)
            return;

        _draggedItem.transform.SetAsLastSibling();
        
        _draggedItem.transform.position = _beginDragItemPos + Input.mousePosition - _beginCursorPos;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (_draggedItem == null || _draggedItemUI == null) return;
        else _sound.PlayEquip();

        int ignoreLayer = LayerMask.NameToLayer("SelectedSlot");

        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventData, results);

        GameObject droppedObj = FindDroppableUI(results, ignoreLayer); //슬롯
        SlotUI droppedSlotUI = droppedObj.GetComponent<SlotUI>();

        if (droppedObj != null && droppedSlotUI != null)
        {
            HandleDroppedObject(droppedObj);
        }
        else
        {
            ResetDraggedSlotPosition();
        }

        ResetDraggedSlotPosition();
        _draggedItem.layer = _originalLayer;
        _draggedItem = null;
        _draggedItemUI = null;
        SetItemList();

        _weaponFactory.SetChangedItemStat();
    }

    private GameObject FindDroppableUI(List<RaycastResult> results, int ignoreLayer)
    {
        foreach (var result in results)
        {
            if (result.gameObject.layer != ignoreLayer)
            {
                return result.gameObject;
            }
        }
        return null;
    }

    private void HandleDroppedObject(GameObject droppedObj)
    {
        SlotUI droppedSlotUI = droppedObj.GetComponent<SlotUI>();

        if (droppedSlotUI == null) return;

        if (droppedSlotUI._SlotType == eSLOTTYPE.None || _draggedItemUI._Category.ToString() == droppedSlotUI._SlotType.ToString())
        {
            SlotUI slotUI = droppedObj.GetComponent<SlotUI>();
            //GameObject droppedItem = droppedObj.transform.GetChild(0).gameObject; //놓일 슬롯에 있던 아이템
            //GetChild로 안하고 인스펙터에 ItemUI를 할당하게 수정하기, ItemUI를 Items의 자식오브젝트로 둬서 SetAsLastSibling사용하기
            ItemUI droppedItemUI = slotUI._itemUI;


            if (droppedSlotUI._SlotType == eSLOTTYPE.None || _beginDragSlotUI._SlotType.ToString() == droppedItemUI._Category.ToString())
                SwapItem(_draggedItemUI, droppedItemUI);
            else
                ResetDraggedSlotPosition();
        }
        else
        {
            ResetDraggedSlotPosition();
        }
    }

    private void ResetDraggedSlotPosition()
    {
        _draggedItem.transform.position = _beginDragItemPos;
    }


    void SwapItem(ItemUI dragItem, ItemUI dropItem)
    {


        CItem dragItemInfo = dragItem._Item;
        CItem dropItemInfo = dropItem._Item;

        int index = 0;
        int count = 0;
        foreach (ItemUI itemUI in _equipedItemUI)
        {
            if (itemUI == dragItem)
                index = count;
            else
                count++;
        }
        count = 0;
        foreach (ItemUI itemUI in _equipedItemUI)
        {
            if (itemUI == dropItem)
                index = count;
            else
                count++;
        }

        dragItem.SetItemSlot(dropItemInfo);
        dropItem.SetItemSlot(dragItemInfo);
        SetSlotIcon(dropItem);
        SetSlotIcon(dragItem);

        UpdateCurWeaphons();
    }
    void SetSlotIcon(ItemUI itemUI)
    {
        Sprite icon = null;
        if (itemUI._Item != null)
        {
            foreach (Sprite sprite in _itemIcons)
            {
                if (sprite.name == itemUI._Item._Pre_Name)
                {
                    icon = sprite;
                    break;
                }
            }
            if (icon != null)
                itemUI.SetIcon(icon);
        }
        else
            itemUI.SetIcon(null);

    }
    void UpdateCurWeaphons()
    {

        for (int i = 0; i < _equipedItemUI.Length; i++)
        {
            _curWeaphon[i] = _equipedItemUI[i]._Item;

        }
        _weaponFactory.SetCurWeaphons(_curWeaphon);
    }
    void SetItemList()
    {
        _equipedItems.Clear();
        _gotItems.Clear();
        foreach (ItemUI itemUI in _equipedItemUI)
        {
            if (itemUI._Item != null)
                _equipedItems.Add(itemUI._Item);
        }
        foreach (ItemUI itemUI in _gotItemUI)
        {
            if (itemUI != null)
                _gotItems.Add(itemUI._Item);
        }
    }
    public void GetItem(CItem item)
    {
        for (int i = 0; i < _gotItemUI.Length; i++)
        {
            if (_gotItemUI[i]._Item == null)
            {
                _gotItemUI[i].SetItemSlot(item);
                SetSlotIcon(_gotItemUI[i]);
                break;
            }
        }
        SetItemList();
    }
}
