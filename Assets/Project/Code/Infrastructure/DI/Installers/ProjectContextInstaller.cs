using Project.Code.Infrastructure.Services.AssetProvider;
using Project.Code.Infrastructure.Services.CoroutineRunner;
using Project.Code.Infrastructure.Services.Factory;
using Project.Code.Infrastructure.Services.SaveLoadService;
using Project.Code.Infrastructure.Services.SceneContext;
using Project.Code.Infrastructure.Services.SceneLoaderService;
using Project.Code.Infrastructure.Services.StaticData;
using Project.Code.Infrastructure.Services.UI;
using Project.Code.Infrastructure.Services.UpdateBehavior;
using Project.Code.Infrastructure.Services.ZenjectFactory;
using Project.Code.Infrastructure.StateMachine;
using Project.Code.Infrastructure.StateMachine.States;
using UnityEngine;
using Zenject;

namespace Project.Code.Infrastructure.DI.Installers
{
    public class ProjectContextInstaller : MonoInstaller, ICoroutineRunner
    {
        [SerializeField] private SceneLoader _sceneLoader;
        [SerializeField] private UpdateBehaviour _update;
        
        public override void InstallBindings()
        {
            BindServices();
            BindFactories();
            BindGameStateMachine();
        }

        private void BindServices()
        {
            Container.Bind<IAssetProvider>().To<AssetProvider>().AsSingle();
            Container.Bind<IStaticDataService>().To<StaticDataService>().AsSingle();
            Container.Bind<ISceneContextService>().To<SceneContextService>().AsSingle();
            Container.Bind<ISceneLoader>().FromInstance(_sceneLoader).AsSingle();
            Container.Bind<ICoroutineRunner>().FromInstance(this).AsSingle();
            Container.Bind<ISaveLoadService>().To<SaveLoadService>().AsSingle();
            Container.Bind<IPersistentProgressService>().To<PersistentProgressService>().AsSingle();
            Container.Bind<IUpdateBehaviourService>().FromInstance(_update).AsSingle();
            
        }

        private void BindGameStateMachine()
        {
            Container.Bind<IGameStateMachine>().To<GameStateMachine>().AsSingle().NonLazy();
            Container.Bind<BootstrapState>().To<BootstrapState>().AsSingle();
            Container.Bind<LoadLevelState>().To<LoadLevelState>().AsSingle();
            Container.Bind<GameLoopState>().To<GameLoopState>().AsSingle();
        }

        private void BindFactories()
        {
            Container.Bind<IUIFactory>().To<UIFactory>().AsSingle();
            Container.Bind<IZenjectFactory>().To<ZenjectFactory>().AsSingle();
            Container.Bind<IUnitFactory>().To<UnitFactory>().AsSingle();
        }
    }
}