using Assets.Scripts.Core.Services;
using TwoHandThrowing.Core;
using UnityEngine;
using UnityEngine.UI;

namespace TwoHandThrowing.UI
{
    public class HandConfigurationUI : MonoBehaviour
    {
        [Space]
        [Header("Physic Material Fields")]
        [SerializeField] private SliderComponent _dynamicFrictionSlider;
        [SerializeField] private SliderComponent _staticFrictionSlider, _bouncinessSlider;
        [SerializeField] private DropdownComponent _frictionCombineDropdown, _bounceCombineDropdown;

        [Space]
        [Header("Others")]
        [SerializeField] private SliderComponent _maxVelocityToAttach;
        [SerializeField] private Button _confirmBtn;

        private PhysicMaterial _physicMaterial;

        private HandDataConfigurationService _handDataConfigurationService;
        private InputService _inputService;
        
        private void Awake()
        {
            _confirmBtn.onClick.AddListener(OnClickConfirmBtn);

            _handDataConfigurationService = Engine.GetService<HandDataConfigurationService>();
            _inputService = Engine.GetService<InputService>();
            
            _physicMaterial = new PhysicMaterial();
        }

        private void OnDestroy()
        {
            _confirmBtn.onClick.RemoveListener(OnClickConfirmBtn);
        }

        private void OnClickConfirmBtn()
        {
            _physicMaterial.dynamicFriction = _dynamicFrictionSlider.Value;
            _physicMaterial.staticFriction = _staticFrictionSlider.Value;
            _physicMaterial.bounciness = _bouncinessSlider.Value;
            _physicMaterial.frictionCombine = _frictionCombineDropdown.GetDropDownEnumValue<PhysicMaterialCombine>();
            _physicMaterial.bounceCombine = _bounceCombineDropdown.GetDropDownEnumValue<PhysicMaterialCombine>();
            
            _handDataConfigurationService.UpdateHandConfig(_physicMaterial, _maxVelocityToAttach.Value, _inputService.LocalPlayer.LeftHand);
            _handDataConfigurationService.UpdateHandConfig(_physicMaterial, _maxVelocityToAttach.Value, _inputService.LocalPlayer.RightHand);
        }
    }
    
}