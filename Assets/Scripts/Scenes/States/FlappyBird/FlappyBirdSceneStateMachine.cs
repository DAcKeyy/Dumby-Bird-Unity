using Scenes.Actors.FlappyBird;
using Scenes.States.Base;
using UnityEngine.InputSystem;
using Zenject;

namespace Scenes.States.FlappyBird
{
    public class FlappyBirdSceneStateMachine : SceneStateMachine
    {
        [Inject] public InputActionAsset PlayerInputAction { get; }
        [Inject] public SignalBus SignalBus { get; }
        [Inject] public Bird Player { get; } 

        private void Start()
        {
            if(SceneState == null) SetState(new WaitingForInputSceneState(this, PlayerInputAction));
        }
    }
}