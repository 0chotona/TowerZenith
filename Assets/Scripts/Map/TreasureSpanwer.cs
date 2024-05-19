using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreasureSpanwer : MonoBehaviour
{
    [SerializeField] WeaphonData _weaphonData;
    [SerializeField] WeaphonFactory _weaphonFactory;
    [SerializeField] Inventory _inventory;

    Dictionary<string, CItem> _itemDatas = new Dictionary<string, CItem>();
    [Header("아이템 모델링"), SerializeField] List<GameObject> _itemModels = new List<GameObject>();


    [SerializeField] GameObject _itemChestObj;
    CItem _rndItem;
    GameObject _rndItemObj;

    [SerializeField] SoundUI _soundUI;

    private void Start()
    {
        StartCoroutine(CRT_SetData());
    }
    IEnumerator CRT_SetData()
    {
        yield return new WaitForSeconds(1f);
        _itemDatas = new Dictionary<string, CItem>(_weaphonData._ItemDatas);
    }
    
    public IEnumerator CRT_SpawnTreasure(List<MapInfo> mapInfos, bool isTreasureRoom)
    {
        yield return new WaitForSeconds(2f);
        for(int i = 0; i < mapInfos.Count; i++)
        {
            _rndItem = GetRandomItem();
            string removeKey = null;
            foreach(var item in _itemDatas)
            {
                if (item.Value == _rndItem)
                {
                    removeKey = item.Key;
                    break;
                }
            }
            _itemDatas.Remove(removeKey);
            //_itemModels = _weaphonFactory._ItemModels;

            Transform spawnTrs = mapInfos[i]._TreasurePos;
            GameObject chest = Instantiate(_itemChestObj, spawnTrs.position, Quaternion.identity, spawnTrs);
            foreach (GameObject itemModel in _itemModels)
            {
                if (itemModel.name == _rndItem._Pre_Name)
                {
                    _rndItemObj = itemModel;
                }
            }
            Treasure_Item treasure = chest.GetComponent<Treasure_Item>();
            treasure.SetItem(_rndItemObj, _rndItem, _inventory);
            treasure.SetSoundUI(_soundUI);

            chest.SetActive(isTreasureRoom);
        }
        
    }
    CItem GetRandomItem()
    {

        List<string> keys = new List<string>(_itemDatas.Keys);
        int rndIndex = Random.Range(0, keys.Count);
        string rndKey = keys[rndIndex];

        CItem rndItem = _itemDatas[rndKey];
        return rndItem;
    }
}
