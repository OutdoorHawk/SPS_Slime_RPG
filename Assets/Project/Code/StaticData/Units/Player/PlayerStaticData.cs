using UnityEngine;

namespace Project.Code.StaticData.Units.Player
{
    [CreateAssetMenu(fileName = "PlayerStaticData", menuName = "Static Data/PlayerStaticData")]
    public class PlayerStaticData : UnitStaticData
    {
        [SerializeField] private StatConfig[] _statConfigs;

        public StatConfig[] StatConfigs => _statConfigs;
    }
}