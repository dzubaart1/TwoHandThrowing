using UnityEngine;

namespace TwoHandThrowing.Player
{
    public class MirrorHandPose : MonoBehaviour
    {
        [SerializeField] private HandData _handDataPoseFrom;
        [SerializeField] private HandData _handDataPoseTo;
        
        private Vector3 _initialRootPosition;
        private Quaternion _initialRootRotation;
        private Quaternion[] _initialRootRotations;

        public void SaveInitPose()
        {
            _initialRootPosition = _handDataPoseTo.Root.localPosition;
            _initialRootRotation = _handDataPoseTo.Root.localRotation;

            _initialRootRotations = new Quaternion[_handDataPoseTo.Bones.Length];
            for(int i = 0; i < _handDataPoseTo.Bones.Length; i++)
            {
                _initialRootRotations[i] = _handDataPoseTo.Bones[i].localRotation;
            }
        }

        public void ResetPose()
        {
            _handDataPoseTo.Root.localPosition = _initialRootPosition;
            _handDataPoseTo.Root.localRotation = _initialRootRotation;

            for (int i = 0; i < _handDataPoseTo.Bones.Length; i++)
            {
                _handDataPoseTo.Bones[i].localRotation = _initialRootRotations[i];
            }
        }

        public void UpdatePose()
        {
            // Симметрично отображаем руки
            Vector3 mirroredPosition = _handDataPoseFrom.Root.localPosition;
            mirroredPosition.x *= -1;

            Quaternion mirroredQuaternion = _handDataPoseFrom.Root.localRotation;
            mirroredQuaternion.y *= -1;
            mirroredQuaternion.z *= -1;

            Quaternion rotate180 = Quaternion.Euler(0, 0, 180);
            mirroredQuaternion = rotate180 * mirroredQuaternion;

            // Применяем к рукм location и rotation
            _handDataPoseTo.Root.localPosition = mirroredPosition;
            _handDataPoseTo.Root.localRotation = mirroredQuaternion;

            for (int i = 0; i < _handDataPoseFrom.Bones.Length; i++)
            {
                _handDataPoseTo.Bones[i].localRotation = _handDataPoseFrom.Bones[i].localRotation;
            }
        }
    }
}
