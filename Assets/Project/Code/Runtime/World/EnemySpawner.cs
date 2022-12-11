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

        private IUnitFactory _unitFactory;
        private List<Enemy> _aliveEnemies;

        private int _enemyAmount;

        public void Init(IUnitFactory unitFactory)
        {
            _unitFactory = unitFactory;
            _aliveEnemies = new List<Enemy>();
        }

        public void SpawnWave()
        {
            _enemyAmount = Random.Range(_spawnerStaticData.EnemiesMinAmount, _spawnerStaticData.EnemiesMaxAmount);

            for (int i = 0; i < _enemyAmount; i++)
            {
                Enemy enemy = _unitFactory.SpawnEnemy(transform.position, transform.rotation);
            }
        }
    }
}