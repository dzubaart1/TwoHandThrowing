using System;
using System.Collections.Generic;
using UnityEngine;

namespace TwoHandThrowing.Core
{
    public class Engine
    {
        public static RuntimeBehaviour Behaviour { get; set; }

        private static Dictionary<Type, IService> _services = new Dictionary<Type, IService>();

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        public static void Initialize()
        {
            Behaviour = new GameObject("RuntimeBehaviour", typeof(RuntimeBehaviour)).GetComponent<RuntimeBehaviour>();

            var networkService = new NetworkService();
            AddService(networkService);
            AddService(new BallConfigurationService(GetConfiguration<BallUIConfiguration>(), networkService));
            AddService(new BallSpawnerService(GetConfiguration<BallSpawnerConfiguration>()));
            AddService(new InputService(GetConfiguration<InputConfiguration>()));


            foreach(var pair in _services)
            {
                pair.Value.Initialize();
            }
        }

        ~Engine()
        {
            foreach (var service in _services)
            {
                service.Value.Destroy();
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

        public static void Destroy(GameObject gameObject, float time = 0)
        {
            UnityEngine.Object.Destroy(gameObject, time);
        }
    }
}
