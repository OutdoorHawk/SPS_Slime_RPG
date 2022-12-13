using System.Collections;
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
        private PlayerHealSystem _healSystem;
        private IEnumerator _fightRoutine;

        public override void Init(UnitStaticData unitStaticData, PlayerProgress playerProgress,
            RectTransform hpPanel)
        {
            base.Init(unitStaticData, playerProgress, hpPanel);
            _animatorComponent = GetComponent<PlayerAnimatorComponent>();
            _statsProgress = playerProgress.PlayerStatsProgress;
            _staticData = unitStaticData as PlayerStaticData;
            _damageComponent = GetComponent<PlayerDealDamageComponent>();
            _healSystem = new PlayerHealSystem(this, HealthComponent);
            UpdateComponents();
            HealthComponent.Respawn();
            _healthBar.UpdateHealth(HealthComponent.HealthPercent);
        }

        public void UpdateComponents()
        {
            float loadedAttack = _statsProgress.GetStatProgress(StatID.ATK).StatValue;
            float loadedAtkSpeed = _statsProgress.GetStatProgress(StatID.ASPD).StatValue;
            float loadedHP = _statsProgress.GetStatProgress(StatID.HP).StatValue;
            float loadedHPREC = _statsProgress.GetStatProgress(StatID.HPREC).StatValue;
            float loadedCRIT = _statsProgress.GetStatProgress(StatID.CRIT).StatValue;
            float loadedDoubleShot = _statsProgress.GetStatProgress(StatID.DoubleShot).StatValue;

            _healSystem.UpdateStats(loadedHPREC);
            _damageComponent.Init(loadedAttack, loadedAtkSpeed, loadedCRIT, loadedDoubleShot);
            HealthComponent.Init(loadedHP);
            _animatorComponent.SpawnUpgradeParticles();
        }

        public void SetWalkingState()
        {
            if (_fightRoutine != null)
                StopCoroutine(_fightRoutine);
            _animatorComponent.EnableWalkAnim();
        }

        public void SetFightState()
        {
            _animatorComponent.EnableIdleAnim();
            _fightRoutine = FightingRoutine();
            StartCoroutine(_fightRoutine);
        }

        private IEnumerator FightingRoutine()
        {
            while (UnitCollector.AliveEnemies.Count > 0)
            {
                _damageComponent.UpdateTarget();
                _damageComponent.UpdateAttack();
                yield return new WaitForSeconds(Time.deltaTime);
            }
        }
    }
}