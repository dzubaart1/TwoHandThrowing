using Services;
using System;
using UnityEngine;

namespace Core
{
    public class RuntimeBehaviour : MonoBehaviour
    {
        public event Action BehaviourUpdateEvent;
        public event Action BehaviourLateUpdateEvent;
        public event Action BehaviourFixedUpdateEvent;
        public event Action BehaviourDestroyEvent;
        public event Action BehaviourStartEvent;


        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]
        private static async void Initialize()
        {
            var go = new GameObject("Platform<Runtime>");
            var comp = go.AddComponent<RuntimeBehaviour>();

            await Engine.Initialize(comp);
        }

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
