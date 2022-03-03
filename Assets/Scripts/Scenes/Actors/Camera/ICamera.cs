using UnityEngine;

namespace Camera
{
    public interface ICamera
    {
        public void FollowTarget(GameObject target);
    }
}