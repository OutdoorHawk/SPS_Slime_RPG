using System.Collections.Generic;
using Project.Code.Infrastructure.Services.Factory;
using Project.Code.Runtime.Units.Enemy;
using Project.Code.Runtime.Units.Player;
using Project.Code.StaticData.World;
using UnityEngine;

namespace Project.Code.Runtime.World
{
    public class EnemySpawner : MonoBehaviour
    {
        [SerializeField] private EnemySpawnerStaticData _spawnerStaticData;

        private IEnemyFactory _enemyFactory;
        private List<Enemy> _aliveEnemies;

        private int _enemyAmount;

        public void Init(IEnemyFactory enemyFactory)
        {
            _enemyFactory = enemyFactory;
            _aliveEnemies = new List<Enemy>();
        }

        public void SpawnWave()
        {
            _enemyAmount = Random.Range(_spawnerStaticData.EnemiesMinAmount, _spawnerStaticData.EnemiesMaxAmount);

            for (int i = 0; i < _enemyAmount; i++)
            {
                Enemy enemy = _enemyFactory.SpawnEnemy(transform.position, transform.rotation);
            }
        }
    }
}