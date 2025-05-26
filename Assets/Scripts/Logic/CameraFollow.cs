using UnityEngine;

namespace Scripts.Logic
{
    public class CameraFollow : MonoBehaviour
    {
        public Transform target;
                
        private const float SMOOTH_SPEED = 2f;
        private static readonly Vector3 CAMERA_OFFSET = new Vector3(0, 5, -10);
        
        private const bool LOOK_AT_TARGET = true;
        private const float LOOK_AHEAD_FACTOR = 2f;

        private void LateUpdate()
        {
            if (target == null) return;
           
            Vector3 desiredPosition = target.position + CAMERA_OFFSET;            
            
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, SMOOTH_SPEED * Time.deltaTime);
            transform.position = smoothedPosition;
           
            if (LOOK_AT_TARGET)
            {            
                Vector3 lookTarget = target.position + target.forward * LOOK_AHEAD_FACTOR;
                transform.LookAt(lookTarget);
            }
        }
    }
} 