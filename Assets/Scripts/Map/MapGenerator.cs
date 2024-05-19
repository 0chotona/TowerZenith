using System.Collections;
using System.Collections.Generic;
using Unity.AI.Navigation;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    CMap _map;
    List<CMapNode> _mapNodes = new List<CMapNode>();

    [Header("시작 방 오브젝트"), SerializeField] GameObject _startRoomObj;
    [Header("보물 방 오브젝트"), SerializeField] GameObject _treasureRoomObj;
    [Header("상점 방 오브젝트"), SerializeField] GameObject _storeRoomObj;
    [Header("보스 방 오브젝트"), SerializeField] GameObject _bossRoomObj;

    [Header("방 오브젝트"), SerializeField] GameObject[] _mapObjs;
    [Header("방 간격"), SerializeField] float _mapOffset = 10;
    [Header("방 갯수"), SerializeField] int _mapSize = 10;

    [Header("보물 생성기"),SerializeField] TreasureSpanwer _treasureSpanwer;
    [Header("미니맵 생성기"),SerializeField] MiniMapGenerator _miniMapGenerator;

    [SerializeField] Transform _enemySpawnerTrs;

    CMapNode _startNode;
    GameObject _startMap;

    List<MapInfo> _treasureMaps = new List<MapInfo>();
    List<MapInfo> _defalutMaps = new List<MapInfo>();

    private void Awake()
    {
        _map = new CMap();
        _mapNodes = _map.GetMapData(_mapSize, _mapObjs.Length);
        _miniMapGenerator.SetExistPoses(_mapNodes);

        _startNode = _mapNodes[0];
        SpawnMap();
        SpawnBossMap();
    }
    void SpawnMap()
    {
        Vector3 spawnPos = Vector3.zero;
        for (int i = 0; i < _mapNodes.Count; i++)
        {
            ROOMTYPE roomType = _mapNodes[i]._roomType;
            spawnPos = new Vector3(_mapNodes[i]._mapPos.x, 0, _mapNodes[i]._mapPos.y) * _mapOffset;

            GameObject spawnMapObj = null;
            

            if(roomType == ROOMTYPE.Start)
            {
                spawnMapObj = _startRoomObj;
            }
            if (roomType == ROOMTYPE.Default)
            {
                spawnMapObj = _mapObjs[_mapNodes[i]._MapIndex];
                
            }
            else if(roomType == ROOMTYPE.Store)
            {
                spawnMapObj = _storeRoomObj;
            }
            else if (roomType == ROOMTYPE.Treasure)
            {
                spawnMapObj = _treasureRoomObj;
            }
            else if (roomType == ROOMTYPE.Boss)
            {
                spawnMapObj = _bossRoomObj;
            }
            GameObject map = Instantiate(spawnMapObj, spawnPos, Quaternion.identity, transform);

            MapInfo mapInfo = map.GetComponent<MapInfo>();
            mapInfo._maxMapIndex = _mapSize;

            mapInfo.SetMapNode(_mapNodes[i]);
            mapInfo.SetEnemySpawner(_enemySpawnerTrs);
            if(roomType == ROOMTYPE.Default)
            {
                float rndNum = Random.value;
                if(rndNum < 1f)
                {
                    _defalutMaps.Add(mapInfo);
                    mapInfo._isHasTreasure = true;
                }
            }
            else if(roomType == ROOMTYPE.Treasure)
            {
                _treasureMaps.Add(mapInfo);
                mapInfo._isHasTreasure = true;
            }
            else if(roomType == ROOMTYPE.Boss)
            {
                _defalutMaps.Add(mapInfo);
                mapInfo._isHasTreasure = true;
            }


        }

        _treasureSpanwer.StartCoroutine(_treasureSpanwer.CRT_SpawnTreasure(_defalutMaps, false));
        _treasureSpanwer.StartCoroutine(_treasureSpanwer.CRT_SpawnTreasure(_treasureMaps, true));

        NavMeshSurface surface = GetComponent<NavMeshSurface>();
        surface.RemoveData();
        surface.BuildNavMesh();
    }
    void SpawnBossMap()
    {
        Debug.Log(_map.GetFarthestNode(_mapNodes, _startNode)._index);
    }
}
