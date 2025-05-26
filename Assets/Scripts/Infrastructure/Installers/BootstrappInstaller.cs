using Scripts.Infrastructure.AssetManagement;
using Scripts.Infrastructure.States;
using Scripts.Logic;
using Scripts.Services;
using UnityEngine;
using Zenject;

namespace Scripts.Infrastructure
{
    public class BootstrapInstaller : MonoInstaller
    {
        [SerializeField] private LoadingCurtain _curtainPrefab;
        
        private ISceneLoaderService _sceneLoaderService;
        private ICoroutineRunner _coroutineRunner;

        public override void InstallBindings()
        {
            Debug.Log("Game Started");

            RegisterServices();

            CreateGameStateMachine();
        }

        private void RegisterServices()
        {
            BindCoroutineRunner();

            BindSceneLoaderService();

            BindConfigDataService();          

            BindAssetProvider();

            BindInputService();

            BindGameFactoryService();

            BindSliderHandlerService();              
        }
        
        private void BindCoroutineRunner()
        {
            var coroutineRunnerObject = new GameObject("Coroutine Runner");

            GameObject.DontDestroyOnLoad(coroutineRunnerObject);

            _coroutineRunner = coroutineRunnerObject.AddComponent<CoroutineRunner>();

            Container
               .Bind<ICoroutineRunner>()
               .FromInstance(_coroutineRunner)
               .AsSingle()
               .NonLazy();            
        }

        private void BindSceneLoaderService()
        {  
            var loaderCurtain = Instantiate(_curtainPrefab);

            loaderCurtain.Construct(_coroutineRunner);

            GameObject.DontDestroyOnLoad(loaderCurtain);

            _sceneLoaderService = new SceneLoaderService(_coroutineRunner, loaderCurtain);

            Container
               .Bind<ISceneLoaderService>()
               .FromInstance(_sceneLoaderService)
               .AsSingle()
               .NonLazy();
        }

        private void BindConfigDataService()
        {
            Container
                .BindInterfacesAndSelfTo<ConfigDataService>()               
                .AsSingle()
                .NonLazy();   
        }      

        private void BindAssetProvider()
        {
            Container
                .BindInterfacesAndSelfTo<AssetProvider>()
                .AsSingle()
                .NonLazy();
            
            Container
                .Resolve<AssetProvider>()
                .Cleanup();

            Container
                .Resolve<AssetProvider>()
                .Initialize();


        }

        private void BindInputService()
        {
            Container
                .BindInterfacesAndSelfTo<InputManagerService>()
                .AsSingle()
                .NonLazy();
        } 

        private void BindGameFactoryService()
        {
            Container
                .BindInterfacesAndSelfTo<GameFactoryService>()
                .AsSingle()
                .NonLazy();
        } 

        private void BindSliderHandlerService()
        {
            Container
                .BindInterfacesAndSelfTo<SliderHandlerService>()
                .AsSingle()
                .NonLazy();
        } 
        
        private void CreateGameStateMachine()
        {           
            _sceneLoaderService.ShowLoadingCurtain();

            Container
                .Bind<GameStateMachine>()  
                .AsSingle()
                .NonLazy();          
        }
    }
}