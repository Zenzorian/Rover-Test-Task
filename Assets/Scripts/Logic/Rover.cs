using Scripts.ScriptableObjects;
using Scripts.Services;
using UnityEngine;

namespace Scripts.Logic
{
    public class Rover : MonoBehaviour
    {
        [Header("Wheel Colliders")]
        public WheelCollider frontLeftWheel;
        public WheelCollider frontRightWheel;
        public WheelCollider rearLeftWheel;
        public WheelCollider rearRightWheel;
        
        [Header("Wheel Visual Objects")]
        public Transform frontLeftWheelMesh;
        public Transform frontRightWheelMesh;
        public Transform rearLeftWheelMesh;
        public Transform rearRightWheelMesh;

        private RoverConfig _roverConfig;      
       
        private IInputManagerService _inputManagerService;

        private Rigidbody _rigidbody;

        public void Construct(IInputManagerService inputManagerService, RoverConfig roverConfig)
        {
           _inputManagerService = inputManagerService;
           _roverConfig = roverConfig;

           _rigidbody = gameObject.GetComponent<Rigidbody>();
        }

        private void Update()
        {   
            if(_inputManagerService == null||_roverConfig == null)return;
            
            _inputManagerService.Update();
            
            // Левый стик Y - газ/тормоз, правый стик X - поворот
            float motor = _inputManagerService.LeftStickValue.y;
            float steering = _inputManagerService.RightStickValue.x;
            
            HandleMotor(motor);
            HandleSteering(steering);
            
            // Синхронизируем визуальные колеса с коллайдерами
            UpdateWheelVisuals();
        }
        
        private void HandleMotor(float motorInput)
        {
            float motorTorque = motorInput * _roverConfig.maxMotorTorque;
            
            // Применяем тягу ко всем колесам (полный привод)
            frontLeftWheel.motorTorque = motorTorque;
            frontRightWheel.motorTorque = motorTorque;
            rearLeftWheel.motorTorque = motorTorque;
            rearRightWheel.motorTorque = motorTorque;
            
            // Применяем тормоза когда нет ввода
            if (Mathf.Abs(motorInput) < 0.1f)
            {
                frontLeftWheel.brakeTorque = _roverConfig.brakeTorque;
                frontRightWheel.brakeTorque = _roverConfig.brakeTorque;
                rearLeftWheel.brakeTorque = _roverConfig.brakeTorque;
                rearRightWheel.brakeTorque = _roverConfig.brakeTorque;
            }
            else
            {
                frontLeftWheel.brakeTorque = 0;
                frontRightWheel.brakeTorque = 0;
                rearLeftWheel.brakeTorque = 0;
                rearRightWheel.brakeTorque = 0;
            }
        }
        
        private void HandleSteering(float steeringInput)
        {
            // Поворачиваем только передние колеса
            float steerAngle = steeringInput * _roverConfig.maxSteerAngle;
            frontLeftWheel.steerAngle = steerAngle;
            frontRightWheel.steerAngle = steerAngle;
        }
        
        private void UpdateWheelVisuals()
        {
            UpdateWheelVisual(frontLeftWheel, frontLeftWheelMesh);
            UpdateWheelVisual(frontRightWheel, frontRightWheelMesh);
            UpdateWheelVisual(rearLeftWheel, rearLeftWheelMesh);
            UpdateWheelVisual(rearRightWheel, rearRightWheelMesh);
        }
        
        private void UpdateWheelVisual(WheelCollider wheelCollider, Transform wheelMesh)
        {
            if (wheelCollider == null || wheelMesh == null) return;
            
            Vector3 position;
            Quaternion rotation;
            
            // Получаем позицию и поворот от WheelCollider
            wheelCollider.GetWorldPose(out position, out rotation);
            
            // Применяем к визуальному объекту колеса
            wheelMesh.position = position;
            wheelMesh.rotation = rotation;
        }       
    }
}