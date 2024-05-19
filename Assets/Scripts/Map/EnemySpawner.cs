using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MONSTERTYPE
{
    Knight,
    Ghost,
    Zoroark
}
public class EnemySpawner : MonoBehaviour
{
    [SerializeField] Transform _playerTrs;
    [SerializeField] CameraMove _camMove;
    public List<GameObject> GetSpawnedEnemies(GameObject[] enemyObjs, Transform[] spawnPoses)
    {
        List<GameObject> spawnedEnemies = new List<GameObject>();
        for(int i = 0; i < enemyObjs.Length; i++)
        {
            GameObject enemy = Instantiate(enemyObjs[i], spawnPoses[i].position, Quaternion.identity);
            spawnedEnemies.Add(enemy);

            EnemyMove enemyMove = enemy.GetComponentInChildren<EnemyMove>();

            IEnemyBehavior enemyBehavior = null;
            switch(enemyMove._monsterType)
            {
                case MONSTERTYPE.Knight:
                    Knight_Move knightMove = enemy.GetComponent<Knight_Move>();
                    enemyBehavior = knightMove;
                    knightMove.SetStat(new CEnemy("방탕 기사", 100, 10, 10, 3.5f));
                    break;
                case MONSTERTYPE.Ghost:
                    Ghost_Move ghost_Move = enemy.GetComponentInChildren<Ghost_Move>();
                    enemyBehavior = ghost_Move;
                    ghost_Move.SetStat(new CEnemy("미스마키", 150, 20, 5, 1f));
                    break;
                case MONSTERTYPE.Zoroark:
                    Zoroark_Move zoroark_Move = enemy.GetComponent<Zoroark_Move>();
                    enemyBehavior = zoroark_Move;
                    zoroark_Move.SetStat(new CEnemy("조로아크", 1000, 40, 20, 10));
                    zoroark_Move.SetCameraMove(_camMove);
                    break;
            }
            
            enemyMove.SetPlayerTrs(_playerTrs);

            
            enemyMove.SetEnemyBehavior(enemyBehavior);
        }
        return spawnedEnemies;
    }
}
