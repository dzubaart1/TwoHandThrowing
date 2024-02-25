using System.Collections.Generic;
using TwoHandThrowing.BallStuff;
using UnityEngine;

namespace TwoHandThrowing.PushStaff
{
    public class PushingPlace : MonoBehaviour
    {
        [SerializeField] private PushPoint _pushPointPrefab;

        private Transform _pushPointsHolder;

        private Dictionary<Ball, PushPoint> _recorededBallsDict;

        private void Awake()
        {
            _pushPointsHolder = new GameObject("PushPointsHolder").GetComponent<Transform>();

            _recorededBallsDict = new Dictionary<Ball, PushPoint>();
        }

        private void OnCollisionEnter(Collision collision)
        {
            Ball ball = collision.transform.GetComponent<Ball>();

            if (ball is null)
            {
                return;
            }

            if (_recorededBallsDict.ContainsKey(ball))
            {
                return;
            }

            PushPoint pushPoint = SpawnPushPoint(collision.GetContact(0));
            _recorededBallsDict.Add(ball, pushPoint);

            ball.DestroyBall();
        }

        private PushPoint SpawnPushPoint(ContactPoint point)
        {
            return Instantiate(_pushPointPrefab, point.point, Quaternion.identity, _pushPointsHolder);
        }
    }
}