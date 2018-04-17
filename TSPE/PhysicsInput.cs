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

        public void AddAcceleration(Vector vector, bool force)
        {
            Acceleration += force
                ? Rigidbody.Inertia.ForceToAcceleration(vector)
                : vector;
        }

        public void AddAngularAcceleration(Vector vector, bool force)
        {
            AngularAcceleration += force
                ? Rigidbody.Inertia.TorqueToAngularAcceleration(vector)
                : vector;
        }

        public void AddForceAtPosition(Vector vector, Vector position)
        {
            Vector localPosition = position - Rigidbody.PhysicsState.Position;

            AddAcceleration(vector, true);
            AddAngularAcceleration(localPosition.Cross(vector), true);
        }

        public void AddVelocity(Vector vector, bool impulse)
        {
            Velocity += impulse
                ? Rigidbody.Inertia.ForceToAcceleration(vector)
                : vector;
        }

        public void AddAngularVelocity(Vector vector, bool impulse)
        {
            AngularVelocity += impulse
                ? Rigidbody.Inertia.TorqueToAngularAcceleration(vector)
                : vector;
        }

        public void AddImpulseAtPosition(Vector vector, Vector position)
        {
            Vector localPosition = position - Rigidbody.PhysicsState.Position;

            AddVelocity(vector, true);
            AddAngularVelocity(localPosition.Cross(vector), true);
        }
    }
}
