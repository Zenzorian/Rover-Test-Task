using Scripts.ScriptableObjects;
using Scripts.Services;
using UnityEngine;

namespace Scripts.Logic
{
    public class Rover : MonoBehaviour
    {
        public WheelCollider frontLeftWheel;
        public WheelCollider frontRightWheel;
        public WheelCollider rearLeftWheel;
        public WheelCollider rearRightWheel;       

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
            Move(_inputManagerService.LeftStickValue.y, _inputManagerService.RightStickValue.y);
            RotateRover(_inputManagerService.LeftStickValue.y, _inputManagerService.RightStickValue.y);
        }
        
        private void Move(float LeftStickValue, float RightStickValue)
        {   
            float leftSpeed = LeftStickValue * _roverConfig.maxMotorTorque;
            float rightSpeed = RightStickValue * _roverConfig.maxMotorTorque;
           
            frontLeftWheel.motorTorque = leftSpeed;
            rearLeftWheel.motorTorque = leftSpeed;

            frontRightWheel.motorTorque = rightSpeed;
            rearRightWheel.motorTorque = rightSpeed;

           
            if (Mathf.Abs(LeftStickValue) < 0.1f && Mathf.Abs(RightStickValue) < 0.1f)
            {
                frontLeftWheel.brakeTorque = _roverConfig.brakeTorque;
                rearLeftWheel.brakeTorque = _roverConfig.brakeTorque;
                frontRightWheel.brakeTorque = _roverConfig.brakeTorque;
                rearRightWheel.brakeTorque = _roverConfig.brakeTorque;
            }
            else
            {               
                frontLeftWheel.brakeTorque = 0;
                rearLeftWheel.brakeTorque = 0;
                frontRightWheel.brakeTorque = 0;
                rearRightWheel.brakeTorque = 0;
            }
        } 
        void RotateRover(float leftStick, float rightStick)
        {           
            if (Mathf.Sign(leftStick) != Mathf.Sign(rightStick) && Mathf.Abs(leftStick - rightStick) > 1.5f)
            {
                float input = leftStick - rightStick; 
                float rotation = input * _roverConfig.rotationSpeed * Time.fixedDeltaTime;
                Quaternion turnRotation = Quaternion.Euler(0f, rotation, 0f);
                _rigidbody.MoveRotation(_rigidbody.rotation * turnRotation);
            }
        }       
    }
}