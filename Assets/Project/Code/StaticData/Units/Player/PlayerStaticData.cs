using Project.Code.Infrastructure.Data;
using UnityEngine;
using static Project.Code.Extensions.Utils;

namespace Project.Code.StaticData.Units.Player
{
    [CreateAssetMenu(fileName = "PlayerStaticData", menuName = "Static Data/PlayerStaticData")]
    public class PlayerStaticData : UnitStaticData
    {
        [SerializeField] private StatConfig[] _statConfigs;

        public StatData GetStatData(StatID id) => GetStatDataFromConfig(id, _statConfigs);

        public StatConfig[] StatConfigs => _statConfigs;
    }
}