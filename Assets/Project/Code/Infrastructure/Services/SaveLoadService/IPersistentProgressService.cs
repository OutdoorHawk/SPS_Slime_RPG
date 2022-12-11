using Project.Code.Infrastructure.Services.SaveLoadService.Progress;

namespace Project.Code.Infrastructure.Services.SaveLoadService
{
    public interface IPersistentProgressService
    {
        PlayerProgress Progress { get; set; }
    }
}