using Scripts.UI.Markers;

namespace Scripts.Services
{
    public interface ISliderHandlerService: IService
    {
        float LeftSliderValue { get; set; }
        float RightSliderValue { get; set; }
        void Initialize(HudSliders hudSliders);
        void Dispose();
    }
} 