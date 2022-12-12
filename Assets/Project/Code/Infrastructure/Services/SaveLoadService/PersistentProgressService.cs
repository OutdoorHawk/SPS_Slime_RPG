using Project.Code.Infrastructure.Services.SaveLoadService.Progress;

namespace Project.Code.Infrastructure.Services.SaveLoadService
{
    public class PersistentProgressService : IPersistentProgressService
    {
        public PlayerProgress Progress { get; set; }
    }
}