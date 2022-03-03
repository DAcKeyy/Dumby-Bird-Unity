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

        public override void InstallBindings()
        {
            Container.Bind<FlappyBirdGameCanvas>().FromComponentInNewPrefab(_gameCanvas).AsSingle().NonLazy();
            Container.Bind<FlappyBirdGameOverCanvas>().FromComponentInNewPrefab(_gameOverCanvas).AsSingle().NonLazy();
            Container.Bind<FlappyBirdPauseCanvas>().FromComponentInNewPrefab(_gamePauseCanvas).AsSingle().NonLazy();
            
            _gameOverCanvas.gameObject.SetActive(false);
            _gamePauseCanvas.gameObject.SetActive(false);
        }
    }
}
