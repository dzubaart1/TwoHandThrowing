using Services;
using UnityEngine;

namespace MaterialFactory
{
    public class BallSpawner : MonoBehaviour
    {
        private BallConfigurationService _ballConfigurationService;

        private Ball _ballPrefab;
        private Transform _ballHolder;

        private void Awake()
        {
            _ballConfigurationService = BallConfigurationService.Instance();

            _ballPrefab = Resources.Load<Ball>("Prefabs/BallPrefab");
            _ballHolder = new GameObject("BallHolder").transform;
        }

        public void Spawn(Vector3 spawnPoint, Vector3 force)
        {
            var ball = Instantiate(_ballPrefab, _ballHolder);
            ball.SetBallConfiguration(_ballConfigurationService.BallConfiguration);

            ball.transform.position = spawnPoint;
            ball.AddForce(force);
        }
    }
}
