using Project.Code.Infrastructure.Services.SaveLoadService.Progress;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Project.Code.UI.Windows.PlayerHUD.LevelProgress
{
    public class LevelProgressContainer : MonoBehaviour
    {
        [SerializeField] private TMP_Text _levelText;
        [SerializeField] private Image _levelBarImage;

        private PlayerLevelsProgress _levelsProgress;

        public void Init(PlayerLevelsProgress levelsProgress)
        {
            _levelsProgress = levelsProgress;
            UpdateLevelProgress();
        }

        public void UpdateLevelProgress()
        {
            _levelText.text = $"1 - {_levelsProgress.CurrentLevel + 1}";
        }

        public void UpdateFightProgress()
        {
            _levelBarImage.fillAmount = (float)_levelsProgress.CurrentFight / _levelsProgress.MaxFightsOnLevel;
        }
    }
}