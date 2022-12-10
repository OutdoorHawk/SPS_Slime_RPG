using System;

namespace SPS_Slime_RPG.Code.Infrastructure.Services.SceneLoaderService
{
    public interface ISceneLoader : IService
    {
        void LoadScene(string sceneName, Action onLoaded = null);
    }
}