using Scripts.Services;
using Scripts.StaticData;
using Scripts.UI.Markers;
using UnityEngine;
using UnityEngine.UI;

namespace Scripts.Infrastructure.States
{
    public class MainMenuState : IPayloadedState<GameData>
    {
        private const  string MAIN_MENU_SCENE_NAME = "MainMenu";

        private readonly GameStateMachine _stateMachine;
        private readonly ISceneLoaderService _sceneLoader;
        private readonly IGameFactoryService _gameFactory;
        private readonly IInputManagerService _inputManagerService;

        private GameData _gameData;

        public MainMenuState
        (
            GameStateMachine gameStateMachine,
            IInputManagerService inputManagerService,
            ISceneLoaderService sceneLoader,           
            IGameFactoryService gameFactory           
        )
        {
            _stateMachine = gameStateMachine;
            _sceneLoader = sceneLoader;
            _inputManagerService = inputManagerService;           
            _gameFactory = gameFactory;    
        }

        public void Enter(GameData gameData)
        {
            Debug.Log("MainMenuState State"); 

            _gameFactory.Cleanup();  

            _gameData = gameData;                   

            _sceneLoader.Load(MAIN_MENU_SCENE_NAME, OnLoaded);
        }

        public void Exit()
        {    
           
        }
     
        private async void OnLoaded()
        {  
            MainMenuButtons mainMenu = await _gameFactory.CreateMainMenu();            
            
            _inputManagerService.SwitchToKeyboard();

            mainMenu.exitButton.onClick.AddListener(() => Application.Quit());
            mainMenu.keyboardButton.onClick.AddListener(() => _inputManagerService.SwitchToKeyboard());
            mainMenu.gamePadButton.onClick.AddListener(() => _inputManagerService.SwitchToGamepad());

            mainMenu.playButton.onClick.AddListener(() =>_stateMachine.Enter<LoadLevelState,GameData>(_gameData));
            
            _stateMachine.Enter<GameLoopState>();
            _sceneLoader.HideLoadingCurtain();
        }
    }
}