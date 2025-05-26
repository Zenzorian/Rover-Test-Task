using System.Threading.Tasks;
using Scripts.Infrastructure.AssetManagement;
using UnityEngine;      
using Scripts.ScriptableObjects;
using Scripts.UI.Markers;
using Scripts.Logic;
using System;

namespace Scripts.Services
{
    public class GameFactoryService : IGameFactoryService
    { 
        private const float ROVER_Y_OFFSET = 5f;

        private readonly IAssetProvider _assets;       
        private readonly IInputManagerService _inputManager;       
                  
        public GameFactoryService
        (
            IAssetProvider assets,            
            IInputManagerService inputManager           
        )
        {
            _assets = assets;           
            _inputManager = inputManager;               
        }
            
        
        public async Task<MainMenuButtons> CreateMainMenu()
        {           
            GameObject mainMenu = await _assets.Instantiate(AssetAddress.MainMenuPath);
            return mainMenu.GetComponent<MainMenuButtons>();           
        }
        
        public async Task<GameObject> CreateHud()
        {  
            GameObject hud = await _assets.Instantiate(AssetAddress.HudPath);
            return hud;          
        }
        
        public async Task<GameObject> CreateRover(Vector3 at, RoverConfig roverConfig)
        {  
           
            Vector3 spawnPosition = new Vector3(at.x, at.y + ROVER_Y_OFFSET, at.z);
            GameObject roverObject = await _assets.Instantiate(AssetAddress.RoverPath, spawnPosition);            

            Rover rover = roverObject.GetComponent<Rover>();
            if (rover == null)
            {
                throw new NullReferenceException("Rover component not found on instantiated object");
            }

            rover.Construct(_inputManager, roverConfig);

            var camera = GameObject.FindAnyObjectByType<Camera>();
            if (camera == null)
            {
                throw new NullReferenceException("Camera not found in scene");
            }

            camera.gameObject.AddComponent<CameraFollow>().target = roverObject.transform;

            return roverObject;
           
        }

        public void Cleanup()
        {           
            _assets.Cleanup();        
        }              
    }
}