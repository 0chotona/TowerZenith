using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniMapGenerator : MonoBehaviour
{
    List<Vector2> _existPoses = new List<Vector2>();
    List<CMapNode> _existMaps = new List<CMapNode>();

    [Header("콘텐츠"), SerializeField] Transform _contentTrs;

    [Header("미니맵 프리펩"), SerializeField] GameObject _miniMapPref;
    [Header("미니맵 간격"), SerializeField] float _offset = 150f;

    [Header("방 간격"), SerializeField] float _roomOffset = 43f;

    [Header("플레이어 시작 위치"), SerializeField] Vector2 _playerStartPos = new Vector2(21.5f, 21.5f);
    [Header("플레이어"), SerializeField] Transform _playerTrs;
    [Header("플레이어 아이콘"), SerializeField] GameObject _playerIcon;

    [Header("문 프리펩"), SerializeField] GameObject _doorPref;
    [SerializeField] Vector2 _centerPos;
    private void Start()
    {
        _centerPos = GetCenterPos();
        SpawnMiniMap();
        SpawnDoorIcon();

        gameObject.SetActive(false);
    }
    private void Update()
    {
        _playerIcon.transform.localPosition = GetPlayerIconPos();
        
    }
    public void SetExistPoses(List<CMapNode> existMaps)
    {
        _existMaps = existMaps;
        foreach (CMapNode map in _existMaps)
            _existPoses.Add(map._mapPos);
    }
    Vector2 GetCenterPos()
    {
        Vector2 centerMapPos = _existPoses[0];
        Vector2 sumPos = Vector2.zero;

        /*
        foreach(Vector2 p in _existPoses)
        {
            sumPos += p;
        }
        Vector2 centerPos = sumPos / _existPoses.Count;
        */
        float minX = _existPoses[0].x;
        float maxX = _existPoses[0].x;

        float minY = _existPoses[0].y;
        float maxY = _existPoses[0].y;

        foreach (Vector2 pos in _existPoses)
        {
            if(pos.x < minX)
                minX = pos.x;
            if(pos.y < minY)
                minY = pos.y;
            if(pos.x > maxX)
                maxX = pos.x;
            if( pos.y > maxY)
                maxY = pos.y;
        }
        Vector2 centerPos = new Vector2((minX + maxX) / 2, (minY + maxY) / 2);
        /*
        float nearestDist = Vector2.Distance(centerMapPos, centerPos);
        foreach (Vector2 p in _existPoses)
        {

            if (Vector2.Distance(p, centerPos) < nearestDist)
            {
                nearestDist = Vector2.Distance(p, sumPos/_existPoses.Count);
                centerMapPos = p;
            }
        }
        */
        return centerPos;
    }
    void SpawnMiniMap()
    {
        for(int i = 0; i < _existPoses.Count; i++)
        {
            GameObject mapPref = Instantiate(_miniMapPref, _existPoses[i] * _offset, Quaternion.identity, _contentTrs);
            mapPref.transform.localPosition = (_existPoses[i] - _centerPos) * _offset ;
            Debug.Log(_centerPos);
        }
    }
    Vector2 GetPlayerIconPos()
    {
        Vector2 playerIconPos = Vector2.zero;

        playerIconPos.x = _playerTrs.position.x * _offset / _roomOffset - _centerPos.x * _offset - _offset / 2;
        playerIconPos.y = _playerTrs.position.z * _offset / _roomOffset - _centerPos.y * _offset - _offset / 2;
        return playerIconPos;
    }
    void SpawnDoorIcon()
    {
        /*
        Vector2 neighborPos = Vector2.zero;
        
        for(int i = 0; i < _existMaps.Count; i++)
        {
            if (_existMaps[i]._up != null)
                neighborPos = new Vector2(0, 1f);
            if (_existMaps[i]._right != null)
                neighborPos = new Vector2(1f, 0f);
            if (_existMaps[i]._down != null)
                neighborPos = new Vector2(0, -1f);
            if (_existMaps[i]._left != null)
                neighborPos = new Vector2(-1f, 0f);
        }
        */
        Vector2 spawnPos = Vector2.zero;
        for (int i = 0; i < _existMaps.Count;i++)
        {
            for(int j = 0; j < _existMaps[i]._neighbors.Count; j++)
            {
                if (_existMaps[i]._neighbors[j] != null)
                {
                    spawnPos = ((_existMaps[i]._mapPos + _existMaps[i]._neighbors[j]._mapPos) / 2 - _centerPos) * _offset;
                    GameObject door = Instantiate(_doorPref, spawnPos, Quaternion.identity, _contentTrs);
                    door.transform.localPosition = spawnPos;
                }
            }
            
        }
        
    }
}
