using Scripts.Services;
using Scripts.StaticData;
using UnityEngine;

namespace Scripts.Infrastructure.States
{
    public class LoadConfigState : IState
    {
        private readonly GameStateMachine _gameStateMachine;
        private readonly IConfigDataService _configDataService;
       
        public LoadConfigState(GameStateMachine gameStateMachine, IConfigDataService configDataService)
        {
            _gameStateMachine = gameStateMachine;
            _configDataService = configDataService;           
        }

        public void Enter()
        {
            Debug.Log("Load ConfigState");
        
            _configDataService.Load();  

            var roverConfig = _configDataService.GetRoverConfig();            ;    
            
            var gameData = new GameData(roverConfig);
            _gameStateMachine.Enter<MainMenuState, GameData>(gameData);
        }

        public void Exit()
        {

        }  
    }
}