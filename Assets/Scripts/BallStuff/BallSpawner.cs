using TwoHandThrowing.Core;
using UnityEngine;

namespace TwoHandThrowing.BallStuff
{
    public class BallSpawner : MonoBehaviour
    {
        private const string BALL_PREFAB_PATH = "Prefabs/Ball";

        private BallConfigurationService _ballConfigurationService;

        private Ball _ballPrefab;
        private Transform _ballHolder;

        private void Awake()
        {
            _ballConfigurationService = Engine.GetService<BallConfigurationService>();

            _ballPrefab = Resources.Load<Ball>(BALL_PREFAB_PATH);
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
