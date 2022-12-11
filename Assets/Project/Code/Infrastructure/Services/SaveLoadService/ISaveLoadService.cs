using Project.Code.Infrastructure.Services.SaveLoadService.Progress;

namespace Project.Code.Infrastructure.Services.SaveLoadService
{
    public interface ISaveLoadService
    {
        void SaveProgress();
        PlayerProgress LoadProgress();
    }
}