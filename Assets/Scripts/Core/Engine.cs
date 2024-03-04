using System;
using System.Collections.Generic;
using Assets.Scripts.Core.Services;
using UnityEngine;

namespace TwoHandThrowing.Core
{
    public class Engine
    {
        public static EventCombiner EventCombiner { get; set; }

        private static Dictionary<Type, IService> _services = new();

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        public static void Initialize()
        {
            EventCombiner = new GameObject("RuntimeBehaviour", typeof(EventCombiner)).GetComponent<EventCombiner>();
            
            NetworkService networkService = new NetworkService();
            SceneSwitchingService sceneSwitchingService = new SceneSwitchingService();
            
            AddService(sceneSwitchingService);
            AddService(networkService);
            AddService(new BallConfigurationService());
            AddService(new HandDataConfigurationService(GetConfiguration<HandDataConfiguration>()));
            AddService(new BallSpawnerService(GetConfiguration<BallSpawnerConfiguration>()));
            AddService(new UIService(GetConfiguration<UIConfiguration>(), sceneSwitchingService));
            AddService(new InputService(GetConfiguration<InputConfiguration>()));

            foreach(var pair in _services)
            {
                pair.Value.Initialize();
            }
        }

        public static T GetConfiguration<T>() where T : Configuration
        {
            return ResourcesLoader.GetConfiguration<T>();
        }

        public static void AddService<T>(T service) where T : IService
        {
            if (_services.ContainsKey(typeof(T)))
            {
                return;
            }

            _services.Add(typeof(T), service);
        }

        public static T GetService<T>() where T : IService
        {
            if (!_services.ContainsKey(typeof(T)))
            {
                throw new Exception("Service doesn't exists.");
            }

            return (T)_services[typeof(T)];
        }
    }
}
