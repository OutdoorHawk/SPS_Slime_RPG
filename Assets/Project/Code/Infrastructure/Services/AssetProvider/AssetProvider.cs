using Project.Code.Infrastructure.StaticData;
using UnityEngine;

namespace Project.Code.Infrastructure.Services.AssetProvider
{
    public class AssetProvider : IAssetProvider
    {
        private const string GAME_STATIC_DATA_PATH = "GameStaticData";


        public GameStaticData GetGameStaticData()
        {
            return Resources.Load<GameStaticData>(GAME_STATIC_DATA_PATH);
        }
    }
}
