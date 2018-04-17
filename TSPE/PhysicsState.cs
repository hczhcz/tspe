namespace TSPE
{
    using TSPE.Utils;

    public struct PhysicsState
    {
        public readonly Rigidbody Rigidbody;

        public Vector Velocity;
        public Vector Position;
        public Vector AngularVelocity;
        public Quaternion Rotation;

        public PhysicsState(Rigidbody rigidbody)
        {
            Rigidbody = rigidbody;

            Velocity = new Vector();
            Position = new Vector();
            AngularVelocity = new Vector();
            Rotation = new Quaternion();
        }

    }
}
