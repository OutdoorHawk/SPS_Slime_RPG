using Project.Code.Infrastructure.Data;
using Project.Code.Infrastructure.StaticData;
using Project.Code.Runtime.Player;
using Project.Code.StaticData;
using UnityEngine;

namespace Project.Code.Infrastructure.Services.StaticData
{
    public interface IStaticDataService
    {
        void Load();
        WindowConfig GetWindow(WindowID id);
        PlayerStaticData GetPlayerStaticData();
        WorldStaticData GetWorldStaticData();
        PlayerSlime GetPlayerPrefab();
    }
}