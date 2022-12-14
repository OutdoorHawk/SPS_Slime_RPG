using System;
using Project.Code.Runtime.CustomData;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Project.Code.Runtime.Units.Components.Health
{
    public class HealthComponent : MonoBehaviour
    {
        public event Action OnDeath;
        public event Action<AttackDetails> OnReceivedDamage;
        public event Action<float> OnHeal;

        private float _currentHealth;
        private float _maxHealth;
        private bool _isDead;

        public float HealthPercent => CurrentHealth / _maxHealth;
        public float CurrentHealth => _currentHealth;

        public void UpdateMaxHealth(float newHealthAmount)
        {
            float hpDiff = newHealthAmount - _maxHealth;
            _maxHealth = newHealthAmount;
            TakeHeal(hpDiff);
        }

        public void Respawn()
        {
            _currentHealth = _maxHealth;
        }

        public void TakeDamage(AttackDetails attackDetails)
        {
            if (!_isDead)
                CheckDamageTaken(attackDetails);
        }

        private void CheckDamageTaken(AttackDetails attackDetails)
        {
            if (attackDetails.Crit > Random.Range(0, 99))
                attackDetails.Damage *= AttackDetails.CritValue;
            _currentHealth = CurrentHealth - attackDetails.Damage;
            if (CurrentHealth < 0)
                _currentHealth = 0;
            CheckIsAlive(attackDetails);
        }

        private void CheckIsAlive(AttackDetails attackDetails)
        {
            if (CurrentHealth <= 0)
            {
                OnReceivedDamage?.Invoke(attackDetails);
                OnDeath?.Invoke();
                _isDead = true;
            }
            else
                OnReceivedDamage?.Invoke(attackDetails);
        }

        public void TakeHeal(float _hpCount)
        {
            _currentHealth = CurrentHealth + _hpCount;
            if (CurrentHealth > _maxHealth)
                _currentHealth = _maxHealth;

            OnHeal?.Invoke(_hpCount);
        }
    }
}