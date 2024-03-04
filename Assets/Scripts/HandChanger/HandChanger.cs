using Assets.Scripts.Core.Services;
using JetBrains.Annotations;
using TwoHandThrowing.Core;
using TwoHandThrowing.Player;
using UnityEngine;

public class HandChanger : MonoBehaviour
{
    [SerializeField] private HandDataType _handDataType;

    private HandDataConfigurationService _handDataService;

    private void Awake()
    {
        _handDataService = Engine.GetService<HandDataConfigurationService>();
    }

    private void OnTriggerEnter(Collider col)
    {
        HandRef handRef = FindComponentInParents<HandRef>(col.transform); ;
        
        if (handRef == null)
        {
            return;
        }

        handRef.HandData.UpdateHandDataSettings(_handDataService.GetSettingsByType(_handDataType));
    }

    [CanBeNull]
    private TComp FindComponentInParents<TComp>(Transform obj) where TComp : Component
    {
        if (obj == null)
        {
            return null;
        }
        
        TComp comp = obj.GetComponentInParent<TComp>();

        return comp ? comp : FindComponentInParents<TComp>(obj.parent);
    }
}
