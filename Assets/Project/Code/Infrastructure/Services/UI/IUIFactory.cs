using Project.Code.Infrastructure.Data;
using UnityEngine;

namespace Project.Code.Infrastructure.Services.UI
{
    public interface IUIFactory
    {
        GameObject CreateWindow(WindowID id);
        void CreatePlayerHUD();
        void CreateUiRoot();
        void ClearUIRoot();
    }
}