using Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;

namespace Core
{
    public class Engine
    {
        public static RuntimeBehaviour RuntimeBehaviour;

        private static Dictionary<Type, IService> _services;

        public static Task Initialize(RuntimeBehaviour runtimeBehaviour)
        {
            RuntimeBehaviour = runtimeBehaviour;
            _services = new Dictionary<Type, IService>();

            AddService(new BallConfigurationService());
            AddService(new BallSpawnerService());
            AddService(new InputService());

            var services = _services.Values.ToList();

            foreach (var service in services)
                service.Initialize();

            return Task.CompletedTask;
        }

        public static void AddService<T>(T service) where T : IService
        {
            if (_services.ContainsKey(typeof(T)))
                throw new Exception($"Service {typeof(T)} already exists");

            _services.Add(typeof(T), service);
        }

        public static void RemoveService<T>() where T : IService
        {
            if (_services.ContainsKey(typeof(T)))
                _services.Remove(typeof(T));
            else
                throw new Exception($"Service {typeof(T)} doesn't exists");
        }

        public static T GetService<T>() where T : class, IService
        {
            if (_services.ContainsKey(typeof(T)))
                return (T)_services[typeof(T)];

            Type type = typeof(T);
            var result = _services.FirstOrDefault(x => type.IsInstanceOfType(x.Value));

            if (result.Value is null)
                throw new Exception($"Service {typeof(T)} doesn't exists");

            return (T)result.Value;
        }
    }
}
