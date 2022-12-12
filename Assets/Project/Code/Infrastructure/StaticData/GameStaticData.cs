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
        [SerializeField] private LevelConfig[] _levelsStaticData;
        [SerializeField] private WorldStaticData _worldStaticData;

        public IEnumerable<WindowConfig> Windows => _windows;

        public UnitConfig[] Units => _units;
        public LevelConfig[] LevelsStaticData => _levelsStaticData;
        public WorldStaticData WorldStaticData => _worldStaticData;

        [ContextMenu("EraseProgress")]
        private void EraseProgress() => PlayerPrefs.DeleteAll();
        
        [ContextMenu("LoadLevelDataMultiplier")]
        private void LoadLevelDataMultiplier()
        {
            ResetMultiplier();
            for (var i = 1; i < _levelsStaticData.Length; i++)
            {
                var config = _levelsStaticData[i];
                config.DamageMultiplier += _levelsStaticData[i - 1].DamageMultiplier;
                config.HealthMultiplier += _levelsStaticData[i - 1].HealthMultiplier;
                config.MoneyMultiplier += _levelsStaticData[i - 1].MoneyMultiplier;
            }
        }

        [ContextMenu("UpdateLevelsStaticData")]
        private void UpdateLevelsStaticData()
        {
            ResetToDefaultValues();
            foreach (var config in _levelsStaticData)
                config.LevelStaticData.IncreaseValue(config.DamageMultiplier, config.HealthMultiplier,
                    config.MoneyMultiplier);
        }

        private void ResetMultiplier()
        {
            for (var i = 1; i < _levelsStaticData.Length; i++)
            {
                var config = _levelsStaticData[i];
                config.DamageMultiplier = _levelsStaticData[0].DamageMultiplier;
                config.HealthMultiplier = _levelsStaticData[0].HealthMultiplier;
                config.MoneyMultiplier = _levelsStaticData[0].MoneyMultiplier;
            }
        }

        private void ResetToDefaultValues()
        {
            foreach (var config in _levelsStaticData)
                config.LevelStaticData.ResetToDefaultValues();
        }
        
        [ContextMenu("SpeedUP")]
        private void SpeedUP()
        {
            Time.timeScale = Time.timeScale < 2 ? 2 : 1;
        }

    }
}