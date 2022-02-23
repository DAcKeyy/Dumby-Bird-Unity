using Data.Generators;
using UnityEngine;
using Zenject;

namespace DI.Installers
{
    [CreateAssetMenu(fileName = "Flappy Level Generation Settings", menuName = "ScriptableObjects/Levels/FlappyBird", order = 1)]
    public class FlappyLevelGenerationSettingsInstaller : ScriptableObjectInstaller<FlappyLevelGenerationSettingsInstaller>
    {
        [SerializeField] private FlappyLevelGenerationSettings _levelSettings;
        
        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<FlappyLevelGenerationSettings>().FromInstance(_levelSettings).AsSingle();
        }
    }
}