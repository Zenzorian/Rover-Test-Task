using UnityEngine;
using Scripts.UI.Markers;

namespace Scripts.Services
{
    public class SliderHandlerService : ISliderHandlerService
    {
        public float LeftSliderValue
        {
            get => _leftSliderValue;
            set
            {
                _leftSliderValue = value;
                _hudSliders.leftSlider.value = value;
            }
        }

        public float RightSliderValue
        {
            get => _rightSliderValue;
            set
            {
                _rightSliderValue = value;
                _hudSliders.rightSlider.value = value;
            }
        }     

        private HudSliders _hudSliders;

        private float _leftSliderValue;
        private float _rightSliderValue;
       
        private IInputManagerService _inputManagerService;

        public SliderHandlerService(IInputManagerService inputManagerService)
        {           
            _inputManagerService = inputManagerService;
        }

        public void Initialize(HudSliders hudSliders)
        {  
            _hudSliders = hudSliders;  
            _inputManagerService.OnValueChanged += OnValueChanged;

            _hudSliders.leftSlider.onValueChanged.AddListener(value => LeftSliderValue = value);
            _hudSliders.rightSlider.onValueChanged.AddListener(value => RightSliderValue = value);            
        }
        public void Dispose()
        {
            _inputManagerService.OnValueChanged -= OnValueChanged;
        }   
        private void OnValueChanged()
        {
            LeftSliderValue = _inputManagerService.LeftStickValue.y;
            RightSliderValue = _inputManagerService.RightStickValue.y;            
        }      
    }
}