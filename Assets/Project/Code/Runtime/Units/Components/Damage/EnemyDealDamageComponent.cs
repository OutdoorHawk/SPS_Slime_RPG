using Project.Code.Runtime.Units.Components.Animation;
using Project.Code.Runtime.Units.Components.Health;
using UnityEngine;

namespace Project.Code.Runtime.Units.Components.Damage
{
    public class EnemyDealDamageComponent : DealDamageComponent
    {
        [SerializeField] private GameObject _hitParticles;
        private HealthComponent _playerHealth;
        private EnemyAnimator _enemyAnimator;

        public void Init(float attack, float atkSpeed, EnemyAnimator animator)
        {
            base.Init(attack, atkSpeed);
            _enemyAnimator = animator;
        }

        public void SetPlayer(HealthComponent playerHealth)
        {
            _playerHealth = playerHealth;
        }

        protected override void Attack()
        {
            base.Attack();
            _enemyAnimator.DoAttack();
        }

       /// <summary>
       /// AnimationEvent
       /// </summary>
        private void DealDamage()
        {
            _playerHealth.TakeDamage(_attackDetails);
            _hitParticles.gameObject.SetActive(true);
        }
    }
}