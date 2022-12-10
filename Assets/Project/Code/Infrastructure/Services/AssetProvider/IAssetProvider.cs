using SPS_Slime_RPG.Code.Infrastructure.StaticData;

namespace SPS_Slime_RPG.Code.Infrastructure.Services.AssetProvider
{
    public interface IAssetProvider
    {
        void Load();
        GameStaticData GetGameStaticData();
    }
}