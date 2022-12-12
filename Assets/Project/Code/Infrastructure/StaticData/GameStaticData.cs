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
            
            /*EnemyMultipliers multipliersForLevelData = new EnemyMultipliers()
            {
                DamageMultiplier = 
            }*/
        }

     
        [ContextMenu("SpeedUP")]
        private void SpeedUP()
        {
            Time.timeScale = Time.timeScale < 2 ? 2 : 1;
        }

    }
}