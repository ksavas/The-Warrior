using UnityEngine;

namespace Framework.Extensions
{
    public static class TransformExtensions
    {
        /// <summary>
        /// Hedef görüş alanındamı ?
        /// </summary>
        /// <param name="origin">Transform origin</param>
        /// <param name="target">Target direction</param>
        /// <param name="fieldOfView">field of view</param>
        /// <param name="collisionMask">Check against layers</param>
        /// <param name="offset">transform origin offset</param>
        /// <returns></returns>
        public static bool IsInLineOfSight(this Transform origin, Vector3 target, float fieldOfView, LayerMask collisionMask, Vector3 offset)
        {
            Vector3 direction = target - origin.position;

            if (Vector3.Angle(origin.forward, direction.normalized) < fieldOfView / 2)
            {
                float distanceToTarget = Vector3.Distance(origin.position, target);

                if (Physics.Raycast(origin.position + offset + origin.forward * .3f, direction.normalized, distanceToTarget, collisionMask))
                    return false;
                //Debug.DrawLine(başlagıc pos, finish pos) 
                return true;

            }
            return false;
        }
    }
}
