using UnityEngine;

namespace Scenes.Actors.Camera
{
    public interface ICamera
    {
        public void FollowTarget(GameObject target);
    }
}