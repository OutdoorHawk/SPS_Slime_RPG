using Project.Code.StaticData.World;
using UnityEngine;

namespace Project.Code.StaticData.Units
{
    [CreateAssetMenu(fileName = "EnemyStaticData", menuName = "Static Data/EnemyStaticData")]
    public class EnemyStaticData : UnitStaticData
    {
        [SerializeField] private float _attackSpeedBase = 1f;
        [SerializeField] private float _damageAmountBase = 10f;
        [SerializeField] private float _healthAmountBase = 50;
        [SerializeField] private int _moneyDropBase = 5;
        [SerializeField] private float _minSpeed = 0.95f;
        [SerializeField] private float _maxSpeed = 1.3f;

        private float _attackSpeed;
        private float _healthAmount;
        private float _damageAmount;
        private int _moneyDrop;

        public float AttackSpeed => _attackSpeedBase;
        public float HealthAmount => _healthAmount;
        public float DamageAmount => _damageAmount;
        public int MoneyDrop => _moneyDrop;
        public float MinSpeed => _minSpeed;
        public float MaxSpeed => _maxSpeed;

        public void UpdateEnemyStats(EnemyMultipliers multipliers)
        {
            _damageAmount = _damageAmountBase * multipliers.DamageMultiplier;
            _healthAmount = _healthAmountBase * multipliers.HealthMultiplier;
            _moneyDrop = (int)(_moneyDropBase * multipliers.MoneyMultiplier);
        }
    }
}