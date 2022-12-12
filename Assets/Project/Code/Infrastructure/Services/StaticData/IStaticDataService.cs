using Project.Code.Infrastructure.Data;
using Project.Code.Infrastructure.StaticData;
using Project.Code.StaticData;
using Project.Code.StaticData.Units;
using Project.Code.StaticData.Units.Player;
using Project.Code.StaticData.World;
using UnityEngine;

namespace Project.Code.Infrastructure.Services.StaticData
{
    public interface IStaticDataService
    {
        void Load();
        WindowConfig GetWindow(WindowID id);
        UnitStaticData GetUnit(UnitID unitID);
        WorldStaticData GetWorldStaticData();
        PlayerStaticData GetPlayerStaticData();
        LevelStaticData GetLevelStaticData(int level);
    }
}