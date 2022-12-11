using Project.Code.Infrastructure.Services.SaveLoadService.Progress;
using Project.Code.Runtime.Units.Components.Damage;
using Project.Code.Runtime.Units.PlayerUnit;
using Project.Code.Runtime.World;
using Project.Code.StaticData.Units;
using UnityEngine;
using UnityEngine.AI;

namespace Project.Code.Runtime.Units.EnemyUnit
{
    public class Enemy : BaseUnit
    {
        private PlayerSlime _player;
        private NavMeshAgent _navMeshAgent;
        private EnemyDealDamageComponent _damageComponent;
        private EnemyStaticData _enemyStaticData;

        public void SetupPlayer(PlayerSlime slime) => _player = slime;

        public override void Init(UnitStaticData unitStaticData, PlayerProgress playerProgress)
        {
            base.Init(unitStaticData, playerProgress);
            _enemyStaticData = unitStaticData as EnemyStaticData;
            _navMeshAgent = GetComponent<NavMeshAgent>();
            _damageComponent = GetComponent<EnemyDealDamageComponent>();
            _damageComponent.SetPlayer(_player.HealthComponent);
            DealDamageComponent.Init(_enemyStaticData.DamageAmount, _enemyStaticData.AttackSpeed);
            HealthComponent.Init(_enemyStaticData.HealthAmount);
            HealthComponent.Respawn();
            enabled = true;
        }

        public void OnSpawn()
        {
            UnitCollector.AddUnit(this);
        }

        private void Update()
        {
            if (!enabled || _player == null)
                return;
            if (FarFromPlayer())
                MoveToPlayer();
            else
                _damageComponent.UpdateAttack();
        }

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