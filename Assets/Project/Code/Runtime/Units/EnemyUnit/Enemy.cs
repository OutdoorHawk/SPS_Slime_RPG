using Project.Code.Infrastructure.Services.SaveLoadService.Progress;
using Project.Code.Infrastructure.Services.UpdateBehavior;
using Project.Code.Runtime.Units.Components.Animation;
using Project.Code.Runtime.Units.Components.Damage;
using Project.Code.Runtime.Units.PlayerUnit;
using Project.Code.Runtime.World;
using Project.Code.StaticData.Units;
using UnityEngine;
using UnityEngine.AI;

namespace Project.Code.Runtime.Units.EnemyUnit
{
    [RequireComponent(typeof(EnemyAnimator))]
    [RequireComponent(typeof(NavMeshAgent))]
    [RequireComponent(typeof(EnemyDealDamageComponent))]
    public class Enemy : BaseUnit
    {
        private PlayerSlime _player;
        private NavMeshAgent _navMeshAgent;
        private EnemyDealDamageComponent _damageComponent;
        private EnemyStaticData _enemyStaticData;
        private EnemyAnimator _animator;

        public void InitEnemy(UnitStaticData unitStaticData, PlayerProgress playerProgress,
            RectTransform hpPanel, PlayerSlime slime)
        {
            base.Init(unitStaticData, playerProgress, hpPanel);
            _player = slime;
            _enemyStaticData = unitStaticData as EnemyStaticData;
            _navMeshAgent = GetComponent<NavMeshAgent>();
            _damageComponent = GetComponent<EnemyDealDamageComponent>();
            _animator = GetComponent<EnemyAnimator>();
            _navMeshAgent.speed = Random.Range(_enemyStaticData.MinSpeed, _enemyStaticData.MaxSpeed);
            _damageComponent.SetPlayer(_player.HealthComponent);
            _damageComponent.Init(_enemyStaticData.DamageAmount, _enemyStaticData.AttackSpeed, _animator);
        }

        public void OnSpawn()
        {
            HealthComponent.UpdateMaxHealth(_enemyStaticData.HealthAmount);
            HealthComponent.Respawn();
            enabled = true;
            HPBar.UpdateHealthText(HealthComponent.CurrentHealth);
            UnitCollector.AddUnit(this);
        }

        protected override void Tick()
        {
            base.Tick();
            if (!CanMove())
                return;
            
            if (FarFromPlayer())
                MoveToPlayer();
            else
                _damageComponent.UpdateAttack();
            
            _animator.UpdateUnitAnimation(_navMeshAgent.velocity.magnitude);
        }

        private bool CanMove() => 
            enabled && _player != null;

        private bool FarFromPlayer()
            => Vector3.Distance(_player.transform.position, transform.position) >
               _navMeshAgent.stoppingDistance;

        private void MoveToPlayer() =>
            _navMeshAgent.SetDestination(_player.transform.position);

        protected override void HandleDeath()
        {
            UnitCollector.RemoveUnit(this);
            Progress.PlayerCurrencyProgress.AddMoney(_enemyStaticData.MoneyDrop);
            base.HandleDeath();
        }
    }
}