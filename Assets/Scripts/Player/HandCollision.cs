using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class HandCollision : MonoBehaviour
{

    private List<Collider> _handColliders;
    private Rigidbody _rigidbody;
    private void Awake()
    {
        _handColliders = new List<Collider>();
    }

    private void Start()
    {
        _handColliders = gameObject.GetComponentsInChildren<Collider>().ToList();
        _rigidbody = gameObject.GetComponentInChildren<Rigidbody>();
    }

    public void TurnOffColliders()
    {
        foreach (var collider in _handColliders)
        {
            collider.enabled = false;
        }
        
        _rigidbody.Sleep();
    }

    public void TurnOnColliders()
    {
        foreach (var collider in _handColliders)
        {
            collider.enabled = true;
        }
        
        _rigidbody.WakeUp();
    }
}
