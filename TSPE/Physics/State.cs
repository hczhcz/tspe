namespace TSPE.Physics
{
    using TSPE.Utils;

    public struct State
    {
        public readonly Rigidbody Rigidbody;

        public Vector Velocity;
        public Vector Position;
        public Vector AngularVelocity;
        public Quaternion Rotation;

        public State(
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

        public void Simulate(Input input, double timeDelta)
        {
            Position += Velocity * (0.5 * timeDelta);
            Velocity += input.Velocity + input.Acceleration * timeDelta;
            Position += Velocity * (0.5 * timeDelta);

            Vector delta = AngularVelocity * (0.25 * timeDelta);
            AngularVelocity += input.AngularVelocity + input.AngularAcceleration * timeDelta;
            delta += AngularVelocity * (0.25 * timeDelta);
            Rotation = new Quaternion(1, delta).Normalize().Transform(Rotation);
        }
    }
}
