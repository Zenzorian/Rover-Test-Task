using Scripts.ScriptableObjects;

namespace Scripts.StaticData
{
    public class GameData
    {
        public RoverConfig roverConfig;
        public GameData(RoverConfig roverConfig)
        {
            this.roverConfig = roverConfig;           
        }
    }
}
