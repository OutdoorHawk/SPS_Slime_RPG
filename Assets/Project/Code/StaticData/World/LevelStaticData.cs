using Project.Code.StaticData.Units;
using UnityEngine;

namespace Project.Code.StaticData.World
{
    [CreateAssetMenu(fileName = "LevelStaticData", menuName = "Static Data/LevelStaticData")]
    public class LevelStaticData : ScriptableObject
    {
        [SerializeField] private EnemyStaticData _bossStaticData;
        [SerializeField] private float _damageIncrease = 10f;
        [SerializeField] private float _healthIncrease = 50;
        [SerializeField] private int _moneyIncrease = 5;
        [SerializeField] private int _maxFightsOnLevel = 3;

        public float DamageIncrease => _damageIncrease;
        public float HealthIncrease => _healthIncrease;
        public int MoneyIncrease => _moneyIncrease;
        public int MaxFightsOnLevel => _maxFightsOnLevel;
        public EnemyStaticData BossStaticData => _bossStaticData;

        public void ResetToDefaultValues()
        {
            _damageIncrease = 4;
            _healthIncrease = 10;
            _moneyIncrease = 5;
        }

        public void IncreaseValue(float configDamageMultiplier, float configHealthMultiplier, float configMoneyMultiplier)
        {
            _damageIncrease *= configDamageMultiplier;
            _healthIncrease *= configHealthMultiplier;
            _moneyIncrease = (int)(configMoneyMultiplier * _moneyIncrease);
        }
    }
}