using System.Collections;
using DI.Signals;
using Scenes.States.Base;
using UnityEngine;


namespace Scenes.States.FlappyBird
{
    public class GameFiredSceneState : SceneState
    {
        private readonly FlappyBirdSceneStateMachine thisMachine;
        
        public GameFiredSceneState(FlappyBirdSceneStateMachine stateMachine) : base(stateMachine)
        {
            thisMachine = stateMachine;
            
            thisMachine.SignalBus.Subscribe<BirdDiedSignal>(x => 
                thisMachine.SetState(new GameEndedSceneState(thisMachine)));
        }

        public override IEnumerator Init()
        {
            Debug.Log($"{this.GetType()} initiated.");
            
            thisMachine.SignalBus.Fire(new GameStarted(thisMachine.Player.transform.position));
            
            yield return null;
        }

        public override IEnumerator Exit()
        {
            Debug.Log($"{this.GetType()} ended.");
            
            yield return null;
        }
    }
}