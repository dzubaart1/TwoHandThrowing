using TwoHandThrowing.UI;

namespace TwoHandThrowing.Core
{
    public class UIService : IService
    {
        public ConnectionUI ConnectionUI { get; private set; }
        public BallConfigurationUI BallConfigurationUI { get; private set; }
        public HandConfigurationUI HandConfigurationUI { get; private set; }
        public UIConfiguration Configuration { get; private set; }

        private SceneSwitchingService _sceneSwitchingService;
        
        public UIService(UIConfiguration configuration, SceneSwitchingService sceneSwitchingService)
        {
            Configuration = configuration;
            
            _sceneSwitchingService = sceneSwitchingService;
            
            Engine.EventCombiner.BehaviourStartEvent += OnBehaviourStart;
            _sceneSwitchingService.SceneLoadedEvent += OnSceneLoad;
        }
        
        public void Initialize()
        {
            // Nothing to initialize
        }

        private void OnBehaviourStart()
        {
            ConnectionUI = UnityEngine.Object.Instantiate(Configuration.ConnectionUI);
        }

        private void OnSceneLoad(EScene scene)
        {
            switch (scene)
            {
                case EScene.DistanceGame:
                    BallConfigurationUI = UnityEngine.Object.Instantiate(Configuration.BallConfigurationUI);
                    break;
                case EScene.GoalkeeperGame:
                    HandConfigurationUI = UnityEngine.Object.Instantiate(Configuration.HandConfigurationUI);
                    break;
            }
        }
    }
}