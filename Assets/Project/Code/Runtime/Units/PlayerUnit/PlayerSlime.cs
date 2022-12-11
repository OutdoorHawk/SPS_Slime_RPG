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
            float loadedAttack = GetLoadedStat(statsProgress, StatID.ATK);
            float loadedAtkSpeed = GetLoadedStat(statsProgress, StatID.ASPD);
            float loadedHP = GetLoadedStat(statsProgress, StatID.HP);

            _damageComponent = GetComponent<PlayerDealDamageComponent>();
            _damageComponent.Init(loadedAttack, loadedAtkSpeed);
            HealthComponent.Init(loadedHP);
        }

        private float GetLoadedStat(PlayerStatsProgress statsProgress,  StatID id)
        {
            return _staticData.GetStatData(id).StatBaseValue + statsProgress.GetStatProgress(id).StatValue;
        }

        private void Update()
        {
            _damageComponent.UpdateTarget();
            _damageComponent.UpdateAttack();
        }
    }
}