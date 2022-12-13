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
        [SerializeField] private TMP_Text _actionTitleText;
        [SerializeField] private Image _levelBarImage;
        [SerializeField] private float _levelBarTime;

        private PlayerLevelsProgress _levelsProgress;
        private LevelStaticData _levelStaticData;
        private Tween _levelBarTween;
        private Tween _bossTitleTween;
        private float _actionTextDuration = 0.75f;

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

        public void EnableBossTitle()
        {
            _actionTitleText.DOFade(1, 0);
            _actionTitleText.gameObject.SetActive(true);
            _actionTitleText.text = "BOSS FIGHT!";
            _actionTitleText.transform.localScale = Vector3.zero;
            _actionTextDuration = 0.5f;
            _actionTitleText.transform.DOScale(Vector3.one, _actionTextDuration).OnComplete(() =>
                _actionTitleText.DOFade(0, _actionTextDuration)
                    .OnComplete(() => _actionTitleText.gameObject.SetActive(false)));
        }

        public void EnableDefeatTitle(Action onDone)
        {
            _actionTitleText.DOFade(1, 0);
            _actionTitleText.gameObject.SetActive(true);
            _actionTitleText.text = "TRY AGAIN!";
            _actionTitleText.transform.localScale = Vector3.zero;
            _actionTextDuration = 0.5f;
            _actionTitleText.transform.DOScale(Vector3.one, _actionTextDuration).OnComplete(() =>
                _actionTitleText.DOFade(0, _actionTextDuration)
                    .OnComplete(() => _actionTitleText.gameObject.SetActive(false)).OnComplete(onDone.Invoke));
        }

        public void EnableEndGameTitle()
        {
            _actionTitleText.DOFade(1, 0);
            _actionTitleText.gameObject.SetActive(true);
            _actionTitleText.text = "CONGRATULATIONS!\n YOU FINISHED THE GAME";
            _actionTitleText.transform.localScale = Vector3.zero;
            _actionTextDuration = 0.5f;
            _actionTitleText.transform.DOScale(Vector3.one, _actionTextDuration);
        }

        private void OnDestroy()
        {
            _levelBarTween?.Kill();
        }
    }
}