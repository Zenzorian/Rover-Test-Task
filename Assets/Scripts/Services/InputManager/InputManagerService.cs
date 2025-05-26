using UnityEngine;
using System;

namespace Scripts.Services
{
    public class InputManagerService : IInputManagerService
    {            
        private IInputManagerService _inputManagerService;
       
        public Vector2 LeftStickValue => _inputManagerService.LeftStickValue;
        public Vector2 RightStickValue => _inputManagerService.RightStickValue;
        
        public event Action OnValueChanged
        {
            add => _inputManagerService.OnValueChanged += value;
            remove => _inputManagerService.OnValueChanged -= value;
        }
       

        public void Update()        
            =>_inputManagerService.Update();        

        public void Reset()
            =>_inputManagerService.Reset();        

        public void SwitchToKeyboard()
        {              
           Disable();
            _inputManagerService = new KeyboardInputManager();  
        }      

        public void SwitchToGamepad()
        { 
            Disable();
            _inputManagerService = new GamepadInputManager();
        }

        public void Disable()
            => _inputManagerService?.Disable();
    }
}