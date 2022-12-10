using SPS_Slime_RPG.Code.Infrastructure.Data;
using SPS_Slime_RPG.Code.Infrastructure.StaticData;

namespace SPS_Slime_RPG.Code.Infrastructure.Services.StaticData
{
    public interface IStaticDataService : IService
    {
        void Load();
        WindowConfig GetWindow(WindowID id);
    }
}