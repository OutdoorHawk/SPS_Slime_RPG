namespace Project.Code.Infrastructure.Services.SaveLoadService
{
    public interface ISaveLoadService
    {
        void SaveProgress();
        PlayerProgress LoadProgress();
    }
}