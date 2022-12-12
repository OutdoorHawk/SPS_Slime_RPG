using System;
using Project.Code.Infrastructure.Data;
using Project.Code.Infrastructure.Services.SceneLoaderService;
using Project.Code.Infrastructure.StateMachine;
using Project.Code.Infrastructure.StateMachine.States;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

namespace Project.Code.Infrastructure
{
    public class BootstrapSceneChecker : MonoBehaviour
    {
        private IGameStateMachine _gameStateMachine;
        private ISceneLoader _sceneLoader;

        [Inject]
        private void Construct(IGameStateMachine gameStateMachine, ISceneLoader sceneLoader)
        {
            _sceneLoader = sceneLoader;
            _gameStateMachine = gameStateMachine;
        }

        private void Awake()
        {
            if (_gameStateMachine.ActiveState is not LoadLevelState)
                _sceneLoader.LoadScene(Constants.BOOTSTRAP_SCENE);
        }
    }
}
