using System.Collections.Generic;
using UnityEngine;

namespace TwoHandThrowing.Gameplay
{
    public class PushingPlace : MonoBehaviour
    {
        [SerializeField] private Transform _pushPointPrefab;

        private Transform _pushPointsHolder;

        private Dictionary<Ball, Transform> _recoredBallsDict;

        private void Awake()
        {
            _pushPointsHolder = new GameObject("PushPointsHolder").GetComponent<Transform>();

            _recoredBallsDict = new Dictionary<Ball, Transform>();
        }

        private void OnCollisionEnter(Collision collision)
        {
            Ball ball = collision.transform.GetComponent<Ball>();

            if (ball == null)
            {
                return;
            }

            if (_recoredBallsDict.ContainsKey(ball))
            {
                return;
            }

            Transform pushPoint = SpawnPushPoint(collision.GetContact(0));
            _recoredBallsDict.Add(ball, pushPoint);

            ball.DestroyBall();
        }

        private Transform SpawnPushPoint(ContactPoint point)
        {
            return Instantiate(_pushPointPrefab, point.point, Quaternion.identity, _pushPointsHolder);
        }
    }
}