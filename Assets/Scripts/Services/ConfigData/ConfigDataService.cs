using Scripts.ScriptableObjects;
using UnityEngine;

namespace Scripts.Services
{
    public class ConfigDataService : IConfigDataService
    {
        private const string CAR_CONFIG_PATH = "Configs/RoverConfig";
             
        private RoverConfig _roverConfig;

        public void Load()
        {
            _roverConfig = Resources
                .Load<RoverConfig>(CAR_CONFIG_PATH);                    
        }
        
        public RoverConfig GetRoverConfig() => _roverConfig;
    }
}