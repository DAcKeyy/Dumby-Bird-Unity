using Cinemachine;
using DI.Signals;
using UnityEngine;
using Zenject;

namespace Scenes.Actors.Camera
{
    [RequireComponent(typeof(CinemachineVirtualCamera))]
    public class FlappyCamera : MonoBehaviour , ICamera
    {
        private CinemachineVirtualCamera _camera;

        [Inject]
        public void Init(SignalBus signalBus)
        {
            _camera = GetComponent<CinemachineVirtualCamera>();
            signalBus.Subscribe<BirdDiedSignal>(x => FollowTarget(null));
        }
        
        public void FollowTarget(GameObject target)
        {
            if (target == null)
            {
                _camera.Follow = null;
                _camera.enabled = false;
                return;
            }
            
            _camera.enabled = true;
            _camera.Follow = target.transform;
        }
    }
}