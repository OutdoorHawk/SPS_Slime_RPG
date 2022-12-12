using System.Collections.Generic;
using Project.Code.StaticData;
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
        private void LoadLevelStaticData()
        {
            foreach (var config in _levelsStaticData) 
                config.LevelStaticData.IncreaseValue(config.StatMultiplier);
        } 
        
        [ContextMenu("ResetLevelStaticData")]
        private void ResetLevelStaticData()
        {
            foreach (var config in _levelsStaticData) 
                config.LevelStaticData.ResetToDefaultValues();
        }
    }
}