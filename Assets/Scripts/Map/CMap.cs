using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
public enum ROOMTYPE
{
    Start,
    Default,
    Store,
    Treasure,
    Boss
}
public class CMapNode
{
    public ROOMTYPE _roomType; // 방의 종류 (시작 방, 전투 방, 보물 방, 보스 방 등)
    public int _MapIndex { get; private set; } // 전투 방 종류

    public CMapNode(int mapIdx, ROOMTYPE roomType) { this._MapIndex = mapIdx; this._roomType = roomType; }

    public CMapNode _left = null; // 상하좌우 방 노드
    public CMapNode _right = null;
    public CMapNode _up = null;
    public CMapNode _down = null;

    public List<CMapNode> _neighbors; // 상하좌우 방 리스트

    public int _index; // 방 번호
    public Vector2 _mapPos; // 방의 위치 좌표
   
}
public class CMap
{

    int _treasureRoomIndex = 4;
    int _storeRoomIndex = 6;

    public CMapNode _startNode = null;
    List<CMapNode> _mapNodes;
    int _index;

    List<Vector2> _existPoses = new List<Vector2>();
    public List<Vector2> _ExistPoses => _existPoses;
    Vector2[] _dirs = new Vector2[4] { new Vector2(0,1), new Vector2(1, 0) , new Vector2(0, -1) , new Vector2(-1, 0) };
    
