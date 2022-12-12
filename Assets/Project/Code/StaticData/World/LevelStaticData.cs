using Project.Code.StaticData.Units;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Project.Code.StaticData.World
{
    [CreateAssetMenu(fileName = "LevelStaticData", menuName = "Static Data/LevelStaticData")]
    public class LevelStaticData : ScriptableObject
    {
        [SerializeField] private EnemyMultipliers _multipliers;
        [SerializeField] private int _maxFightsOnLevel = 3;
        
        [SerializeField] private EnemyStaticData[] _enemiesStaticData;
        [SerializeField] private EnemyStaticData _bossStaticData;

        public int MaxFightsOnLevel => _maxFightsOnLevel;

        public void UpdateMultipliers()
        {
            
        }

        public EnemyStaticData GetEnemyStaticData()
        {
            EnemyStaticData staticData = _enemiesStaticData[Random.Range(0, _enemiesStaticData.Length - 1)];
            staticData.UpdateEnemyStats(_multipliers);
            return staticData;
        }
        
        public EnemyStaticData GetBossStaticData()
        {
            _bossStaticData.UpdateEnemyStats(_multipliers);
            return _bossStaticData;
        }
        
    }
}