using SPS_Slime_RPG.Code.Infrastructure.StaticData;
using UnityEngine;

namespace SPS_Slime_RPG.Code.Infrastructure.Services.AssetProvider
{
    public class AssetProvider : IAssetProvider
    {
        private const string GAME_STATIC_DATA_PATH = "GameStaticData";
        
        private GameStaticData _data;
        
        public void Load() => _data = Resources.Load<GameStaticData>(GAME_STATIC_DATA_PATH);

        public GameStaticData GetGameStaticData() => _data;
    }
}
