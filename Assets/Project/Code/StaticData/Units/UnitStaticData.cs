using Project.Code.Runtime.Units.Player;
using UnityEngine;

namespace Project.Code.StaticData.Units
{
    public abstract class UnitStaticData : ScriptableObject
    {
        [SerializeField] private BaseUnit _unitPrefab;
        [SerializeField] private float _attackSpeed = 1f;
        [SerializeField] private float _damageAmount = 10f;
        [SerializeField] private float _healthAmount = 50;

        public float AttackSpeed => _attackSpeed;
        public float HealthAmount => _healthAmount;
        public float DamageAmount => _damageAmount;
        public BaseUnit UnitPrefab => _unitPrefab;
    }
}