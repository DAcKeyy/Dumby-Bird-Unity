using Scenes.States.Base;
using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

public class FlappyLevelStateInstaller : MonoInstaller
{
    [SerializeField] private SceneStateMachine _flappyBirdStateMachine;
    [SerializeField] private InputActionAsset _playerControls;

    public override void InstallBindings()
    {
        Container.Bind<SceneStateMachine>().FromInstance(_flappyBirdStateMachine).AsSingle();
        Container.Bind<InputActionAsset>().FromInstance(_playerControls).AsSingle();
    }
}
