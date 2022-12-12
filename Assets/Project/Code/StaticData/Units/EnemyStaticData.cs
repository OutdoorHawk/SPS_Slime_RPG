using Project.Code.StaticData.World;
using UnityEngine;

namespace Project.Code.StaticData.Units
{
    [CreateAssetMenu(fileName = "EnemyStaticData", menuName = "Static Data/EnemyStaticData")]
    public class EnemyStaticData : UnitStaticData
    {
        [SerializeField] private float _attackSpeed = 1f;
        [SerializeField] private float _damageAmount = 10f;
        [SerializeField] private float _healthAmount = 50;
        [SerializeField] private int _moneyDrop = 5;

        public float AttackSpeed => _attackSpeed;
        public float HealthAmount => _healthAmount;
        public float DamageAmount => _damageAmount;
        public int MoneyDrop => _moneyDrop;

        public void UpdateEnemyStats(LevelStaticData levelStaticData)
        {
            SetToDefaults();
            _damageAmount += levelStaticData.DamageIncrease;
            _healthAmount += levelStaticData.HealthIncrease;
            _moneyDrop += levelStaticData.MoneyIncrease;
        }

        private void SetToDefaults()
        {
            _attackSpeed = 1f;
            _damageAmount = 0f;
            _healthAmount = 0;
            _moneyDrop = 0;
        }
    }
}