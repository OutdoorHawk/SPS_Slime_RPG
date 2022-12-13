using Project.Code.Infrastructure.Data;
using Project.Code.UI.Windows.PlayerHUD;
using UnityEngine;

namespace Project.Code.Infrastructure.Services.UI
{
    public interface IUIFactory
    {
        GameObject CreateWindow(WindowID id);
        PlayerHUDWindow CreatePlayerHUD();
        void CreateUiRoot();
        void ClearUIRoot();
    }
}