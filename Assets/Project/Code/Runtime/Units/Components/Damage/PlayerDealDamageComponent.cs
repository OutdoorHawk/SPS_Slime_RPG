using System.Collections;
using DG.Tweening;
using Project.Code.Extensions;
using Project.Code.Runtime.Units.EnemyUnit;
using Project.Code.Runtime.Units.PlayerUnit;
using UnityEngine;

namespace Project.Code.Runtime.Units.Components.Damage
{
    public class PlayerDealDamageComponent : DealDamageComponent
    {
        [SerializeField] private Projectile _projectilePrefab;

        private Enemy _currentTarget;

        public void UpdateTarget()
        {
            if (_currentTarget == null)
                _currentTarget = Utils.FindClosestTarget(transform.position);
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