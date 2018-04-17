namespace TSPE
{
    using TSPE.Utils;

    public class Rigidbody
    {
        public Manager Manager;

        public Inertia Inertia;
        public PhysicsInput PhysicsInput;

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
