using MaterialFactory.BallStuff;
using Services;
using System;
using System.Threading.Tasks;
using UnityEngine;

namespace Services
{
    public class BallSpawnerService : IService
    {
        public event Action SpawnEvent;

        private BallSpawner _ballSpawner;
        private Vector3 _spawnPoint;
        private Vector3 _force;

        public Task Initialize()
        {
            _ballSpawner = new GameObject("Spawner", typeof(BallSpawner)).GetComponent<BallSpawner>();

            return Task.CompletedTask;
        }

        public void Spawn()
        {
            _ballSpawner.Spawn(_spawnPoint, _force);
            SpawnEvent?.Invoke();
        }

        public void SetSpawnConfig(Vector3 spawnPoint, Vector3 force)
        {
            _spawnPoint = spawnPoint;
            _force = force;
        }
    }
}
