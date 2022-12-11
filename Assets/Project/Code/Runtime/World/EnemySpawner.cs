using System;
using System.Collections.Generic;
using Project.Code.Infrastructure.Services.Factory;
using Project.Code.Runtime.Units.EnemyUnit;
using Project.Code.Runtime.Units.PlayerUnit;
using Project.Code.StaticData.World;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Project.Code.Runtime.World
{
    public class EnemySpawner : MonoBehaviour
    {
        public event Action OnWaveKilled;
        
        [SerializeField] private EnemySpawnerStaticData _spawnerStaticData;

        private IUnitFactory _unitFactory;
        private int _enemyAmount;

        public void Init(IUnitFactory unitFactory)
        {
            _unitFactory = unitFactory;
        }

        public void SpawnWave()
        {
            _enemyAmount = Random.Range(_spawnerStaticData.EnemiesMinAmount, _spawnerStaticData.EnemiesMaxAmount);

            for (int i = 0; i < _enemyAmount; i++)
            {
                Enemy enemy = _unitFactory.SpawnEnemy(transform.position, transform.rotation);
                enemy.OnUnitDead += CheckEnemiesLeft;
            }
        }

        private void CheckEnemiesLeft(BaseUnit enemy)
        {
            enemy.OnUnitDead -= CheckEnemiesLeft;
            if (UnitCollector.AliveEnemies.Count == 0) 
                OnWaveKilled?.Invoke();
        }

  
    }
}