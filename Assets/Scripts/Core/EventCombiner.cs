using System;
using UnityEngine;

namespace TwoHandThrowing.Core
{
    public class EventCombiner : MonoBehaviour
    {
        public event Action BehaviourUpdateEvent;
        public event Action BehaviourLateUpdateEvent;
        public event Action BehaviourFixedUpdateEvent;
        public event Action BehaviourDestroyEvent;
        public event Action BehaviourStartEvent;

        private void Start()
        {
            BehaviourStartEvent?.Invoke();
        }

        private void Update()
        {
            BehaviourUpdateEvent?.Invoke();
        }

        private void LateUpdate()
        {
            BehaviourLateUpdateEvent?.Invoke();
        }

        private void OnDestroy()
        {
            BehaviourDestroyEvent?.Invoke();
        }

        private void FixedUpdate()
        {
            BehaviourFixedUpdateEvent?.Invoke();
        }
    }
}
