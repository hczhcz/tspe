namespace TSPE
{
    using TSPE.Utils;

    public class Rigidbody
    {
        public Manager Manager;

        public double Mass;
        public Vector InertiaTensor;
        public Quaternion InertiaTensorRotation;

        public Vector Velocity;
        public Vector Position;

        public Vector AngularVelocity;
        public Quaternion Rotation;

        // TODO: constraints

        public Rigidbody()
        {
        }
    }
}
