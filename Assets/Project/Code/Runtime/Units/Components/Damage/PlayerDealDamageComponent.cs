using Project.Code.Runtime.Units.EnemyUnit;
using Project.Code.Runtime.Units.PlayerUnit;
using UnityEngine;
using static Project.Code.Extensions.Utils;

namespace Project.Code.Runtime.Units.Components.Damage
{
    public class PlayerDealDamageComponent : DealDamageComponent
    {
        [SerializeField] private Projectile _projectilePrefab;

        private Enemy _currentTarget;

        public void Init(float attack, float atkSpeed,float loadedCRIT)
        {
            base.Init(attack, atkSpeed);
            _attackDetails.Crit = loadedCRIT;
        }

        public void UpdateTarget()
        {
            if (_currentTarget == null)
                _currentTarget = FindClosestTarget(transform.position);
        }

        protected override void Attack()
        {
            base.Attack();
            if (_currentTarget == null)
                return;
            ShootProjectile();
        }

        private void ShootProjectile()
        {
            Projectile projectile = SpawnProjectile();
            projectile.SeekTarget(_currentTarget, OnHit);
        }

        private Projectile SpawnProjectile()
        {
            return Instantiate(_projectilePrefab, transform.position, Quaternion.identity);
        }

        private void OnHit(Enemy target) => target.HealthComponent.TakeDamage(_attackDetails);
    }
}