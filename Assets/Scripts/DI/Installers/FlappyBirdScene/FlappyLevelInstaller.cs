using System.Collections;
using Data.Generators;
using Data.Saving;
using DI.Signals;
using Scenes.Actors.FlappyBird;
using Scenes.Actors.Movement;
using Scenes.Generation.Base;
using Scenes.Generation.Contexts.FlappyBird;
using Scenes.Generation.Factories;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using Zenject;

namespace DI.Installers.FlappyBirdScene
{
    public class FlappyLevelInstaller : MonoInstaller
    {
        [SerializeField] private UnityEvent _gamePointObtained;
        [SerializeField] private UnityEvent<bool> _gamePlaying;
        [SerializeField] private Bird _bird;
        [Inject] private FlappyLevelGenerationSettings _settings;
        
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
            Container.DeclareSignal<GameStarted>();

            //TODO: GlobalPrefs :)
            Container.BindSignal<GamePointObtainedSignal>().ToMethod(x => {
                if (GlobalPrefs.BestScore < GlobalPrefs.CurrentScore)
                    GlobalPrefs.BestScore = GlobalPrefs.CurrentScore;
                
                GlobalPrefs.CurrentScore += x.PointsAmount;
                
                _gamePointObtained.Invoke();
            });

            //TODO: Убрать это
            Container.BindSignal<GamePauseSignal>().ToMethod(x => {
                _gamePlaying.Invoke(!x.Paused);
            });

            //TODO: Убрать это
            Container.BindSignal<BirdDiedSignal>().ToMethod(x => {
                _gamePlaying.Invoke(false);
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