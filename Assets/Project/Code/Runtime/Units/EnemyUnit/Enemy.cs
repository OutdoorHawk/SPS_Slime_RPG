using Project.Code.Runtime.Units.Components.Damage;
using Project.Code.Runtime.Units.Player;
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

        public void SetupPlayer(PlayerSlime slime) => _player = slime;

        public override void Init(UnitStaticData unitStaticData)
        {
            base.Init(unitStaticData);
            _navMeshAgent = GetComponent<NavMeshAgent>();
            _damageComponent = GetComponent<EnemyDealDamageComponent>();
            _damageComponent.SetPlayer(_player.HealthComponent);
            enabled = true;
        }

        public void OnSpawn()
        {
            UnitCollector.AddUnit(this);
        }

        private void Update()
        {
            if (!enabled)
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

        protected override void CleanUp() =>
            UnitCollector.RemoveUnit(this);
    }
}