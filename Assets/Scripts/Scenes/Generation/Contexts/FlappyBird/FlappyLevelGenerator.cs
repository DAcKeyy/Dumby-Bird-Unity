using System;
using Data.Generators;
using DI.Signals;
using Scenes.Actors.FlappyBird;
using Scenes.Generation.Base;
using Scenes.Generation.Factories;
using Zenject;
using Vector2 = UnityEngine.Vector2;

namespace Scenes.Generation.Contexts.FlappyBird
{
    public class FlappyLevelGenerator : ILevelGenerator
    {
        private ILevelGenerator _pipeGenerator;
        private readonly ILevelGenerator _landGenerator;
        private readonly ILevelGenerator _buildingsGenerator;
        private readonly ILevelGenerator _bushesGenerator;
        private readonly ILevelGenerator _cloudsGenerator;
        private readonly FlappyLevelGenerationSettings _levelGenerationSettings;
        private readonly Bird _player;
        private readonly InvisibleBorder2D _topBorder2D;
        
        private Vector2 _lastPlayerPosition;
        private bool isGameStarted;
        //TODO: ПОфиксиить это..
        private float _backgroundGeneratorReminder, _pipeGeneratorReminder, _landGeneratorReminder, _cloudGeneratorReminder, _bushGeneratorReminder = 0;
        
        protected FlappyLevelGenerator(
            FlappyLevelGenerationSettings levelGenerationSettings,
            PipePareSpawnFactory pareSpawnFactory,
            LandSpawnFactory landSpawnFactory,
            BuildingsFactory buildingsFactory,
            BushesFactory bushesFactory,
            CloudsFactory cloudsFactory,
            Bird player,
            SignalBus signalBus, 
            InvisibleBorder2D topBorder2D) 
        {
            signalBus.Subscribe<GamePointObtainedSignal>(x => Update());
            signalBus.Subscribe<GameStarted>(x => {
                CreatePipes(
                    levelGenerationSettings._pipePareGenerationSettings,pareSpawnFactory,
                    _player.transform.position);
                isGameStarted = true;
            });
            
            _player = player;
            _topBorder2D = topBorder2D;
            _levelGenerationSettings = levelGenerationSettings;
            _buildingsGenerator = new BuildingsGenerator(levelGenerationSettings._backgroundGenerationSettings, buildingsFactory);
            _landGenerator = new LandGenerator(levelGenerationSettings._landGenerationSettings, landSpawnFactory);
            _bushesGenerator = new BushesGenerator(levelGenerationSettings._backgroundGenerationSettings, bushesFactory);
            _cloudsGenerator = new CloudsGenerator(levelGenerationSettings._backgroundGenerationSettings, cloudsFactory);
        }

        public void Create()
        {
            _lastPlayerPosition = _player.transform.position;
            _topBorder2D.ChangePosition(_levelGenerationSettings._invisibleBordersPosition);
            _topBorder2D.ChangeColliderSize(new Vector2( //TODO: Remove magic number
                _levelGenerationSettings._pipePareGenerationSettings.PipeStartPosition.x * 2,
                1));
            _landGenerator.Create();
            _buildingsGenerator.Create();
            _bushesGenerator.Create();
            _cloudsGenerator.Create();
        }

        public void Update()
        {
            var playerPathDistance = (Vector2)_player.transform.position - _lastPlayerPosition;
            _topBorder2D.ChangePosition(new Vector2(_player.transform.position.x, _topBorder2D.transform.position.y));
            //TODO: Они все однотипные, как это фискисть...
            if(isGameStarted) UpdatePipes(playerPathDistance);
            UpdateLands(playerPathDistance);
            UpdateBuildings(playerPathDistance);
            UpdateClouds(playerPathDistance);
            UpdateBushes(playerPathDistance);
            
            _lastPlayerPosition = _player.transform.position;
        }

        private void CreatePipes(FlappyLevelGenerationSettings.PipePareGenerationSettings settings, PipePareSpawnFactory spawnFactory, Vector2 playerPosition)
        {
            _lastPlayerPosition = _player.transform.position;
            _pipeGenerator = new PipeGenerator(settings, spawnFactory, playerPosition);
            _pipeGenerator.Create();
        }

        private void UpdatePipes(Vector2 playerPathDistance)
        {
            var fractionalUpdate =
                playerPathDistance.x / _levelGenerationSettings._pipePareGenerationSettings.PipeDistance  + _pipeGeneratorReminder;
            
            for (var i = (int)Math.Truncate(fractionalUpdate); i > 0; --i) 
            {
                _pipeGenerator.Update();
            }
            
            _pipeGeneratorReminder = fractionalUpdate - (float)Math.Truncate(fractionalUpdate);
            
        }

        private void UpdateBuildings(Vector2 playerPathDistance)
        {
            //сколькоПрошёлИгрок / дистанцияМеждуОбектами
            var fractionalUpdate = 
                playerPathDistance.x / _levelGenerationSettings._backgroundGenerationSettings.BuildingsDistance + _backgroundGeneratorReminder;
            
            //округлить(fractionalUpdate + остаток после уменьшения целым)
            for (var i = (int)Math.Truncate(fractionalUpdate); 
                 i > 0; --i)
            {
                _buildingsGenerator.Update();
            }
            
            //остаток после уменьшения целым
            _backgroundGeneratorReminder = fractionalUpdate - (float)Math.Truncate(fractionalUpdate);
        }

        private void UpdateLands(Vector2 playerPathDistance)
        {
            var fractionalUpdate = 
                playerPathDistance.x / _levelGenerationSettings._landGenerationSettings.LandsDistance  + _landGeneratorReminder;
            
            for (var i = (int)Math.Truncate(fractionalUpdate); i > 0; --i) 
            {
                _landGenerator.Update();
            }
            
            _landGeneratorReminder = fractionalUpdate - (float)Math.Truncate(fractionalUpdate);
        }

        private void UpdateClouds(Vector2 playerPathDistance)
        {
            var fractionalUpdate = 
                playerPathDistance.x / _levelGenerationSettings._backgroundGenerationSettings.CloudsDistance  + _cloudGeneratorReminder;
            
            for (var i = (int)Math.Truncate(fractionalUpdate); i > 0; --i) 
            {
                _cloudsGenerator.Update();
            }
            
            _cloudGeneratorReminder = fractionalUpdate - (float)Math.Truncate(fractionalUpdate);
        }
        
        private void UpdateBushes(Vector2 playerPathDistance)
        {
            var fractionalUpdate = 
                playerPathDistance.x / _levelGenerationSettings._backgroundGenerationSettings.BushesDistance  + _bushGeneratorReminder;
            
            for (var i = (int)Math.Truncate(fractionalUpdate); i > 0; --i) 
            {
                _bushesGenerator.Update();
            }
            
            _bushGeneratorReminder = fractionalUpdate - (float)Math.Truncate(fractionalUpdate);
        }
    }
}