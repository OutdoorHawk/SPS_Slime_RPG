using System;
using Project.Code.Runtime.CustomData;
using UnityEngine;

namespace Project.Code.Runtime.Units.Components
{
    public class HealthComponent : MonoBehaviour
    {
        [SerializeField] private float _currentHealth;

        public event Action OnDeath;
        public event Action<AttackDetails> OnReceivedDamage;
        public event Action<float> OnHeal;

        private float _maxHealth;
        private bool _isDead;
        public float HealthPercent => _currentHealth / _maxHealth;

        public void Init(float healthAmount)
        {
            CheckHealthUpdate(healthAmount);
            _maxHealth = healthAmount;
        }

        private void CheckHealthUpdate(float healthAmount)
        {
            if (_maxHealth == 0) 
                return;
            float hpDiff = healthAmount - _maxHealth;
            _currentHealth += hpDiff;
        }

        public void Respawn()
        {
            _currentHealth = _maxHealth;
        }

        public void TakeDamage(AttackDetails attackDetails)
        {
            if (!_isDead)
            {
                CheckDamageTaken(attackDetails);
                CheckIsAlive(attackDetails);
            }
        }

        private void CheckDamageTaken(AttackDetails attackDetails)
        {
            _currentHealth -= attackDetails.Damage;
            if (_currentHealth < 0 ) 
                _currentHealth = 0;
        }

        private void CheckIsAlive(AttackDetails attackDetails)
        {
            if (_currentHealth <= 0)
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
            _currentHealth += _hpCount;
            if (_currentHealth > _maxHealth)
            {
                _currentHealth = _maxHealth;
            }

            OnHeal?.Invoke(_hpCount);
        }
    }
}