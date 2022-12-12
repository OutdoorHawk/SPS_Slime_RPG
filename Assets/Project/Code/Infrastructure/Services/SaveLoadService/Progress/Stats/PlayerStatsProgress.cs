using System;
using Project.Code.Infrastructure.Data;
using Project.Code.StaticData.Units.Player;
using UnityEngine;

namespace Project.Code.Infrastructure.Services.SaveLoadService.Progress.Stats
{
    [Serializable]
    public class PlayerStatsProgress
    {
        [SerializeField] private StatsProgression _statsProgression;

        public PlayerStatsProgress()
        {
            _statsProgression = new StatsProgression
            {
                { StatID.ATK, new StatProgress() },
                { StatID.ASPD, new StatProgress() },
                { StatID.HP, new StatProgress() },
                { StatID.HPREC, new StatProgress() }
            };
        }

        public StatProgress GetStatProgress(StatID id)
        {
            return _statsProgression[id];
        }

        public void UpgradeStat(StatID id, float valueIncrease, int upgradeCostIncrease)
        {
            _statsProgression[id].StatLvl++;
            _statsProgression[id].StatValue += valueIncrease;
            _statsProgression[id].StatUpgradeCost += upgradeCostIncrease;
        }

        public void SetDefaultStatValues(StatConfig[] statConfigs)
        {
            foreach (var config in statConfigs)
            {
                _statsProgression[config.StatID].StatValue = config.StatData.BaseValue;
                _statsProgression[config.StatID].StatUpgradeCost = config.StatData.BaseUpgradeCost;
                _statsProgression[config.StatID].StatLvl = 1;
            }
        }
    }
}