namespace TSPE
{
    using TSPE.Utils;

    public struct PhysicsState
    {
        public readonly Rigidbody Rigidbody;

        public Vector Velocity;
        public Vector Position;
        public Vector AngularVelocity;
        public Quaternion Rotation;

        public PhysicsState(
            Rigidbody rigidbody,
            Vector velocity,
            Vector position,
            Vector angularVelocity,
            Quaternion rotation
        )
        {
            Rigidbody = rigidbody;

            Velocity = velocity;
            Position = position;
            AngularVelocity = angularVelocity;
            Rotation = rotation;
        }

        public void Simulate(PhysicsInput physicsInput, double timeDelta)
        {
            Position += Velocity * (0.5 * timeDelta);
            Velocity += physicsInput.Velocity + physicsInput.Acceleration * timeDelta;
            Position += Velocity * (0.5 * timeDelta);

            Vector delta = AngularVelocity * (0.25 * timeDelta);
            AngularVelocity += physicsInput.AngularVelocity + physicsInput.AngularAcceleration * timeDelta;
            delta += AngularVelocity * (0.25 * timeDelta);
            Rotation = new Quaternion(1, delta).Normalize().Transform(Rotation);
        }
    }
}
