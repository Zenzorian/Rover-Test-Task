using System.Threading.Tasks;
using UnityEngine;
using Scripts.ScriptableObjects;
using Scripts.UI.Markers;

namespace Scripts.Services
{
    public interface IGameFactoryService : IService
    {   
        Task<GameObject> CreateHud();
        Task<MainMenuButtons> CreateMainMenu();       
        Task<GameObject> CreateRover(Vector3 at, RoverConfig roverConfig);
              
        void Cleanup();
    }
}