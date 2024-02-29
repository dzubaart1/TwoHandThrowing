using System;
using JetBrains.Annotations;
using TwoHandThrowing.Core;
using TwoHandThrowing.Player;
using UnityEngine;

public class HandChanger : MonoBehaviour
{
    [SerializeField] private HandDataType _handDataType;


    private void OnTriggerEnter(Collider col)
    {
        var handRef = FindComponentInParents<HandRef>(col.transform);

        Debug.Log("Here1");
        
        if (handRef is null)
        {
            return;
        }
        
        Debug.Log("Here2");
        
        handRef.ChangeHandData(_handDataType);
    }

    [CanBeNull]
    private TComp FindComponentInParents<TComp>(Transform obj) where TComp : Component
    {
        if (obj is null)
        {
            return null;
        }
        
        var comp = obj.GetComponentInParent<TComp>();

        return comp ? comp : FindComponentInParents<TComp>(obj.parent);
    }
}
