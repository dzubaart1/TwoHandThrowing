using UnityEngine;

namespace TwoHandThrowing.Player
{
    public class MirrorHandPose : MonoBehaviour
    {
        [SerializeField] private HandData _handDataPoseFrom;
        [SerializeField] private HandData _handDataPoseTo;

        [HideInInspector]
        public Vector3 InitialRootPosition;
        [HideInInspector]
        public Quaternion InitialRootRotation;
        [HideInInspector]
        public Quaternion[] InitialRootRotations;

        public void SaveInitPose()
        {
            InitialRootPosition = _handDataPoseTo.Root.localPosition;
            InitialRootRotation = _handDataPoseTo.Root.localRotation;

            InitialRootRotations = new Quaternion[_handDataPoseTo.Bones.Length];
            for(int i = 0; i < _handDataPoseTo.Bones.Length; i++)
            {
                InitialRootRotations[i] = _handDataPoseTo.Bones[i].localRotation;
            }
        }

        public void ResetPose()
        {
            _handDataPoseTo.Root.localPosition = InitialRootPosition;
            _handDataPoseTo.Root.localRotation = InitialRootRotation;

            for (int i = 0; i < _handDataPoseTo.Bones.Length; i++)
            {
                _handDataPoseTo.Bones[i].localRotation = InitialRootRotations[i];
            }
        }

        public void UpdatePose()
        {
            Vector3 mirroredPosition = _handDataPoseFrom.Root.localPosition;
            mirroredPosition.x *= -1;

            Quaternion mirroredQuaternion = _handDataPoseFrom.Root.localRotation;
            mirroredQuaternion.y *= -1;
            mirroredQuaternion.z *= -1;

            Quaternion rotate180 = Quaternion.Euler(0, 0, 180);
            mirroredQuaternion = rotate180 * mirroredQuaternion;

            _handDataPoseTo.Root.localPosition = mirroredPosition;
            _handDataPoseTo.Root.localRotation = mirroredQuaternion;

            for (int i = 0; i < _handDataPoseFrom.Bones.Length; i++)
            {
                _handDataPoseTo.Bones[i].localRotation = _handDataPoseFrom.Bones[i].localRotation;
            }
        }
    }
}
