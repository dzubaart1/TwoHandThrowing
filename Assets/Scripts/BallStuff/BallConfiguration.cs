using UnityEngine;

namespace TwoHandThrowing.BallStuff
{
    public class BallConfiguration
    {
        public Rigidbody? RigidBody { get; set; }
        public PhysicMaterial? PhysicMaterial { get; set; }
        public float SmoothingDuration { get; set; }
        public float VelocityScale { get; set; }
        public float AngularVelocityScale { get; set; }
        public float LifeTime { get; set; }
    }
}
