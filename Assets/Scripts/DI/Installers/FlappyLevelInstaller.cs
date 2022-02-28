using Data.Generators;
using DI.Signals;
using Scenes.Generation.Base;
using Scenes.Generation.Contexts;
using Scenes.Generation.Factories;
using Zenject;

namespace DI.Installers
{
    public class FlappyLevelInstaller : MonoInstaller
    {
        [Inject] private FlappyLevelGenerationSettings _settings;

        public override void InstallBindings()
        {
            SignalBusInstaller.Install(Container);
            Container.DeclareSignal<PipeTouchedSignal>();
            Container.DeclareSignal<GamePointObtainedSignal>();
            
            Container.Bind<PipePareSpawnFactory>().AsSingle().WithArguments(_settings.PipeSettings);
            Container.Bind<ILevelGenerator>().
                To<FlappyLevelGenerationContext>().
                FromNew().
                AsSingle().
                WithArguments(_settings);
        }

        private enum LevelType
        {
            FlappyBird = 0
        }
    }
}