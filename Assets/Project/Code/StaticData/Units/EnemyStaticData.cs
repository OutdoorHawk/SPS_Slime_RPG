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

        private float _attackSpeed;
        private float _healthAmount;
        private float _damageAmount;
        private int _moneyDrop;

        public float AttackSpeed => _attackSpeedBase;
        public float HealthAmount => _healthAmount;
        public float DamageAmount => _damageAmount;
        public int MoneyDrop => _moneyDrop;

        public void UpdateEnemyStats(EnemyMultipliers multipliers)
        {
            _damageAmount = _damageAmountBase * multipliers.DamageMultiplier;
            _healthAmount = _healthAmountBase * multipliers.HealthMultiplier;
            _moneyDrop = (int)(_moneyDropBase * multipliers.MoneyMultiplier);
        }
    }
}