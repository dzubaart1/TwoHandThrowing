using Mirror;
using UnityEngine;

namespace VrGunTest.Scripts.Network
{
    public class NetworkPlayer : NetworkBehaviour
    {
        [Header("Core")] 
        [SerializeField] private VrRigReference _localPlayer;
        
        [Header("Точки синхронизации с локальным игроком")]
        [SerializeField] private Transform _root;
        [SerializeField] private Transform _head;
        [SerializeField] private Transform _rightHand;
        [SerializeField] private Transform _leftHand;
        [Space]
        [Header("Visibility")]
        [SerializeField]
        [Tooltip("Эти меши выключаются если этот плеер твой")]
        private MeshRenderer[] _meshes;
        //todo: возможно при изменении объектов внутри плеера придется не меши менять а сами объекты выключать
        
        private VrRigReference _vrRigReference;

        public override void OnStartClient()
        {
            base.OnStartClient();
            if (isOwned)
            {
                _vrRigReference = Instantiate(_localPlayer);
                foreach (var meshRenderer in _meshes)
                {
                    meshRenderer.enabled = false;
                }
            }
        }

        private void Update()
        {
            if (isOwned)
            {
                _root.transform.position = _vrRigReference.Root.transform.position;
                _root.transform.rotation = _vrRigReference.Root.transform.rotation;
                
                _head.transform.position = _vrRigReference.Head.transform.position;
                _head.transform.rotation = _vrRigReference.Head.transform.rotation;
                
                _rightHand.transform.position = _vrRigReference.RightController.transform.position;
                _rightHand.transform.rotation = _vrRigReference.RightController.transform.rotation;
                
                _leftHand.transform.position = _vrRigReference.LeftController.transform.position;
                _leftHand.transform.rotation = _vrRigReference.LeftController.transform.rotation;
            }
        }
    }
}