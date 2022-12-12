using System;
using Project.Code.Infrastructure.Services.Factory;
using Project.Code.Infrastructure.Services.SaveLoadService.Progress;
using Project.Code.Infrastructure.Services.StaticData;
using Project.Code.Runtime.Units.EnemyUnit;
using Project.Code.StaticData.World;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Project.Code.Runtime.World
{
    public class EnemySpawner : MonoBehaviour
    {
        private event Action OnWaveKilled;

        private IUnitFactory _unitFactory;
        private RectTransform _hpPanel;
        private int _enemyAmount;
        private LevelStaticData _levelStaticData;

        public void Init(IUnitFactory unitFactory, RectTransform hpPanel, LevelStaticData levelStaticData)
        {
            _levelStaticData = levelStaticData;
            _hpPanel = hpPanel;
            _unitFactory = unitFactory;
            UnitCollector.OnEnemyRemoved += CheckEnemiesLeft;
        }

        public void SpawnWave(Action onWaveKilled)
        {
            OnWaveKilled = onWaveKilled;
            _enemyAmount = Random.Range(_levelStaticData.MinEnemiesOnFight, _levelStaticData.MaxFightsOnLevel);

            for (int i = 0; i < _enemyAmount; i++)
                _unitFactory.SpawnEnemy(transform.position, transform.rotation, _hpPanel);
        }

        private void CheckEnemiesLeft()
        {
            if (UnitCollector.AliveEnemies.Count == 0)
                OnWaveKilled?.Invoke();
        }

        public void SpawnBoss(Action onBossDefeated)
        {
            OnWaveKilled = onBossDefeated;
            _unitFactory.SpawnBoss(transform.position, transform.rotation, _hpPanel);
        }

        private void OnDestroy()
        {
            UnitCollector.OnEnemyRemoved -= CheckEnemiesLeft;
        }
    }
}