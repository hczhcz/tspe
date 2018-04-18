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

        public Vector ToGlobalPosition(Vector vector)
        {
            return vector + Position;
        }

        public Vector ToGlobalDirection(Vector vector)
        {
            return Rotation.Transform(vector);
        }

        public Quaternion ToGlobalRotation(Quaternion quaternion)
        {
            return Rotation.Transform(quaternion);
        }

        public Vector ToGlobalPoint(Vector vector)
        {
            return ToGlobalPosition(ToGlobalDirection(vector));
        }

        public Vector ToLocalPosition(Vector vector)
        {
            return vector - Position;
        }

        public Vector ToLocalDirection(Vector vector)
        {
            return Rotation.Inverse().Transform(vector);
        }

        public Quaternion ToLocalRotation(Quaternion quaternion)
        {
            return Rotation.Inverse().Transform(quaternion);
        }

        public Vector ToLocalPoint(Vector vector)
        {
            return ToLocalDirection(ToLocalPosition(vector));
        }

        public void Simulate(Input input, double timeDelta)
        {
            Position += Velocity * (0.5 * timeDelta);
            Velocity += input.Velocity + input.Acceleration * timeDelta;
            Position += Velocity * (0.5 * timeDelta);

            Vector delta = AngularVelocity * (0.25 * timeDelta);
            AngularVelocity += input.AngularVelocity + input.AngularAcceleration * timeDelta;
            delta += AngularVelocity * (0.25 * timeDelta);
            // note: the last normalization is to correct the float-point error
            Rotation = new Quaternion(1, delta).Normalize().Transform(Rotation).Normalize();
        }
    }
}
