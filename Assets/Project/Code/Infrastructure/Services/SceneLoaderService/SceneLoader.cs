using System;
using System.Collections;
using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Project.Code.Infrastructure.Services.SceneLoaderService
{
    public class SceneLoader : MonoBehaviour, ISceneLoader
    {
        [SerializeField] private CanvasGroup _canvasGroup;
        [SerializeField] private float _fadeTime;

        private IEnumerator _loadingRoutine;
        private Tween _imageTween;

        public void LoadScene(string sceneName, Action onLoaded = null)
        {
            _imageTween?.Kill();
            _imageTween = _canvasGroup.DOFade(1, _fadeTime)
                .OnComplete(() => StartLoadingOperation(sceneName, onLoaded));
        }

        private void StartLoadingOperation(string sceneName, Action onLoaded)
        {
            _loadingRoutine = LoadingScreenStartRoutine(sceneName, onLoaded);
            StartCoroutine(_loadingRoutine);
        }

        private IEnumerator LoadingScreenStartRoutine(string sceneName, Action onLoaded)
        {
            AsyncOperation operation = SceneManager.LoadSceneAsync(sceneName);
            while (operation != null && !operation.isDone)
                yield return 0;

            onLoaded?.Invoke();
            _loadingRoutine = null;
            _imageTween = _canvasGroup.DOFade(0, _fadeTime);
        }

        private void OnDestroy()
        {
            _imageTween?.Kill();
        }
    }
}