    public CMap()
    {
        _mapNodes = new List<CMapNode>();
    }
    public List<CMapNode> GetMapData(int size, int roomCount) // 방 갯수, 방 종류 갯수
    {
        _existPoses.Add(Vector2.zero);

        // 전투 방 종류 랜덤 설정
        List<int> rndList = new List<int>();
        for (int i = 0; i < size; i++)
        {
            int num = Random.Range(0, roomCount);
            rndList.Add(num);
        }
        // 방 리스트 생성
        for (int i = 0; i < rndList.Count; i++)
        {
            if (i == _treasureRoomIndex)
                AddData(-1, ROOMTYPE.Treasure);
            else if (i == _storeRoomIndex)
                AddData(-1, ROOMTYPE.Store);
            else if(i == 0)
                AddData(rndList[i], ROOMTYPE.Start);
            else
                AddData(rndList[i], ROOMTYPE.Default);
        }
        // 이웃 노드 설정
        foreach (CMapNode mapNode in _mapNodes)
            SetNeighborNode(mapNode);

        // 보스방 생성
        LinkBossRoomNodes();

        return _mapNodes;
    }
    public void AddData(int mapIdx, ROOMTYPE roomType) { AddData(new CMapNode(mapIdx, roomType)); }
    void AddData(CMapNode mapNode)
    {
        
        SetNeighbor(mapNode);
        mapNode._index = _index;
        _index++;
        LinkMapNodes(mapNode);
        _mapNodes.Add(mapNode);
    }
    void LinkMapNodes(CMapNode mapNode)
    {

        if (_mapNodes.Count == 0)
        {
            _startNode = mapNode;
            mapNode._mapPos = Vector2.zero;
        }
        else if(_mapNodes.Count > 0)
        {
            

            int rndIdx = Random.Range(0,_mapNodes.Count); //현재 생성된 맵 개수
            while(!CanAddNeighbor(_mapNodes[rndIdx]))
                rndIdx = Random.Range(0, _mapNodes.Count);


            int rndDir = Random.Range(0, mapNode._neighbors.Count); //4
            while(!CanAddNeighbor(_mapNodes[rndIdx], rndDir))
                rndDir = Random.Range(0, mapNode._neighbors.Count);

            _mapNodes[rndIdx]._neighbors[rndDir] = mapNode;
            int oppoDir = rndDir + 2;
            if (oppoDir > 3)
                oppoDir -= 4;
            mapNode._neighbors[oppoDir] = _mapNodes[rndIdx];

            Vector2 dir = Vector2.zero;
            switch(rndDir)
            {
                case 0:
                    dir = new Vector2(0, 1);
                    break;
                case 1:
                    dir = new Vector2(1, 0);
                    break;
                case 2:
                    dir = new Vector2(0, -1);
                    break;
                case 3:
                    dir = new Vector2(-1, 0);
                    break;
            }
            mapNode._mapPos = _mapNodes[rndIdx]._mapPos + dir;
            _existPoses.Add(mapNode._mapPos);
        }
    }
    void LinkBossRoomNodes()
    {
        CMapNode farthestNode = GetFarthestNode(_mapNodes, _startNode);
        int rndDir = Random.Range(0, farthestNode._neighbors.Count);
        while (!CanAddNeighbor(farthestNode, rndDir))
            rndDir = Random.Range(0, farthestNode._neighbors.Count);

        CMapNode bossMapNode = new CMapNode(-1, ROOMTYPE.Boss);

        SetNeighbor(bossMapNode);
        bossMapNode._index = _index;
        _index++;
        //LinkMapNodes(bossMapNode);

        farthestNode._neighbors[rndDir] = bossMapNode;
        int oppoDir = rndDir + 2;
        if (oppoDir > 3)
            oppoDir -= 4;
        bossMapNode._neighbors[oppoDir] = farthestNode;

        Vector2 dir = Vector2.zero;
        switch (rndDir)
        {
            case 0:
                dir = new Vector2(0, 1);
                break;
            case 1:
                dir = new Vector2(1, 0);
                break;
            case 2:
                dir = new Vector2(0, -1);
                break;
            case 3:
                dir = new Vector2(-1, 0);
                break;
        }
        bossMapNode._mapPos = farthestNode._mapPos + dir;

        SetNeighborNode(bossMapNode);
        _mapNodes.Add(bossMapNode);
        _existPoses.Add(bossMapNode._mapPos);
    }
    bool CanAddNeighbor(CMapNode mapNode)
    {
        int i = 0;
        foreach(CMapNode map in mapNode._neighbors)
        {
            if (map == null && !_existPoses.Contains(mapNode._mapPos + _dirs[i]))
            {
                i++;
                return true;
            }
        }
        return false;
    }
    bool CanAddNeighbor(CMapNode mapNode, int index)
    {
        if (mapNode._neighbors[index] == null && !_existPoses.Contains(mapNode._mapPos + _dirs[index])) 
            return true;
        else 
            return false;
    }
    void SetNeighbor(CMapNode mapNode)
    {
        mapNode._neighbors = new List<CMapNode> { mapNode._up, mapNode._right, mapNode._down, mapNode._left };
    }
    void SetNeighborNode(CMapNode mapNode)
    {
        mapNode._up = mapNode._neighbors[0];
        mapNode._right = mapNode._neighbors[1];
        mapNode._down = mapNode._neighbors[2];
        mapNode._left = mapNode._neighbors[3];
    }

    public CMapNode GetFarthestNode(List<CMapNode> mapNodes, CMapNode startNode)
    {
        Dictionary<CMapNode, float> distances = new Dictionary<CMapNode, float>();

        Queue<CMapNode> queue = new Queue<CMapNode>();
        queue.Enqueue(startNode);
        distances[startNode] = 0f;

        while (queue.Count > 0)
        {
            CMapNode currentNode = queue.Dequeue();

            foreach (CMapNode neighbor in currentNode._neighbors)
            {
                if(neighbor != null)
                {
                    if (!distances.ContainsKey(neighbor))
                    {
                        distances[neighbor] = distances[currentNode] + 1;
                        queue.Enqueue(neighbor);
                    }
                }
                
            }
        }

        CMapNode farthestNode = startNode;
        float maxDistance = 0f;
        foreach (var kvp in distances)
        {
            if (kvp.Value > maxDistance)
            {
                farthestNode = kvp.Key;
                maxDistance = kvp.Value;
            }
        }

        

        return farthestNode;
    }

}
