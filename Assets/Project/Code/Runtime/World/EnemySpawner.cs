using System.Collections.Generic;
using Project.Code.Runtime.Units.Enemy;
using Project.Code.Runtime.Units.Player;
using Project.Code.StaticData.Units;
using Project.Code.StaticData.World;
using UnityEngine;

namespace Project.Code.Runtime.World
{
    public class EnemySpawner : MonoBehaviour
    {
        [SerializeField] private EnemySpawnerStaticData _spawnerStaticData;

        private PlayerSlime _playerSlime;
        private List<Enemy> _aliveEnemies;
        private EnemyStaticData _enemyStaticData;

        private Enemy _enemyPrefab;

        private int _enemyAmount;

        public void Init(PlayerSlime playerSlime, UnitStaticData unitStaticData)
        {
            _enemyStaticData = unitStaticData as EnemyStaticData;
            _enemyPrefab = _enemyStaticData.UnitPrefab as Enemy;
            _playerSlime = playerSlime;
            _aliveEnemies = new List<Enemy>();
        }

        public void SpawnWave()
        {
            _enemyAmount = Random.Range(_spawnerStaticData.EnemiesMinAmount, _spawnerStaticData.EnemiesMaxAmount);

            for (int i = 0; i < _enemyAmount; i++)
            {
                Enemy enemy = Instantiate(_enemyPrefab, transform.position, transform.rotation);
                
            }
        }
    }
}