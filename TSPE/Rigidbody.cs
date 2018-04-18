namespace TSPE
{
    using TSPE.Utils;

    public class Rigidbody
    {
        public readonly Manager Manager;

        public readonly Inertia Inertia;

        protected PhysicsInput PhysicsInput;
        protected PhysicsState PhysicsState;

        // TODO: constraints

        public Rigidbody(
            Manager manager,
            double mass,
            Vector inertiaTensor,
            Quaternion inertiaTensorRotation
        )
        {
            Manager = manager;

            Inertia = new Inertia(
                this,
                mass,
                inertiaTensor,
                inertiaTensorRotation
            );

            PhysicsInput = new PhysicsInput(this);

            PhysicsState = new PhysicsState(this);
        }

        public Vector ToGlobalPosition(Vector position)
        {
            return position + PhysicsState.Position;
        }

        public Vector ToGlobalDirection(Vector direction)
        {
            return PhysicsState.Rotation.Transform(direction);
        }

        public Quaternion ToGlobalRotation(Quaternion rotation)
        {
            return PhysicsState.Rotation.Transform(rotation);
        }

        public Vector ToGlobalPoint(Vector point)
        {
            return ToGlobalPosition(ToGlobalDirection(point));
        }

        public Vector ToLocalPosition(Vector position)
        {
            return position - PhysicsState.Position;
        }

        public Vector ToLocalDirection(Vector direction)
        {
            return PhysicsState.Rotation.Inverse().Transform(direction);
        }

        public Quaternion ToLocalRotation(Quaternion rotation)
        {
            return PhysicsState.Rotation.Inverse().Transform(rotation);
        }

        public Vector ToLocalPoint(Vector point)
        {
            return ToLocalDirection(ToLocalPosition(point));
        }
    }
}
