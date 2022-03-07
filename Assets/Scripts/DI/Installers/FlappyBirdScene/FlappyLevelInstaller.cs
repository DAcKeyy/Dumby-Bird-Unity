using Data.Generators;
using Data.Saving;
using DI.Signals;
using Scenes.Actors.FlappyBird;
using Scenes.Generation.Base;
using Scenes.Generation.Contexts.FlappyBird;
using Scenes.Generation.Factories;
using UnityEngine;
using Zenject;

namespace DI.Installers.FlappyBirdScene
{
    public class FlappyLevelInstaller : MonoInstaller
    {
        [Inject] private FlappyLevelGenerationSettings _settings;
        [SerializeField] private Bird _bird;

        public override void InstallBindings()
        {
            //TODO: GlobalPrefs :)
            GlobalPrefs.CurrentScore = 0;
            
            SignalBusInstaller.Install(Container);
            Container.DeclareSignal<PipeTouchedSignal>();
            Container.DeclareSignal<GamePointObtainedSignal>();
            Container.DeclareSignal<BirdDiedSignal>();
            Container.DeclareSignal<GamePauseSignal>();
            Container.DeclareSignal<LandTouchedSignal>();

            //TODO: GlobalPrefs :)
            Container.BindSignal<GamePointObtainedSignal>().ToMethod(x => {
                if (GlobalPrefs.BestScore < GlobalPrefs.CurrentScore)
                    GlobalPrefs.BestScore = GlobalPrefs.CurrentScore;
                
                GlobalPrefs.CurrentScore += x.PointsAmount;
            });

            Container.Bind<Bird>().FromInstance(_bird).AsSingle().NonLazy();
            
            Container.Bind<PipePareSpawnFactory>().AsSingle().WithArguments(_settings._pipePareGenerationSettings.PipeSettings);
            Container.Bind<LandSpawnFactory>().AsSingle().WithArguments(_settings._landGenerationSettings.LandSettings);
            Container.Bind<BuildingsFactory>().AsSingle().WithArguments(_settings._backgroundGenerationSettings.BuildingsSettings);
            Container.Bind<CloudsFactory>().AsSingle().WithArguments(_settings._backgroundGenerationSettings.CloudsSettings);
            Container.Bind<BushesFactory>().AsSingle().WithArguments(_settings._backgroundGenerationSettings.BushesSettings);
            
            Container.Bind<ILevelGenerator>().
                To<FlappyLevelGenerator>().
                FromNew().
                AsSingle().
                WithArguments(_settings, _bird);
        }
    }
}