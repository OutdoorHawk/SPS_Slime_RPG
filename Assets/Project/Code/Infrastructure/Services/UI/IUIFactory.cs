namespace Project.Code.Infrastructure.Services.UI
{
    public interface IUIFactory
    {
        void CreatePlayerHUD();
        void CreateUiRoot();
        void ClearUIRoot();
    }
}