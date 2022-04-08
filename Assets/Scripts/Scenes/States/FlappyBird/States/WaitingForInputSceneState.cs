using System.Collections;
using Scenes.States.Base;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Scenes.States.FlappyBird
{
    public class WaitingForInputSceneState : SceneState
    {
        private readonly InputAction _jumpInputAction;

        public WaitingForInputSceneState(
            FlappyBirdSceneStateMachine stateMachineMachine, 
            InputActionAsset inputAsset) : base(stateMachineMachine)
        {
            _jumpInputAction = inputAsset.FindActionMap("Game").FindAction("Jump");
        }
        
        
        public override IEnumerator Init()
        {
            Debug.Log($"{this.GetType()} initiated. {_jumpInputAction.name} Input Action is binded");
            
            yield return new WaitUntil(() => _jumpInputAction.triggered);
            
            base.SceneStateMachineMachine.SetState(new GameFiredSceneState(SceneStateMachineMachine as FlappyBirdSceneStateMachine));
        }

        public override IEnumerator Exit()
        {
            Debug.Log($"{this.GetType()} exit");
            
            yield return null;
        }
    }
}