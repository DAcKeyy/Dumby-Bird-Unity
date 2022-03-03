using System;
using Data.Generators;
using DI.Signals;
using Scenes.Generation.Base;
using Scenes.Generation.Factories;
using Zenject;

namespace Scenes.Generation.Contexts
{
    public class FlappyLevelGenerator : ILevelGenerator
    {
        private readonly ILevelGenerator _pipeGenerator;
        private readonly ILevelGenerator _landGenerator;
        private FlappyLevelGenerationSettings _levelGenerationSettings;
        
        protected FlappyLevelGenerator(
            FlappyLevelGenerationSettings levelGenerationSettings,
            PipePareSpawnFactory pareSpawnFactory,
            LandSpawnFactory landSpawnFactory,
            SignalBus signalBus) 
        {
            _levelGenerationSettings = levelGenerationSettings;
            signalBus.Subscribe<GamePointObtainedSignal>(x => Update());
            _pipeGenerator = new PipeGenerator(levelGenerationSettings._pipePareGenerationSettings,pareSpawnFactory);
            _landGenerator = new LandGenerator(levelGenerationSettings._landGenerationSettings, landSpawnFactory);
        }

        public void Create()
        {
            _pipeGenerator.Create();
            _landGenerator.Create();
        }

        public void Update()
        {
            _pipeGenerator.Update();

            for (var i = (int)_levelGenerationSettings._pipePareGenerationSettings.PipeDistance; i > 0; i--) {
                _landGenerator.Update();
            }
        }
    }
}