using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class WeaphonFactory : MonoBehaviour
{
    [SerializeField] List<GameObject> _oneHands;
    [SerializeField] List<GameObject> _shields;
    [SerializeField] List<GameObject> _twoHands;
    [SerializeField] List<GameObject> _bows;
    [SerializeField] List<GameObject> _artifacts;

    [SerializeField] GameObject _arrows;

    [SerializeField] Transform _oneHandPivot;
    [SerializeField] Transform _twoHandPivot;
    [SerializeField] Transform _shieldPivot;
    [SerializeField] Transform _bowPivot;
    [SerializeField] Transform _arrowPivot;

    [SerializeField] PlayerAttack _playerAttack;
    [SerializeField] BuffController _buffController;
    [SerializeField] SynergyController _synergyController;

    int _inputNum = 1;

    List<GameObject> _itemModels = new List<GameObject>();
    public List<GameObject> _ItemModels => _itemModels;


    List<GameObject> _curWeaphon = new List<GameObject>();

    List<int> _gottedItemID = new List<int>();

    [SerializeField] PlayerStatus _status;
    CItem[] _equippedWeaphons;
    private void Start()
    {
        
        

        _itemModels.AddRange(_oneHands);
        _itemModels.AddRange(_shields);
        _itemModels.AddRange(_twoHands);
        _itemModels.AddRange(_bows);
        _itemModels.AddRange(_artifacts);

        _inputNum = 1;
        
    }
    private void Update()
    {
        UpdateWeaphonType();
    }
    public void AwakeSetting()
    {
        _playerAttack.UpdateWeaphonType(1);
        DespawnWeaphon();
        DestroyArrow();

        SpawnWeaphon(_equippedWeaphons[(int)eCATEGORY.One_Hand]);
        SpawnWeaphon(_equippedWeaphons[(int)eCATEGORY.Shield]);
        _status.SetWeaphonStat(_equippedWeaphons[(int)eCATEGORY.One_Hand]._Att_Power, _equippedWeaphons[(int)eCATEGORY.One_Hand]._Att_Speed);

        _status.SetShieldStat(_equippedWeaphons[(int)eCATEGORY.Shield]._Def);
        _status.AddWeaphonStat();
        AddItemSynergy();
    }
    public void SetCurWeaphons(CItem[] curItems)
    {
        DespawnWeaphon();
        _equippedWeaphons = curItems;
        switch(_inputNum)
        {
            case 1:
                SpawnWeaphon(_equippedWeaphons[(int)eCATEGORY.One_Hand]);
                SpawnWeaphon(_equippedWeaphons[(int)eCATEGORY.Shield]);
                break;
            case 2:
                SpawnWeaphon(_equippedWeaphons[(int)eCATEGORY.Two_Hand]);
                break;
            case 3:
                SpawnWeaphon(_equippedWeaphons[(int)eCATEGORY.Bow]);
                break;
        }
    }
    public void UpdateWeaphonType()
    {
        //무기 변경시 스탯 적용
        if(!_playerAttack._isAttack)
        {
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                _playerAttack.ResetAllCombo();
                _buffController.ResetBuff();
                _status.ResetBuffStat();
                _status.ResetWeaphonStat();

                _inputNum = 1;
                _playerAttack.UpdateWeaphonType(_inputNum);
                DespawnWeaphon();
                DestroyArrow();

                CItem oneHand = _equippedWeaphons[(int)eCATEGORY.One_Hand];
                CItem shield = _equippedWeaphons[(int)eCATEGORY.Shield];

                if(oneHand != null)
                {
                    SpawnWeaphon(oneHand);
                    _status.SetWeaphonStat(oneHand._Att_Power, oneHand._Att_Speed);
                    _buffController.SetBuff(oneHand._Buff._BuffType);
                }
                if(shield != null)
                {
                    SpawnWeaphon(shield);

                    _status.SetShieldStat(shield._Def);
                    _buffController.SetBuff(shield._Buff._BuffType);
                }

                SetArtifactBuff();
                _status.AddWeaphonStat();
                AddItemSynergy();

            }
            if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                _playerAttack.ResetAllCombo();
                _buffController.ResetBuff();
                _status.ResetBuffStat();
                _status.ResetWeaphonStat();

                _inputNum = 2;
                _playerAttack.UpdateWeaphonType(_inputNum);
                DespawnWeaphon();
                DestroyArrow();

                CItem twoHand = _equippedWeaphons[(int)eCATEGORY.Two_Hand];

                if(twoHand != null)
                {
                    SpawnWeaphon(twoHand);
                    _status.SetWeaphonStat(twoHand._Att_Power, twoHand._Att_Speed);
                    _buffController.SetBuff(twoHand._Buff._BuffType);
                }
                
                SetArtifactBuff();
                _status.AddWeaphonStat();
                AddItemSynergy();
            }

            if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                _playerAttack.ResetAllCombo();
                _buffController.ResetBuff();
                _status.ResetBuffStat();
                _status.ResetWeaphonStat();

                _inputNum = 3;
                _playerAttack.UpdateWeaphonType(_inputNum);
                DespawnWeaphon();
                DestroyArrow();

                CItem bow = _equippedWeaphons[(int)eCATEGORY.Bow];

                if(bow != null)
                {
                    SpawnWeaphon(bow);

                    _status.SetWeaphonStat(bow._Att_Power);
                    _buffController.SetBuff(bow._Buff._BuffType);
                }

                SetArtifactBuff();
                _status.AddWeaphonStat();
                AddItemSynergy();


            }

            
        }
        
    }
    void AddItemSynergy()
    {
        _gottedItemID.Clear();
        switch (_inputNum)
        {
            case 1:
                if(_equippedWeaphons[(int)eCATEGORY.One_Hand] != null)
                    _gottedItemID.Add(_equippedWeaphons[(int)eCATEGORY.One_Hand]._ID);
                if (_equippedWeaphons[(int)eCATEGORY.Shield] != null)
                    _gottedItemID.Add(_equippedWeaphons[(int)eCATEGORY.Shield]._ID);
                break;
            case 2:
                if (_equippedWeaphons[(int)eCATEGORY.Two_Hand] != null)
                    _gottedItemID.Add(_equippedWeaphons[(int)eCATEGORY.Two_Hand]._ID);
                break;
            case 3:
                if (_equippedWeaphons[(int)eCATEGORY.Bow] != null)
                    _gottedItemID.Add(_equippedWeaphons[(int)eCATEGORY.Bow]._ID);
                break;
        }

        CItem artifact_1 = _equippedWeaphons[(int)eCATEGORY.Artifact];
        CItem artifact_2 = _equippedWeaphons[(int)eCATEGORY.Artifact + 1];
        if (artifact_1 != null)
            _gottedItemID.Add(artifact_1._ID);
        if (artifact_2 != null)
            _gottedItemID.Add(artifact_2._ID);

        _synergyController.SetSynergyItem(_gottedItemID);
    }
    void SetArtifactBuff()
    {
        CItem artifact_1 = _equippedWeaphons[(int)eCATEGORY.Artifact];
        CItem artifact_2 = _equippedWeaphons[(int)eCATEGORY.Artifact + 1];
        if (artifact_1 != null)
            _buffController.SetBuff(artifact_1._Buff._BuffType);
        if (artifact_2 != null)
            _buffController.SetBuff(artifact_2._Buff._BuffType);
    }
    public void SpawnWeaphon(CItem itemInfo)
    {
        if(itemInfo != null)
        {
            GameObject weaphon = GetObjByName(itemInfo._Pre_Name);
            weaphon.SetActive(true);

            _curWeaphon.Add(weaphon);
        }
        
    }
    public void SetChangedItemStat()
    {
        //인벤토리 변경된 아이템 스탯 적용
        _status.ResetWeaphonStat();
        _status.ResetBuffStat();
        _buffController.ResetBuff();
        /*
        240325 12:30 무기를 뺐을때 status의 weaphonAtt가 0으로 초기화 안되고 att도 줄어들지 않음, 다시 착용하면 더해짐\
        (공격력이 20이고 10인 무기를 장착해서 att가 30이 됐다가 무기를 빼도 30으로 남아있고 다시 10인 무기를 장착하면 40으로 상승함 디버깅 필요
        */
        CItem item1 = null;
        CItem item2 = null;
        switch (_inputNum)
        {
            case 1:
                item1 = _equippedWeaphons[(int)eCATEGORY.One_Hand];
                item2 = _equippedWeaphons[(int)eCATEGORY.Shield];

                if (item1 != null)
                {
                    _buffController.SetBuff(item1._Buff._BuffType);
                    _status.SetWeaphonStat(item1._Att_Power, item1._Att_Speed);
                }
                if (item2 != null)
                {
                    _buffController.SetBuff(item2._Buff._BuffType);
                    _status.SetShieldStat(item2._Def);
                }
                break;
            case 2:
                item1 = _equippedWeaphons[(int)eCATEGORY.Two_Hand];
                if (item1 != null)
                {
                    _buffController.SetBuff(item1._Buff._BuffType);
                    _status.SetWeaphonStat(item1._Att_Power, item1._Att_Speed);
                }
                break;
            case 3:
                item1 = _equippedWeaphons[(int)eCATEGORY.Bow];
                if (item1 != null)
                {
                    _buffController.SetBuff(item1._Buff._BuffType);
                    _status.SetWeaphonStat(item1._Att_Power);
                }
                break;
        
        }
        _status.AddWeaphonStat();
        SetArtifactBuff();
        AddItemSynergy();
    }
    void DespawnWeaphon()
    {
        List<GameObject> weaphonsToRemove = new List<GameObject>();
        foreach (GameObject weaphon in _curWeaphon)
        {
            weaphon.SetActive(false);
            weaphonsToRemove.Add(weaphon);
        }

        foreach (GameObject weaphonToRemove in weaphonsToRemove)
        {
            _curWeaphon.Remove(weaphonToRemove);
        }

    }
    void DestroyArrow()
    {
        foreach (Transform child in _arrowPivot)
            Destroy(child.gameObject);
    }
    GameObject GetObjByName(string name)
    {
        GameObject weaphon = null;
        foreach(GameObject obj in _oneHands)
        {
            if(obj.name == name)
                weaphon = obj;
        }
        foreach (GameObject obj in _twoHands)
        {
            if (obj.name == name)
                weaphon = obj;
        }
        foreach (GameObject obj in _shields)
        {
            if (obj.name == name)
                weaphon = obj;
        }
        foreach (GameObject obj in _bows)
        {
            if (obj.name == name)
                weaphon = obj;
        }
        return weaphon;
    }
}
