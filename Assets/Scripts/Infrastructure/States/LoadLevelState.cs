using Scripts.Services;
using Scripts.StaticData;
using Scripts.UI.Markers;
using UnityEngine;

namespace Scripts.Infrastructure.States
{
    public class LoadLevelState : IPayloadedState<GameData>
    {
        private const string LEVEL_SCENE_NAME = "Level";
        
        private readonly GameStateMachine _stateMachine;

        private readonly ISceneLoaderService _sceneLoader;
        private readonly IGameFactoryService _gameFactory;
        private readonly ISliderHandlerService _sliderHandler;
        

        private GameData _gameData;

        public LoadLevelState
        (
            GameStateMachine gameStateMachine,
            ISceneLoaderService sceneLoader,          
            IGameFactoryService gameFactory,           
            ISliderHandlerService sliderHandler
        )
        {
            _stateMachine = gameStateMachine;
            _sceneLoader = sceneLoader;           
            _gameFactory = gameFactory;  
            _sliderHandler = sliderHandler;          
        }

        public void Enter(GameData gameData)
        {
            Debug.Log("Load Level State");
            _gameData = gameData;
            _gameFactory.Cleanup();         
          
            _sceneLoader.Load(LEVEL_SCENE_NAME , OnLoaded);          
        }

        public void Exit()
        {         
        }
     
        private async void OnLoaded()
        { 
            GameObject hud = await _gameFactory.CreateHud();
            var hudElements = hud.GetComponent<HudSliders>();
            _sliderHandler.Initialize(hudElements);
            hudElements.menuBatton.onClick.AddListener(() => _stateMachine.Enter<MainMenuState,GameData>(_gameData));
            await _gameFactory.CreateRover(Vector3.zero, _gameData.roverConfig);                        
            
            _sceneLoader.HideLoadingCurtain();

            _stateMachine.Enter<GameLoopState>();                       
        }      
    }
}