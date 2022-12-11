using System;
using UnityEngine;

namespace Project.Code.StaticData
{
    [Serializable]
    public class PlayerStaticData
    {
        [SerializeField] private float _attackSpeed = 1f;
        [SerializeField] private float _healthAmount = 10;

        public float AttackSpeed => _attackSpeed;

        public float HealthAmount => _healthAmount;
    }
}
