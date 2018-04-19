namespace TSPE.Physics
{
    using TSPE.Utils;

    public class Input
    {
        private readonly Rigidbody rigidbody;

        public Vector Acceleration { get; private set; }
        public Vector Velocity { get; private set; }
        public Vector AngularAcceleration { get; private set; }
        public Vector AngularVelocity { get; private set; }

        public Input(Rigidbody rigidbody)
        {
            this.rigidbody = rigidbody;
        }

        public void AddAcceleration(Vector vector, bool force, bool local)
        {
            if (local)
            {
                vector = rigidbody.State.ToGlobalDirection(vector);
            }

            if (force)
            {
                vector = rigidbody.Inertia.ForceToAcceleration(vector);
            }

            Acceleration += vector;
        }

        public void AddAngularAcceleration(Vector vector, bool force, bool local)
        {
            if (!local)
            {
                vector = rigidbody.State.ToLocalDirection(vector);
            }

            if (force)
            {
                vector = rigidbody.Inertia.TorqueToAngularAcceleration(vector);
            }

            AngularAcceleration += vector;
        }

        public void AddAccelerationAtPosition(Vector vector, Vector position, bool force, bool local)
        {
            AddAcceleration(vector, force, local);

            if (!local)
            {
                vector = rigidbody.State.ToLocalDirection(vector);
                position = rigidbody.State.ToLocalPoint(position);
            }

            if (!force)
            {
                vector = rigidbody.Inertia.AccelerationToForce(vector);
            }

            AddAngularAcceleration(position.Cross(vector), true, true);
        }

        public void AddVelocity(Vector vector, bool impulse, bool local)
        {
            if (local)
            {
                vector = rigidbody.State.ToGlobalDirection(vector);
            }

            if (impulse)
            {
                vector = rigidbody.Inertia.ForceToAcceleration(vector);
            }

            Velocity += vector;
        }

        public void AddAngularVelocity(Vector vector, bool impulse, bool local)
        {
            if (!local)
            {
                vector = rigidbody.State.ToLocalDirection(vector);
            }

            if (impulse)
            {
                vector = rigidbody.Inertia.TorqueToAngularAcceleration(vector);
            }

            AngularVelocity += vector;
        }

        public void AddVelocityAtPosition(Vector vector, Vector position, bool impulse, bool local)
        {
            AddVelocity(vector, impulse, local);

            if (!local)
            {
                vector = rigidbody.State.ToLocalDirection(vector);
                position = rigidbody.State.ToLocalPoint(position);
            }

            if (!impulse)
            {
                vector = rigidbody.Inertia.AccelerationToForce(vector);
            }

            AddAngularVelocity(position.Cross(vector), true, true);
        }
    }
}
