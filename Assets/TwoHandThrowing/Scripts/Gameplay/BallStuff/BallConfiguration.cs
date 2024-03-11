using JetBrains.Annotations;
using UnityEngine;

namespace TwoHandThrowing.Gameplay
{
    public class BallConfiguration
    {
        [CanBeNull] public Rigidbody RigidBody { get; set; }
        [CanBeNull] public PhysicMaterial PhysicMaterial { get; set; }
        public float SmoothingDuration { get; set; }
        public float VelocityScale { get; set; }
        public float AngularVelocityScale { get; set; }
        public float LifeTime { get; set; }
    }
}
