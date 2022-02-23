using System;
using Data.Generators;
using Scenes.Generation.Base;
using Scenes.Generation.Contexts;
using UnityEngine;
using Zenject;

namespace DI.Installers
{
    public class LevelGeneratorInstaller : MonoInstaller
    {
        [SerializeField] private LevelType _type;
        [Inject] private FlappyLevelGenerationSettings _settings;

        public override void InstallBindings()
        {
            Debug.Log(_settings.PipeDistance);
            switch (_type)
            {
                case LevelType.FlappyBird:
                    Container.Bind<ILevelGenerator>().
                        To<FlappyLevelGenerationContext>().
                        FromNew().
                        AsSingle().
                        WithArguments(_settings);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private enum LevelType
        {
            FlappyBird = 0
        }
    }
}