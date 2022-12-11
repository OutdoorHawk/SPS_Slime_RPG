using System.Collections.Generic;
using Project.Code.Runtime.Player;
using Project.Code.StaticData;
using UnityEngine;

namespace Project.Code.Infrastructure.StaticData
{
    [CreateAssetMenu(fileName = "GameStaticData", menuName = "Static Data/GameStaticData")]
    public class GameStaticData : ScriptableObject
    {
        [SerializeField] private WindowConfig[] _windows;
        [SerializeField] private PlayerSlime _player;
        [SerializeField] private PlayerStaticData _playerStaticData;
        [SerializeField] private WorldStaticData _worldStaticData;

        public IEnumerable<WindowConfig> Windows => _windows;

        public PlayerSlime Player => _player;
        public PlayerStaticData PlayerStaticData => _playerStaticData;
        public WorldStaticData WorldStaticData => _worldStaticData;
    }
}