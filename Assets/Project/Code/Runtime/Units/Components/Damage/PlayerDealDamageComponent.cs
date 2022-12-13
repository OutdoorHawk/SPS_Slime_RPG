using System.Collections;
using System.Collections.Generic;
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
        private Enemy[] _currentTargets;
        private List<Projectile> _projectiles;
        private float _doubleShotChance;
        private int _shotCount;

        public void Init(float attack, float atkSpeed, float loadedCRIT, float loadedDoubleShot)
        {
            _doubleShotChance = loadedDoubleShot;
            base.Init(attack, atkSpeed);
            _attackDetails.Crit = loadedCRIT;
            _currentTargets = new Enemy[1];
            _projectiles = new List<Projectile>();
        }

        public void UpdateTarget()
        {
            if (_currentTargets[0] == null)
                _currentTargets = FindTargets();
        }

        protected override void Attack()
        {
            base.Attack();
            CheckShootType();
        }

        private void CheckShootType()
        {
            int randomValue = Random.Range(0, 99);
            if (randomValue < _doubleShotChance)
                StartCoroutine(ShootDoubleRoutine(2));
            else
                ShootProjectile(_currentTargets[0]);
        }

        private IEnumerator ShootDoubleRoutine(int shotCount)
        {
            int currentShots = 0;
            int i = 0;
            do
            {
                ShootProjectile(_currentTargets[i]);
                i = Random.Range(0, _currentTargets.Length);
                currentShots++;
                yield return new WaitForSeconds(0.15f);
            } while (currentShots < shotCount);
        }

        private void ShootProjectile(Enemy target)
        {
            if (target == null)
                return;
            Projectile projectile = SpawnProjectile();
            projectile.SeekTarget(target, OnHit);
            _projectiles.Add(projectile);
        }

        private Projectile SpawnProjectile()
        {
            return Instantiate(_projectilePrefab, transform.position, Quaternion.identity);
        }

        private void OnHit(Enemy target)
        {
            target.HealthComponent.TakeDamage(_attackDetails);
            UpdateProjectilesList();
        }

        private void UpdateProjectilesList()
        {
            for (int i = 0; i < _projectiles.Count; i++)
                if (_projectiles[i] == null)
                    _projectiles.Remove(_projectiles[i]);
        }

        private void OnDestroy()
        {
            for (var i = 0; i < _projectiles.Count; i++)
                if (_projectiles[i] != null)
                    Destroy(_projectiles[i].gameObject);

            _projectiles.Clear();
        }
    }
}