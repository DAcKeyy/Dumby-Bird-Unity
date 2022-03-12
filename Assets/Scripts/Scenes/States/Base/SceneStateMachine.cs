using UnityEngine;
using UnityEngine.Events;

namespace Scenes.States.Base
{
    public abstract class SceneStateMachine : MonoBehaviour
    {
        public UnityEvent<SceneState> _stateChanged;
        protected SceneState SceneState;

        public void SetState(SceneState sceneState)
        {
            if (SceneState != null) 
                StartCoroutine(SceneState.Exit());

            SceneState = sceneState;
            _stateChanged.Invoke(sceneState);
            
            StartCoroutine(SceneState.Init());
        }
    }
}