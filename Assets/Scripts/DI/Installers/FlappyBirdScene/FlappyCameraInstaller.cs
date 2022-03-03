using Camera;
using UnityEngine;
using Zenject;

namespace DI.Installers.FlappyBirdScene
{
    public class FlappyCameraInstaller : MonoInstaller
    {
        [SerializeField] private FlappyCamera _flappyCamera;

        public override void InstallBindings()
        {
            Container.Bind<ICamera>().To<FlappyCamera>().FromInstance(_flappyCamera).AsSingle();
        }
    }
}