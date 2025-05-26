using UnityEngine;

namespace Scripts.ScriptableObjects
{
    [System.Serializable, CreateAssetMenu(fileName = "RoverConfig", menuName = "ScriptableObjects/RoverConfig")]
    public class RoverConfig : ScriptableObject
    {
        public float maxMotorTorque = 500f;       
        public float brakeTorque = 1000f;   
        public float rotationSpeed = 200f; 
    }
}