using UnityEngine;

namespace Project.Code.StaticData.Units
{
    [CreateAssetMenu(fileName = "EnemyStaticData", menuName = "Static Data/EnemyStaticData")]
    public class EnemyStaticData : UnitStaticData
    {
        [SerializeField] private float _attackSpeed = 1f;
        [SerializeField] private float _damageAmount = 10f;
        [SerializeField] private float _healthAmount = 50;
        
        public float AttackSpeed => _attackSpeed;
        public float HealthAmount => _healthAmount;
        public float DamageAmount => _damageAmount;
    }
}