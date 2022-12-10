using System;
using System.Collections;
using Project.Code.Infrastructure.Services.CoroutineRunner;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Project.Code.Infrastructure.Services.SceneLoaderService
{
    public class SceneLoader : ISceneLoader
    {
        private readonly ICoroutineRunner _coroutineRunner;
        private IEnumerator _loadingRoutine;

        public SceneLoader(ICoroutineRunner coroutineRunner)
        {
            _coroutineRunner = coroutineRunner;
        }

        public void LoadScene(string sceneName, Action onLoaded = null)
        {
            _loadingRoutine = LoadingScreenStartRoutine(sceneName, onLoaded);
            _coroutineRunner.StartCoroutine(_loadingRoutine);
        }

        private IEnumerator LoadingScreenStartRoutine(string sceneName, Action onLoaded)
        {
            AsyncOperation operation = SceneManager.LoadSceneAsync(sceneName);
            while (operation != null && !operation.isDone)
                yield return 0;

            _loadingRoutine = null;
            onLoaded?.Invoke();
        }
    }
}