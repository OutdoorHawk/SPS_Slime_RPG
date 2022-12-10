using Project.Code.Extensions;
using UnityEngine;

namespace Project.Code.Infrastructure.Services.SaveLoadService
{
    public class SaveLoadService : ISaveLoadService
    {
        private const string ProgressKey = "PlayerProgress";

        private readonly IPersistentProgressService _progressService;

        public SaveLoadService(IPersistentProgressService progressService)
        {
            _progressService = progressService;
        }

        public void SaveProgress()
        {
            PlayerPrefs.SetString(ProgressKey, _progressService.Progress.ToJson());
        }

        public PlayerProgress LoadProgress()
        {
            return PlayerPrefs.GetString(ProgressKey, null)?.ToDeserialized<PlayerProgress>();
        }
    }
}