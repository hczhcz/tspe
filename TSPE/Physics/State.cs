namespace TSPE.Physics
{
    using JetBrains.Annotations;
    using TSPE.Utils;

    public class State
    {
        public Vector Velocity { get; private set; }
        public Vector Position { get; private set; }
        public Vector AngularVelocity { get; private set; }
        public Quaternion Rotation { get; private set; }

        public State(
            Vector velocity,
            Vector position,
            Vector angularVelocity,
            Quaternion rotation
        )
        {
            Velocity = velocity;
            Position = position;
            AngularVelocity = angularVelocity;
            Rotation = rotation;
        }

        [Pure]
        public Vector ToGlobalPosition(Vector vector)
        {
            return vector + Position;
        }

        [Pure]
        public Vector ToGlobalDirection(Vector vector)
        {
            return Rotation.Transform(vector);
        }

        [Pure]
        public Quaternion ToGlobalRotation(Quaternion quaternion)
        {
            return Rotation.Transform(quaternion);
        }

        [Pure]
        public Vector ToGlobalPoint(Vector vector)
        {
            return ToGlobalPosition(ToGlobalDirection(vector));
        }

        [Pure]
        public Vector ToLocalPosition(Vector vector)
        {
            return vector - Position;
        }

        [Pure]
        public Vector ToLocalDirection(Vector vector)
        {
            return Rotation.Inverse().Transform(vector);
        }

        [Pure]
        public Quaternion ToLocalRotation(Quaternion quaternion)
        {
            return Rotation.Inverse().Transform(quaternion);
        }

        [Pure]
        public Vector ToLocalPoint(Vector vector)
        {
            return ToLocalDirection(ToLocalPosition(vector));
        }

        public void Flip()
        {
            Velocity = -Velocity;
            AngularVelocity = -AngularVelocity;
        }

        public void Simulate(Input input, Time time)
        {
            double timeDelta = time.Step();

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
