namespace SPS_Slime_RPG.Code.Infrastructure.Services.UI
{
    public interface IUIFactory: IService
    {
        void CreateUiRoot();
        void ClearUIRoot();
    }
}