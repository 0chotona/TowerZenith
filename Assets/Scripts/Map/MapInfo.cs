using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MapInfo : MonoBehaviour
{
    int _mapIndex;
    int[] _neighbors = new int[4];

    [Header("위, 오른쪽, 아래, 왼쪽"),SerializeField] Transform[] _doorFrameTrs;

    [SerializeField] GameObject _doorFrameObj;
    [SerializeField] GameObject _wallObj;

    [SerializeField] GameObject _doorObj;
    List<GameObject> _doors = new List<GameObject>();
    List<Vector3> _doorPos = new List<Vector3>();

    [SerializeField] GameObject[] _enemyObjs;
    [SerializeField] Transform[] _enemySpawnTrs;

    [Header("보물 위치"),SerializeField] Transform _treasurePos;

    [Header("포탈"), SerializeField] GameObject _portal;
    public Transform _TreasurePos => _treasurePos;

    List<GameObject> _spawnedEnemies = new List<GameObject>();

    public bool _isFighting = false;
    public bool _isCleared = false;
    public bool _isExistPlayer = false;

    public bool _isFightingRoom = false;
    public bool _isHasTreasure = false;

    float _existTimer = 0;
    float _standardTime = 3;

    float _doorSpeed = 3;

    EnemySpawner _enemySpawner;

    CMapNode _mapNode;

    [SerializeField] ROOMTYPE _roomType;

    [SerializeField] int[] _neighborIndexes;
    [SerializeField] int _mapIndexes;

    public int _maxMapIndex = 0;
    
    private void Start()
    {
        _roomType = _mapNode._roomType;
        _isFightingRoom = (_roomType == ROOMTYPE.Default || _roomType == ROOMTYPE.Boss) ? true : false;
        if(_portal != null)
            _portal.SetActive(false);
    }
    private void Update()
    {
        if (_isExistPlayer && _isFightingRoom)
        {
            if (!_isFighting && !_isCleared)
            {
                _isFighting = true;
                _spawnedEnemies = _enemySpawner.GetSpawnedEnemies(_enemyObjs, _enemySpawnTrs);
            }
            else if (_isFighting && !_isCleared)
                CloseDoor();
            else if (!_isFighting && _isCleared)
            {
                OpenDoor();
                if(_isHasTreasure && _treasurePos.GetChild(0) != null)
                {
                    _treasurePos.GetChild(0).gameObject.SetActive(true);
                    _isHasTreasure = false;
                }
            }

            _spawnedEnemies.RemoveAll(enemy => enemy == null);

            if (_isFighting && _spawnedEnemies.Count <= 0)
            {
                GetComponent<Collider>().enabled = true;
                _isCleared = true;
                _isFighting = false;
                if(_roomType == ROOMTYPE.Boss && _portal != null)
                {
                    _portal.SetActive(true);
                }
            }
        }
    }
    public void SetMapNode(CMapNode mapNode)
    {
        _mapNode = mapNode;
        _mapIndex = mapNode._index;
        SpawnDoor();

        _neighborIndexes = new int[4];
        for (int i = 0; i < 4; i++)
        {
            if (_mapNode._neighbors[i] != null)
                _neighborIndexes[i] = _mapNode._neighbors[i]._index;
            else
                _neighborIndexes[i] = -1;
        }
        _mapIndexes = _mapNode._index;
    }
    void SpawnDoor()
    {
        for(int i = 0; i < _mapNode._neighbors.Count; i++)
        {
            GameObject spawnObj = (_mapNode._neighbors[i] != null) ? _doorFrameObj : _wallObj;
            /*
            if (_mapNode._neighbors[i] != null && _mapNode._neighbors[i]._index == _maxMapIndex)
                continue;
            */
            Instantiate(spawnObj, _doorFrameTrs[i].position, _doorFrameTrs[i].rotation, _doorFrameTrs[i]);

            if (_mapNode._neighbors[i] != null)
            {
                GameObject door = Instantiate(_doorObj, _doorFrameTrs[i].position + new Vector3(0, 4.5f, 0), _doorFrameTrs[i].rotation);
                _doors.Add(door);
                _doorPos.Add(door.transform.position);
            }
        }
        
    }
    
    public void SetEnemySpawner(Transform trs)
    {
        _enemySpawner = trs.GetComponent<EnemySpawner>();
    }
    void CloseDoor()
    {
        for (int i = 0; i < _doors.Count; i++)
        {
            _doors[i].transform.position = Vector3.MoveTowards(_doors[i].transform.position,
                _doorPos[i] + new Vector3(0, -4.5f, 0), _doorSpeed * Time.deltaTime);
        }
    }
    void OpenDoor()
    {
        for (int i = 0; i < _doors.Count; i++)
        {
            _doors[i].transform.position = Vector3.MoveTowards(_doors[i].transform.position,
                _doorPos[i], _doorSpeed * Time.deltaTime);
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if(other.CompareTag("Player") && !_isExistPlayer)
        {
            _existTimer += Time.deltaTime;
            if(_existTimer >= _standardTime)
            {
                _isExistPlayer = true;
                GetComponent<Collider>().enabled = false;
            }
            
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
            _isExistPlayer = false;
    }

}
