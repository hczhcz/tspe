namespace TSPE
{
    using JetBrains.Annotations;
    using TSPE.Utils;

    public struct Inertia
    {
        public readonly Rigidbody Rigidbody;

        public readonly double Mass;
        public readonly Vector InertiaTensor;
        public readonly Quaternion InertiaTensorRotation;

        public Inertia(
            Rigidbody rigidbody,
            double mass,
            Vector inertiaTensor,
            Quaternion inertiaTensorRotation
        )
        {
            Rigidbody = rigidbody;

            Mass = mass;
            InertiaTensor = inertiaTensor;
            InertiaTensorRotation = inertiaTensorRotation;
        }

        [Pure]
        public Vector AccelerationToForce(Vector acceleration)
        {
            return acceleration * Mass;
        }

        [Pure]
        public Vector ForceToAcceleration(Vector force)
        {
            return force / Mass;
        }

        [Pure]
        public Vector AngularAccelerationToTorque(Vector angularAcceleration)
        {
            Quaternion rotation = Rigidbody.PhysicsState.Rotation.Transform(
                InertiaTensorRotation
            );

            return rotation.Transform(
                rotation.Inverse().Transform(
                    angularAcceleration
                ) * InertiaTensor
            );
        }

        [Pure]
        public Vector TorqueToAngularAcceleration(Vector torque)
        {
            Quaternion rotation = Rigidbody.PhysicsState.Rotation.Transform(
                InertiaTensorRotation
            );

            return rotation.Transform(
                rotation.Inverse().Transform(
                    torque
                ) / InertiaTensor
            );
        }
    }
}
