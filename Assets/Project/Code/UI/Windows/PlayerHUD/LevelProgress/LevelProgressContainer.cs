using System;
using DG.Tweening;
using Project.Code.Infrastructure.Services.SaveLoadService.Progress;
using Project.Code.Infrastructure.Services.StaticData;
using Project.Code.StaticData.World;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Project.Code.UI.Windows.PlayerHUD.LevelProgress
{
    public class LevelProgressContainer : MonoBehaviour
    {
        [SerializeField] private TMP_Text _levelText;
        [SerializeField] private Image _levelBarImage;
        [SerializeField] private float _levelBarTime;

        private PlayerLevelsProgress _levelsProgress;
        private LevelStaticData _levelStaticData;
        private Tween _levelBarTween;

        public void Init(PlayerLevelsProgress levelsProgress, IStaticDataService staticDataService)
        {
            _levelsProgress = levelsProgress;
            _levelStaticData = staticDataService.GetLevelStaticData(_levelsProgress.CurrentLevel);
            UpdateLevelProgress();
            UpdateFightProgress();
        }

        private void UpdateLevelProgress()
        {
            _levelText.text = $"1 - {_levelsProgress.CurrentLevel + 1}";
        }

        public void UpdateFightProgress()
        {
            float endValue = (float)_levelsProgress.CurrentFight / _levelStaticData.MaxFightsOnLevel;
            _levelBarTween?.Kill();
            _levelBarTween = _levelBarImage.DOFillAmount(endValue, _levelBarTime);
        }

        private void OnDestroy()
        {
            _levelBarTween?.Kill();
        }
    }
}