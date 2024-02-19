using Keyboard.UI;
using MaterialFactory.Services;
using UnityEngine;
using UnityEngine.UI;

namespace MaterialFactory.UI
{
    public class BallConfigurationUI : MonoBehaviour
    {
        [Header("Rigidbody Component Fields")]
        [SerializeField] private TextFieldComponent _massText;
        [SerializeField] private TextFieldComponent _dragText, _angDragText;
        [SerializeField] private Toggle _useGravityToggle, _isKinematicToggle;
        [SerializeField] private DropdownComponent _interpolationDropdown, _collisionDetectionDropdown;
        [SerializeField] private ToggleGroupComponent _freezePos, _freezeRot;
        [SerializeField] private Button _confirmBtn, _spawnBtn;

        [Space]
        [Header("Physic Material Fields")]
        [SerializeField] private TextFieldComponent _dynamicFrictionText;
        [SerializeField] private TextFieldComponent _staticFrictionText, _bouncinessText;
        [SerializeField] private DropdownComponent _frictionCombineDropdown, _bounceCombineDropdown;

        [Space]
        [Header("Others")]
        [SerializeField] private Vector3FloatComponent _spawnPointComponent;
        [SerializeField] private Vector3FloatComponent _speedComponent;

        private BallConfigurationService _ballConfigurationService;
        private BallSpawnerService _ballSpawnerService;

        private Rigidbody _rigidbody;
        private PhysicMaterial _physicMaterial;

        private float _posSum, _rotSum;

        private void Awake()
        {
            _ballConfigurationService = BallConfigurationService.Instance();
            _ballSpawnerService = BallSpawnerService.Instance();

            _rigidbody = _ballConfigurationService.TEMPObject.AddComponent<Rigidbody>();

            _confirmBtn.onClick.AddListener(OnClickConfirmBtn);
            _spawnBtn.onClick.AddListener(OnClickSpawnBtn);
        }

        private void OnDestroy()
        {
            _confirmBtn.onClick.RemoveListener(OnClickConfirmBtn);
            _spawnBtn.onClick.RemoveListener(OnClickSpawnBtn);
        }

        private void OnClickConfirmBtn()
        {
            _physicMaterial = new PhysicMaterial();

            _rigidbody.mass = float.Parse(_massText.Text);
            _rigidbody.drag = float.Parse(_dragText.Text);
            _rigidbody.angularDrag = float.Parse(_angDragText.Text);
            _rigidbody.useGravity = _useGravityToggle.isOn;
            _rigidbody.isKinematic = _isKinematicToggle.isOn;
            _rigidbody.interpolation = _interpolationDropdown.GetDropDownEnumValue<RigidbodyInterpolation>();
            _rigidbody.collisionDetectionMode = _collisionDetectionDropdown.GetDropDownEnumValue<CollisionDetectionMode>();
            _posSum = TransferBitsToSum(_freezePos.GetBits(), 2);
            _rotSum = TransferBitsToSum(_freezeRot.GetBits(), 16);
            _rigidbody.constraints = (RigidbodyConstraints)(_posSum + _rotSum);

            _physicMaterial.dynamicFriction = float.Parse(_dynamicFrictionText.Text);
            _physicMaterial.staticFriction = float.Parse(_staticFrictionText.Text);
            _physicMaterial.bounciness = float.Parse(_bouncinessText.Text);
            _physicMaterial.frictionCombine = _frictionCombineDropdown.GetDropDownEnumValue<PhysicMaterialCombine>();
            _physicMaterial.bounceCombine = _bounceCombineDropdown.GetDropDownEnumValue<PhysicMaterialCombine>();

            _ballConfigurationService.UpdateBallConfiguration(_rigidbody, _physicMaterial);
            _ballSpawnerService.SetSpawnConfig(_spawnPointComponent.GetVector3(), _speedComponent.GetVector3());
        }

        private void OnClickSpawnBtn()
        {
            _ballSpawnerService.Spawn();
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
    }
}
