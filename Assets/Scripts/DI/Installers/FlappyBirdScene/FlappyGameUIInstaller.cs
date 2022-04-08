using DI.Signals;
using UI.Canvases;
using UnityEngine;
using Zenject;

namespace DI.Installers.FlappyBirdScene
{
    public class FlappyGameUIInstaller : MonoInstaller
    {
        [SerializeField] private FlappyBirdGameCanvas _gameCanvas;
        [SerializeField] private FlappyBirdGameOverCanvas _gameOverCanvas;
        [SerializeField] private FlappyBirdPauseCanvas _gamePauseCanvas;
        [SerializeField] private FlappyBirdPreGameCanvas _preGameCanvas;

        public override void InstallBindings()
        {
            Container.Bind<FlappyBirdGameCanvas>().FromComponentInNewPrefab(_gameCanvas).AsSingle().NonLazy();
            Container.Bind<FlappyBirdGameOverCanvas>().FromComponentInNewPrefab(_gameOverCanvas).AsSingle().NonLazy();
            Container.Bind<FlappyBirdPauseCanvas>().FromComponentInNewPrefab(_gamePauseCanvas).AsSingle().NonLazy();
            Container.Bind<FlappyBirdPreGameCanvas>().FromComponentInNewPrefab(_preGameCanvas).AsSingle().NonLazy();
            
            _gameOverCanvas.gameObject.SetActive(false);
            _gamePauseCanvas.gameObject.SetActive(false);
            _gameCanvas.gameObject.SetActive(false);
        }
    }
}
