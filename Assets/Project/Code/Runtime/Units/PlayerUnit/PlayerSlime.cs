using Project.Code.Infrastructure.Data;
using Project.Code.Infrastructure.Services.SaveLoadService.Progress;
using Project.Code.Infrastructure.Services.SaveLoadService.Progress.Stats;
using Project.Code.Runtime.Units.Components.Animation;
using Project.Code.Runtime.Units.Components.Damage;
using Project.Code.Runtime.World;
using Project.Code.StaticData.Units;
using Project.Code.StaticData.Units.Player;
using UnityEngine;

namespace Project.Code.Runtime.Units.PlayerUnit
{
    [RequireComponent(typeof(PlayerAnimatorComponent))]
    public class PlayerSlime : BaseUnit
    {
        private PlayerStaticData _staticData;
        private PlayerDealDamageComponent _damageComponent;
        private PlayerStatsProgress _statsProgress;
        private PlayerAnimatorComponent _animatorComponent;

        public override void Init(UnitStaticData unitStaticData, PlayerProgress playerProgress,
            RectTransform hpPanel)
        {
            base.Init(unitStaticData, playerProgress, hpPanel);
            _animatorComponent = GetComponent<PlayerAnimatorComponent>();
            _statsProgress = playerProgress.PlayerStatsProgress;
            _staticData = unitStaticData as PlayerStaticData;
            _damageComponent = GetComponent<PlayerDealDamageComponent>();
            UpdateComponents();
            HealthComponent.Respawn();
        }

        public void UpdateComponents()
        {
            float loadedAttack = _statsProgress.GetStatProgress(StatID.ATK).StatValue;
            float loadedAtkSpeed = _statsProgress.GetStatProgress(StatID.ASPD).StatValue;
            float loadedHP = _statsProgress.GetStatProgress(StatID.HP).StatValue;

            _damageComponent.Init(loadedAttack, loadedAtkSpeed);
            HealthComponent.Init(loadedHP);
            _animatorComponent.SpawnUpgradeParticles();
        }

        private void Update()
        {
            if (UnitCollector.AliveEnemies.Count == 0)
                return;
            _damageComponent.UpdateTarget();
            _damageComponent.UpdateAttack();
        }

        public void SetWalkingState()
        {
            _animatorComponent.EnableWalkAnim();
        }

        public void SetFightState()
        {
            _animatorComponent.EnableIdleAnim();
        }
    }
}