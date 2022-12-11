using System;
using Project.Code.Runtime.Units.Player;
using Project.Code.Runtime.World;
using Project.Code.StaticData.Units;
using UnityEngine;
using UnityEngine.AI;

namespace Project.Code.Runtime.Units.Enemy
{
    public class Enemy : BaseUnit
    {
        private PlayerSlime _player;
        private NavMeshAgent _navMeshAgent;

        public override void Init(UnitStaticData unitStaticData)
        {
            base.Init(unitStaticData);
            _navMeshAgent = GetComponent<NavMeshAgent>();
            enabled = true;
        }

        public void SetupPlayer(PlayerSlime slime) => _player = slime;

        public void OnSpawn()
        {
            UnitCollector.AddUnit(this);
        }

        private void Update()
        {
            if (enabled) 
                MoveToPlayer();
        }

        private void MoveToPlayer()
        {
            _navMeshAgent.SetDestination(_player.transform.position);
        }

        protected override void CleanUp()
        {
            UnitCollector.RemoveUnit(this);
        }
    }
}
