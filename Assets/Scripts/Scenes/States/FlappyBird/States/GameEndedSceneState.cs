using System.Collections;
using Scenes.States.Base;

namespace Scenes.States.FlappyBird
{
    public class GameEndedSceneState : SceneState
    {
        public GameEndedSceneState(FlappyBirdSceneStateMachine stateMachineMachine) : base(stateMachineMachine)
        {
            
        }

        public override IEnumerator Init()
        {
            return base.Init();
        }
        
        public override IEnumerator Exit()
        {
            return base.Init();
        }
    }
}