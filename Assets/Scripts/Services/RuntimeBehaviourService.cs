using Keyboard.Services;
using System;
using UnityEngine;

namespace TwoHandThrowing.Services
{
    public class RuntimeBehaviourService
    {
        public RuntimeBehaviour Behaviour => _monobehaviour;

        private RuntimeBehaviour _monobehaviour;

        private static RuntimeBehaviourService _singleton;
        public static RuntimeBehaviourService Instance()
        {
            return _singleton ??= new RuntimeBehaviourService();
        }

        public void Init(RuntimeBehaviour behaviour)
        {
            _monobehaviour = behaviour;
        }
    }

    public class RuntimeBehaviour : MonoBehaviour
    {
        public event Action BehaviourUpdateEvent;
        public event Action BehaviourLateUpdateEvent;
        public event Action BehaviourFixedUpdateEvent;
        public event Action BehaviourDestroyEvent;
        public event Action BehaviourStartEvent;


        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]
        private static void Initialize()
        {
            var go = new GameObject("Platform<Runtime>");
            var comp = go.AddComponent<RuntimeBehaviour>();

            /*InputService.Instance();
            RuntimeBehaviourService.Instance().Init(comp);
            KeyboardService.Instance().SpawnKeyboard();*/
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
