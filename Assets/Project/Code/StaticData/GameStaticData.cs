using System.Collections.Generic;
using Project.Code.StaticData.Units;
using Project.Code.StaticData.World;
using UnityEngine;

namespace Project.Code.Infrastructure.StaticData
{
    [CreateAssetMenu(fileName = "GameStaticData", menuName = "Static Data/GameStaticData")]
    public class GameStaticData : ScriptableObject
    {
        [SerializeField] private WindowConfig[] _windows;
        [SerializeField] private UnitConfig[] _units;
        [SerializeField] private LevelStaticData[] _levelsStaticData;
        [SerializeField] private WorldStaticData _worldStaticData;
        
        [SerializeField] private float _damageLevelScale = 1.3f;
        [SerializeField] private float _healthLevelScale = 1.6f;
        [SerializeField] private float _moneyLevelScale = 1.3f;
        public IEnumerable<WindowConfig> Windows => _windows;

        public UnitConfig[] Units => _units;
        public LevelStaticData[] LevelsStaticData => _levelsStaticData;
        public WorldStaticData WorldStaticData => _worldStaticData;

        #region Editor

        [ContextMenu("EraseProgress")]
        private void EraseProgress() => PlayerPrefs.DeleteAll();

        [ContextMenu("LoadLevelDataMultiplier")]
        private void LoadLevelDataMultiplier()
        {
            for (int i = 1; i < _levelsStaticData.Length; i++)
            {
                EnemyMultipliers multipliers = _levelsStaticData[i - 1].Multipliers;
                EnemyMultipliers updatedMultipliers = new EnemyMultipliers
                {
                    DamageMultiplier = multipliers.DamageMultiplier * _damageLevelScale,
                    HealthMultiplier = multipliers.HealthMultiplier * _healthLevelScale,
                    MoneyMultiplier = multipliers.MoneyMultiplier * _moneyLevelScale
                };

                _levelsStaticData[i].UpdateMultipliers(updatedMultipliers);
            }
        }

        [ContextMenu("SpeedUP")]
        private void SpeedUP()
        {
            Time.timeScale = Time.timeScale < 2 ? 2 : 1;
        }

        #endregion
    }
}