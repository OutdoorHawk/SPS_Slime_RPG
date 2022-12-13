using System;

namespace Project.Code.Infrastructure.Services.SceneLoaderService
{
    public interface ISceneLoader
    {
        void LoadScene(string sceneName, Action onLoaded = null, Action onReadyToPlay = null);
    }
}