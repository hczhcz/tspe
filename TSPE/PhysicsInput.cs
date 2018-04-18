﻿namespace TSPE
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

        public void AddAccelerationAtPosition(Vector vector, Vector position, bool force)
        {
            Vector localPosition = Rigidbody.ToLocalPosition(position);

            AddAcceleration(vector, force);
            AddAngularAcceleration(
                force
                    ? localPosition.Cross(vector)
                    : localPosition.Cross(Rigidbody.Inertia.AccelerationToForce(vector)),
                true
            );
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

        public void AddVelocityAtPosition(Vector vector, Vector position, bool force)
        {
            Vector localPosition = Rigidbody.ToLocalPosition(position);

            AddVelocity(vector, force);
            AddAngularVelocity(
                force
                    ? localPosition.Cross(vector)
                    : localPosition.Cross(Rigidbody.Inertia.AccelerationToForce(vector)),
                true
            );
        }
    }
}
