namespace SPS_Slime_RPG.Code.Infrastructure.Services.SaveLoadService
{
    public interface IPersistentProgressService
    {
        PlayerProgress Progress { get; set; }
    }
}