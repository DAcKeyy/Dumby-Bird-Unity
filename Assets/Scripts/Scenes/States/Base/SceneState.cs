using System.Collections;
using UnityEngine;
using UnityEngine.Events;

namespace Scenes.States.Base
{
    public abstract class SceneState
    {
        public UnityEvent _init;
        public UnityEvent _exit;
        protected SceneStateMachine SceneStateMachineMachine;

        public SceneState(SceneStateMachine stateMachineMachine)
        {
            SceneStateMachineMachine = stateMachineMachine;
        }

        public virtual IEnumerator Init()
        {
            _init.Invoke();
            yield break;
        }

        public virtual IEnumerator Exit()
        {
            _exit.Invoke();
            yield break;
        }
    }
}