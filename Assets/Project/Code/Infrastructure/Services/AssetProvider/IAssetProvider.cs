using Project.Code.Infrastructure.StaticData;

namespace Project.Code.Infrastructure.Services.AssetProvider
{
    public interface IAssetProvider
    {
        void Load();
        GameStaticData GetGameStaticData();
    }
}