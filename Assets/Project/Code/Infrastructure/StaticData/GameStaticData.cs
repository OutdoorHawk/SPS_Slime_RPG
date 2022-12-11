using System.Collections.Generic;
using Project.Code.Runtime.Units.Player;
using Project.Code.StaticData;
using Project.Code.StaticData.Units;
using Project.Code.StaticData.World;
using UnityEngine;

namespace Project.Code.Infrastructure.StaticData
{
    [CreateAssetMenu(fileName = "GameStaticData", menuName = "Static Data/GameStaticData")]
    public class GameStaticData : ScriptableObject
    {
        [SerializeField] private WindowConfig[] _windows;
        [SerializeField] private UnitConfig[] _units;
        [SerializeField] private WorldStaticData _worldStaticData;

        public IEnumerable<WindowConfig> Windows => _windows;
        
        public UnitConfig[] Units => _units;
        public WorldStaticData WorldStaticData => _worldStaticData;
    }
}