using System.Collections.Generic;
using TwoHandThrowing.BallStuff;
using UnityEngine;

namespace TwoHandThrowing.PushStaff
{
    public class PushingPlace : MonoBehaviour
    {
        [SerializeField] private Transform _pushPointPrefab;

        private Transform _pushPointsHolder;

        private Dictionary<Ball, Transform> _recorededBallsDict;

        private void Awake()
        {
            _pushPointsHolder = new GameObject("PushPointsHolder").GetComponent<Transform>();

            _recorededBallsDict = new Dictionary<Ball, Transform>();
        }

        private void OnCollisionEnter(Collision collision)
        {
            Ball ball = collision.transform.GetComponent<Ball>();

            if (ball == null)
            {
                return;
            }

            if (_recorededBallsDict.ContainsKey(ball))
            {
                return;
            }

            Transform pushPoint = SpawnPushPoint(collision.GetContact(0));
            _recorededBallsDict.Add(ball, pushPoint);

            ball.DestroyBall();
        }

        private Transform SpawnPushPoint(ContactPoint point)
        {
            return Instantiate(_pushPointPrefab, point.point, Quaternion.identity, _pushPointsHolder);
        }
    }
}