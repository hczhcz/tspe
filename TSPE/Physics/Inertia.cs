namespace TSPE.Physics
{
    using JetBrains.Annotations;
    using TSPE.Utils;

    public struct Inertia
    {
        public readonly double Mass;
        public readonly Vector InertiaTensor;
        public readonly Quaternion InertiaTensorRotation;

        public Inertia(
            double mass,
            Vector inertiaTensor,
            Quaternion inertiaTensorRotation
        )
        {
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
            return InertiaTensorRotation.Transform(
                InertiaTensorRotation.Inverse().Transform(
                    angularAcceleration
                ) * InertiaTensor
            );
        }

        [Pure]
        public Vector TorqueToAngularAcceleration(Vector torque)
        {
            return InertiaTensorRotation.Transform(
                InertiaTensorRotation.Inverse().Transform(
                    torque
                ) / InertiaTensor
            );
        }
    }
}
