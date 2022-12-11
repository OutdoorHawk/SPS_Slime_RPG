using System;
using Project.Code.Infrastructure.Data;
using Project.Code.Infrastructure.Services.SaveLoadService.Progress.Stats;
using Project.Code.Runtime.Units.Components.Damage;
using Project.Code.StaticData.Units;
using Project.Code.StaticData.Units.Player;

namespace Project.Code.Runtime.Units.PlayerUnit
{
    public class PlayerSlime : BaseUnit
    {
        private PlayerStaticData _staticData;
        private PlayerDealDamageComponent _damageComponent;

        public void Init(UnitStaticData unitStaticData, PlayerStatsProgress statsProgress)
        {
            base.Init(unitStaticData);
            _staticData = unitStaticData as PlayerStaticData;
            float loadedAttack = statsProgress.GetStatProgress(StatID.ATK).StatValue;
            float loadedAtkSpeed = statsProgress.GetStatProgress(StatID.ASPD).StatValue;
            float loadedHP = statsProgress.GetStatProgress(StatID.HP).StatValue;

            _damageComponent = GetComponent<PlayerDealDamageComponent>();
            _damageComponent.Init(loadedAttack, loadedAtkSpeed);
            HealthComponent.Init(loadedHP);
        }

        private void Update()
        {
            _damageComponent.UpdateTarget();
            _damageComponent.UpdateAttack();
        }
    }
}