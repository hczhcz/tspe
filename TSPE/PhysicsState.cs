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

        public void Simulate(PhysicsInput physicsInput, double time)
        {
            Position += Velocity * (0.5 * time);
            Velocity += physicsInput.Velocity + physicsInput.Acceleration * time;
            Position += Velocity * (0.5 * time);

            Vector delta = AngularVelocity * (0.25 * time);
            AngularVelocity += physicsInput.AngularVelocity + physicsInput.AngularAcceleration * time;
            delta += AngularVelocity * (0.25 * time);
            Rotation = new Quaternion(1, delta).Normalize().Transform(Rotation);
        }
    }
}
