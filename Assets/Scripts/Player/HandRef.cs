using System;
using Assets.Scripts.Core.Services;
using TwoHandThrowing.Core;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

namespace TwoHandThrowing.Player
{
    [RequireComponent(typeof(ActionBasedController), typeof(XRDirectInteractor))]
    public class HandRef : MonoBehaviour
    {
        public XRDirectInteractor Interactor { get; private set; }
        public ActionBasedController Controller { get; private set; }

        public HandCollision Collision => _collision;
        [SerializeField] private HandCollision _collision;
        public HandData HandData => _handData;
        [SerializeField] private HandData _handData;

        private HandDataService _handDataService;

        private void Awake()
        {
            _handDataService = Engine.GetService<HandDataService>();
            
            Interactor = GetComponent<XRDirectInteractor>();
            Controller = GetComponent<ActionBasedController>();
        }

        public void ChangeHandData(HandDataType handDataType)
        {
            var settings = _handDataService.GetSettingsByType(handDataType);
            _handData.UpdateHandDataSettings(settings);
        }

    }
}
