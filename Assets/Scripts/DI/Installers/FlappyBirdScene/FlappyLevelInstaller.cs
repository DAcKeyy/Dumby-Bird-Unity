using Data.Generators;
using Data.Saving;
using DI.Signals;
using Scenes.Actors;
using Scenes.Actors.FlappyBird;
using Scenes.Generation.Base;
using Scenes.Generation.Contexts;
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
            GlobalPrefs.CurrentScore = 0;
            
            SignalBusInstaller.Install(Container);
            Container.DeclareSignal<PipeTouchedSignal>();
            Container.DeclareSignal<GamePointObtainedSignal>();
            Container.DeclareSignal<BirdDiedSignal>();
            Container.DeclareSignal<GamePauseSignal>();
            Container.DeclareSignal<LandTouchedSignal>();
            
            Container.BindSignal<GamePointObtainedSignal>().ToMethod(x => 
                GlobalPrefs.CurrentScore += x.PointsAmount);
            
            Container.BindSignal<BirdDiedSignal>().ToMethod(x => {
                if (GlobalPrefs.BestScore < GlobalPrefs.CurrentScore) 
                    GlobalPrefs.BestScore = GlobalPrefs.CurrentScore; });
            
            Container.Bind<Bird>().FromInstance(_bird).AsSingle().NonLazy();
            Container.Bind<PipePareSpawnFactory>().AsSingle().WithArguments(_settings._pipePareGenerationSettings.PipeSettings);
            Container.Bind<LandSpawnFactory>().AsSingle().WithArguments(_settings._landGenerationSettings.LandSettings);
            Container.Bind<ILevelGenerator>().
                To<FlappyLevelGenerator>().
                FromNew().
                AsSingle().
                WithArguments(_settings);
        }
    }
}