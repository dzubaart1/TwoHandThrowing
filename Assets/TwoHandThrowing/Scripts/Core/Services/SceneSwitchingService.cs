using System;
using UnityEngine.SceneManagement;

namespace TwoHandThrowing.Core
{
    public class SceneSwitchingService : IService
    {
        public Action<EScene> SceneLoadedEvent;
        private IService _serviceImplementation;
        public EScene CurrentScene { get; private set; }

        public void LoadScene(EScene scene)
        {
            SceneManager.LoadSceneAsync((int)scene, LoadSceneMode.Additive);
            CurrentScene = scene;
            SceneLoadedEvent?.Invoke(scene);
        }
        
        public void Initialize()
        {
            // Nothing to initialize
        }
    }

    public enum EScene : byte
    {
        Menu = 0,
        GoalkeeperGame = 1,
        DistanceGame = 2,
    }
}
