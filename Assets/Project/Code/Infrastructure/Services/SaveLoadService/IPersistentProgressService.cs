namespace Project.Code.Infrastructure.Services.SaveLoadService
{
    public interface IPersistentProgressService
    {
        PlayerProgress Progress { get; set; }
    }
}