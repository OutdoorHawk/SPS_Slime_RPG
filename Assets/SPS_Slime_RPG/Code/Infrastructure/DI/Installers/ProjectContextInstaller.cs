using SPS_Slime_RPG.Code.Infrastructure.Services.AssetProvider;
using SPS_Slime_RPG.Code.Infrastructure.Services.CoroutineRunner;
using SPS_Slime_RPG.Code.Infrastructure.Services.SceneContext;
using SPS_Slime_RPG.Code.Infrastructure.Services.SceneLoaderService;
using SPS_Slime_RPG.Code.Infrastructure.Services.StaticData;
using SPS_Slime_RPG.Code.Infrastructure.Services.UI;
using SPS_Slime_RPG.Code.Infrastructure.Services.ZenjectFactory;
using SPS_Slime_RPG.Code.Infrastructure.StateMachine;
using Zenject;

namespace SPS_Slime_RPG.Code.Infrastructure.DI.Installers
{
    public class ProjectContextInstaller : MonoInstaller, ICoroutineRunner
    {
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
            Container.Bind<ISceneLoader>().To<SceneLoader>().AsSingle();
            Container.Bind<ICoroutineRunner>().FromInstance(this).AsSingle();

        }

        private void BindGameStateMachine()
        {
            Container.Bind<IGameStateMachine>().To<GameStateMachine>().AsSingle();
        }

        private void BindFactories()
        {
            Container.Bind<IUIFactory>().To<UIFactory>().AsSingle();
            Container.Bind<IZenjectFactory>().To<ZenjectFactory>().AsSingle();
        }
    }
}