using Assets.Scripts;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class HandPose : MonoBehaviour
{
    [SerializeField] private GameObject _attachPointPrefab;
    [SerializeField] private Transform _wrist;
    [SerializeField] private HandGrabInteractable _handGrabInteractable;
    [SerializeField] private HandData _handDataPose;

    private void Start()
    {
        _handGrabInteractable.selectEntered.AddListener(OnSelectEntered);
        _handGrabInteractable.selectExited.AddListener(OnSelectExited);
    }

    private void OnDestroy()
    {
        _handGrabInteractable.selectEntered.RemoveListener(OnSelectEntered);
        _handGrabInteractable.selectExited.RemoveListener(OnSelectExited);
    }

    public void CreateAttachPoint()
    {
        var point = Instantiate(_attachPointPrefab, transform);
        point.transform.localPosition = transform.InverseTransformPoint(_wrist.position);
    }

    private void OnSelectEntered(SelectEnterEventArgs args)
    {
        var handRef = args.interactorObject.transform.GetComponent<HandRef>();
        if (handRef is null || handRef.HandData.HandType != _handDataPose.HandType)
        {
            return;
        }

        handRef.HandData.Animator.enabled = false;
        handRef.HandData.Root.parent.localRotation = _handDataPose.Root.localRotation;

        for(int i = 0; i < handRef.HandData.Bones.Length; i++)
        {
            handRef.HandData.Bones[i].localRotation = _handDataPose.Bones[i].localRotation;
        }
    }

    private void OnSelectExited(SelectExitEventArgs args)
    {
        var handRef = args.interactorObject.transform.GetComponent<HandRef>();
        if (handRef is null || handRef.HandData.HandType != _handDataPose.HandType)
        {
            return;
        }

        handRef.HandData.Animator.enabled = true;

        switch(handRef.HandData.HandType)
        {
            case HandType.Left:
                handRef.HandData.Root.parent.localRotation = Quaternion.Euler(new Vector3(90, 0, -90));
                break;
            case HandType.Right:
                handRef.HandData.Root.parent.localRotation = Quaternion.Euler(new Vector3(-90, 0, -90));
                break;
        }
    }
}
