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

        public IEnumerable<WindowConfig> Windows => _windows;

        public UnitConfig[] Units => _units;
        public LevelStaticData[] LevelsStaticData => _levelsStaticData;
        public WorldStaticData WorldStaticData => _worldStaticData;

        [ContextMenu("EraseProgress")]
        private void EraseProgress() => PlayerPrefs.DeleteAll();
        
        [ContextMenu("LoadLevelDataMultiplier")]
        private void LoadLevelDataMultiplier()
        {
            for (int i = 1; i < _levelsStaticData.Length; i++)
            {
                EnemyMultipliers multipliers = _levelsStaticData[i-1].Multipliers;
                EnemyMultipliers updatedMultipliers = new EnemyMultipliers
                {
                    DamageMultiplier = multipliers.DamageMultiplier * 1.3f,
                    HealthMultiplier = multipliers.HealthMultiplier * 1.5f,
                    MoneyMultiplier = multipliers.MoneyMultiplier * 1.1f
                };
                
                _levelsStaticData[i].UpdateMultipliers(updatedMultipliers);
            }
        }
        
        [ContextMenu("SpeedUP")]
        private void SpeedUP()
        {
            Time.timeScale = Time.timeScale < 2 ? 2 : 1;
        }

    }
}