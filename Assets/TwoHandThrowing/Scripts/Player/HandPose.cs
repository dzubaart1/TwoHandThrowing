using UnityEngine;

namespace TwoHandThrowing.Player
{
    public class HandPose : MonoBehaviour
    {
        [SerializeField] private GameObject _attachPointPrefab;
        [SerializeField] private Transform _wrist;
        [SerializeField] private GhostHandData _ghostHandData;

        public void CreateAttachPoint()
        {
            GameObject point = Instantiate(_attachPointPrefab, transform);
            point.transform.localPosition = transform.InverseTransformPoint(_wrist.position);
        }

        public void ToggleGhostHandVisible(bool isVisible)
        {
            _ghostHandData.Renderer.enabled = isVisible;
        }
    }
}