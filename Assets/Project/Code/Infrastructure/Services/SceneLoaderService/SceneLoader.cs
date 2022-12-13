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

        public void LoadScene(string sceneName, Action onLoaded = null, Action onReadyToPlay = null)
        {
            _imageTween?.Kill();
            _imageTween = _canvasGroup.DOFade(1, _fadeTime)
                .OnComplete(() => StartLoadingOperation(sceneName, onLoaded, onReadyToPlay));
        }

        private void StartLoadingOperation(string sceneName, Action onLoaded, Action onReadyToPlay)
        {
            _loadingRoutine = LoadingScreenStartRoutine(sceneName, onLoaded, onReadyToPlay);
            StartCoroutine(_loadingRoutine);
        }

        private IEnumerator LoadingScreenStartRoutine(string sceneName, Action onLoaded, Action onReadyToPlay)
        {
            AsyncOperation operation = SceneManager.LoadSceneAsync(sceneName);
            while (operation != null && !operation.isDone)
                yield return 0;

            onLoaded?.Invoke();
            yield return new WaitForSeconds(0.25f);
            _loadingRoutine = null;
            _imageTween = _canvasGroup.DOFade(0, _fadeTime);
            onReadyToPlay?.Invoke();
        }

        private void OnDestroy()
        {
            _imageTween?.Kill();
        }
    }
}