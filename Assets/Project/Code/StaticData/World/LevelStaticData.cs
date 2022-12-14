using Project.Code.Runtime.Roads;
using Project.Code.StaticData.Units;
using UnityEngine;

namespace Project.Code.StaticData.World
{
    [CreateAssetMenu(fileName = "LevelStaticData", menuName = "Static Data/LevelStaticData")]
    public class LevelStaticData : ScriptableObject
    {
        [SerializeField] private EnemyMultipliers _multipliers;
        [SerializeField] private int _maxFightsOnLevel = 3;
        [SerializeField] private int _minEnemiesOnFight = 2;
        [SerializeField] private int _maxEnemiesOnFight = 3;

        [SerializeField] private EnemyStaticData[] _enemiesStaticData;
        [SerializeField] private EnemyStaticData _bossStaticData;
        [SerializeField] private Road[] _roads;

        public Road[] Roads => _roads;
        public EnemyMultipliers Multipliers => _multipliers;
        public int MaxFightsOnLevel => _maxFightsOnLevel;
        public int MaxEnemiesOnFight => _maxEnemiesOnFight;
        public int MinEnemiesOnFight => _minEnemiesOnFight;

        public void UpdateMultipliers(EnemyMultipliers multipliersForLevelData)
        {
            _multipliers = multipliersForLevelData;
        }

        public EnemyStaticData GetEnemyStaticData()
        {
            EnemyStaticData staticData = _enemiesStaticData[Random.Range(0, _enemiesStaticData.Length - 1)];
            staticData.UpdateEnemyStats(Multipliers);
            return staticData;
        }

        public EnemyStaticData GetBossStaticData()
        {
            _bossStaticData.UpdateEnemyStats(Multipliers);
            return _bossStaticData;
        }
    }
}