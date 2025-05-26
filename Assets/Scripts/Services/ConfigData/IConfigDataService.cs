using Scripts.ScriptableObjects;

namespace Scripts.Services
{
    public interface IConfigDataService : IService
    {
        void Load();
        
        RoverConfig GetRoverConfig(); 
    }
}