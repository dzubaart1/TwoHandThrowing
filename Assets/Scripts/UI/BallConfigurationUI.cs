using TwoHandThrowing.Core;
using UnityEngine;
using UnityEngine.UI;

namespace TwoHandThrowing.UI
{
    public class BallConfigurationUI : MonoBehaviour
    {
        [Header("Rigidbody Component Fields")]
        [SerializeField] private SliderComponent _massSlider;
        [SerializeField] private SliderComponent _dragSlider, _angDragSlider;
        [SerializeField] private Toggle _useGravityToggle, _isKinematicToggle;
        [SerializeField] private DropdownComponent _interpolationDropdown, _collisionDetectionDropdown;
        [SerializeField] private ToggleGroupComponent _freezePos, _freezeRot;
        [SerializeField] private Button _confirmBtn, _spawnBtn;

        [Space]
        [Header("Physic Material Fields")]
        [SerializeField] private SliderComponent _dynamicFrictionSlider;
        [SerializeField] private SliderComponent _staticFrictionSlider, _bouncinessSlider;
        [SerializeField] private DropdownComponent _frictionCombineDropdown, _bounceCombineDropdown;

        [Space]
        [Header("Throw On Detach Fields")]
        [SerializeField] private SliderComponent _smoothingDurationSlider;
        [SerializeField] private SliderComponent _velocityScale, _angularVelocityScale;

        [Space]
        [Header("Others")]
        [SerializeField] private Button _spawnPointBtn;
        [SerializeField] private SliderComponent _ballLifeTime;

        private BallConfigurationService _ballConfigurationService;
        private BallSpawnerService _ballSpawnerService;
        private NetworkService _networkService;

        private Rigidbody _rigidbody;
        private PhysicMaterial _physicMaterial;

        private float _posSum, _rotSum;
        private bool _isConfirmed;

        private void Awake()
        {
            _ballConfigurationService = Engine.GetService<BallConfigurationService>();
            _ballSpawnerService = Engine.GetService<BallSpawnerService>();
            _networkService = Engine.GetService<NetworkService>();

            _rigidbody = _ballConfigurationService.TEMPObject.AddComponent<Rigidbody>();

            _confirmBtn.onClick.AddListener(OnClickConfirmBtn);
            _spawnPointBtn.onClick.AddListener(OnClickSpawnPointBtn);
            _spawnBtn.onClick.AddListener(OnClickSpawnBtn);
            _networkService.NetworkBehaviourSpawnedEvent += OnSpawnedNetworkBehaviour;
        }

        private void OnDestroy()
        {
            _confirmBtn.onClick.RemoveListener(OnClickConfirmBtn);
            _spawnBtn.onClick.RemoveListener(OnClickSpawnBtn);
            _spawnPointBtn.onClick.RemoveListener(OnClickSpawnPointBtn);
            _networkService.NetworkBehaviourSpawnedEvent -= OnSpawnedNetworkBehaviour;
        }

        private void OnClickConfirmBtn()
        {
            _physicMaterial = new PhysicMaterial();

            _rigidbody.mass = _massSlider.Value;
            _rigidbody.drag = _dragSlider.Value;
            _rigidbody.angularDrag = _angDragSlider.Value;
            _rigidbody.useGravity = _useGravityToggle.isOn;
            _rigidbody.isKinematic = _isKinematicToggle.isOn;
            _rigidbody.interpolation = _interpolationDropdown.GetDropDownEnumValue<RigidbodyInterpolation>();
            _rigidbody.collisionDetectionMode = _collisionDetectionDropdown.GetDropDownEnumValue<CollisionDetectionMode>();
            _posSum = TransferBitsToSum(_freezePos.GetBits(), 2);
            _rotSum = TransferBitsToSum(_freezeRot.GetBits(), 16);
            _rigidbody.constraints = (RigidbodyConstraints)(_posSum + _rotSum);

            _physicMaterial.dynamicFriction = _dynamicFrictionSlider.Value;
            _physicMaterial.staticFriction = _staticFrictionSlider.Value;
            _physicMaterial.bounciness = _bouncinessSlider.Value;
            _physicMaterial.frictionCombine = _frictionCombineDropdown.GetDropDownEnumValue<PhysicMaterialCombine>();
            _physicMaterial.bounceCombine = _bounceCombineDropdown.GetDropDownEnumValue<PhysicMaterialCombine>();

            _ballConfigurationService.UpdateRigidBody(_rigidbody);
            _ballConfigurationService.UpdatePhysicMaterial(_physicMaterial);
            _ballConfigurationService.UpdateBallLifeTime(_ballLifeTime.Value);
            _ballConfigurationService.UpdateThrowOnDetach(_smoothingDurationSlider.Value, _velocityScale.Value, _angularVelocityScale.Value);

            _isConfirmed = true;

            Debug.Log("Confirmed!");
        }

        private void OnClickSpawnPointBtn()
        {
            _ballSpawnerService.GetSpawnPoint();
            
            Debug.Log("Spawn point!");
        }

        private void OnClickSpawnBtn()
        {
            if(_networkService.NetworkBehaviour.isServer)
            {
                if (!_isConfirmed)
                {
                    return;
                }

                _ballSpawnerService.Spawn(_networkService.NetworkBehaviour.netIdentity, Vector3.zero);
                return;
            }

            _networkService.NetworkBehaviour.CmdSpawn(_networkService.NetworkBehaviour.netIdentity, Vector3.zero);
            Debug.Log("Spawn!");
        }

        private int TransferBitsToSum(bool[] bitsArray, int offsetValue)
        {
            int sum = 0;
            int currentNumber = offsetValue;
            foreach(var bit in bitsArray)
            {
                /*
                 * Переводим число из двоичной системы, записанное в массиве, в десятичную
                 */
                sum |= bit ? currentNumber : 0;
                currentNumber <<= 1;
            }

            return sum;
        }

        private void OnSpawnedNetworkBehaviour()
        {
            if (!_networkService.NetworkBehaviour.isServer)
            {
                _spawnPointBtn.enabled = false;
                _confirmBtn.enabled = false;
                _spawnPointBtn.image.color = Color.gray;
                _confirmBtn.image.color = Color.gray;
            }
        }
    }
}
