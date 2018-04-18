namespace TSPE
{
    using TSPE.Utils;

    public struct PhysicsInput
    {
        public readonly Rigidbody Rigidbody;

        public Vector Acceleration;
        public Vector Velocity;
        public Vector AngularAcceleration;
        public Vector AngularVelocity;

        public PhysicsInput(Rigidbody rigidbody)
        {
            Rigidbody = rigidbody;

            Acceleration = new Vector();
            Velocity = new Vector();
            AngularAcceleration = new Vector();
            AngularVelocity = new Vector();
        }

        public void AddAcceleration(Vector vector, bool force, bool local)
        {
            if (local)
            {
                vector = Rigidbody.ToGlobalDirection(vector);
            }

            if (force)
            {
                vector = Rigidbody.Inertia.ForceToAcceleration(vector);
            }

            Acceleration += vector;
        }

        public void AddAngularAcceleration(Vector vector, bool force, bool local)
        {
            if (!local)
            {
                vector = Rigidbody.ToLocalDirection(vector);
            }

            if (force)
            {
                vector = Rigidbody.Inertia.TorqueToAngularAcceleration(vector);
            }

            AngularAcceleration += vector;
        }

        public void AddAccelerationAtPosition(Vector vector, Vector position, bool force, bool local)
        {
            AddAcceleration(vector, force, local);

            if (!local)
            {
                vector = Rigidbody.ToLocalDirection(vector);
                position = Rigidbody.ToLocalPoint(position);
            }

            if (!force)
            {
                vector = Rigidbody.Inertia.AccelerationToForce(vector);
            }

            AddAngularAcceleration(position.Cross(vector), true, true);
        }

        public void AddVelocity(Vector vector, bool impulse, bool local)
        {
            if (local)
            {
                vector = Rigidbody.ToGlobalDirection(vector);
            }

            if (impulse)
            {
                vector = Rigidbody.Inertia.ForceToAcceleration(vector);
            }

            Velocity += vector;
        }

        public void AddAngularVelocity(Vector vector, bool impulse, bool local)
        {
            if (!local)
            {
                vector = Rigidbody.ToLocalDirection(vector);
            }

            if (impulse)
            {
                vector = Rigidbody.Inertia.TorqueToAngularAcceleration(vector);
            }

            AngularVelocity += vector;
        }

        public void AddVelocityAtPosition(Vector vector, Vector position, bool impulse, bool local)
        {
            AddVelocity(vector, impulse, local);

            if (!local)
            {
                vector = Rigidbody.ToLocalDirection(vector);
                position = Rigidbody.ToLocalPoint(position);
            }

            if (!impulse)
            {
                vector = Rigidbody.Inertia.AccelerationToForce(vector);
            }

            AddAngularVelocity(position.Cross(vector), true, true);
        }
    }
}
