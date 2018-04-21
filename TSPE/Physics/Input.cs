namespace TSPE.Physics
{
    using TSPE.Utils;

    public class Input
    {
        private readonly Entity entity;

        public Vector Acceleration { get; private set; }
        public Vector Velocity { get; private set; }
        public Vector AngularAcceleration { get; private set; }
        public Vector AngularVelocity { get; private set; }

        public Input(Entity entity)
        {
            this.entity = entity;
        }

        public void AddAcceleration(Vector vector, bool force, bool local)
        {
            if (local)
            {
                vector = entity.State.ToGlobalDirection(vector);
            }

            if (force)
            {
                vector = entity.Inertia.ForceToAcceleration(vector);
            }

            Acceleration += vector;
        }

        public void AddAngularAcceleration(Vector vector, bool force, bool local)
        {
            if (!local)
            {
                vector = entity.State.ToLocalDirection(vector);
            }

            if (force)
            {
                vector = entity.Inertia.TorqueToAngularAcceleration(vector);
            }

            AngularAcceleration += vector;
        }

        public void AddAccelerationAtPosition(Vector vector, Vector position, bool force, bool local)
        {
            AddAcceleration(vector, force, local);

            if (!local)
            {
                vector = entity.State.ToLocalDirection(vector);
                position = entity.State.ToLocalPoint(position);
            }

            if (!force)
            {
                vector = entity.Inertia.AccelerationToForce(vector);
            }

            AddAngularAcceleration(position.Cross(vector), true, true);
        }

        public void AddVelocity(Vector vector, bool impulse, bool local)
        {
            if (local)
            {
                vector = entity.State.ToGlobalDirection(vector);
            }

            if (impulse)
            {
                vector = entity.Inertia.ForceToAcceleration(vector);
            }

            Velocity += vector;
        }

        public void AddAngularVelocity(Vector vector, bool impulse, bool local)
        {
            if (!local)
            {
                vector = entity.State.ToLocalDirection(vector);
            }

            if (impulse)
            {
                vector = entity.Inertia.TorqueToAngularAcceleration(vector);
            }

            AngularVelocity += vector;
        }

        public void AddVelocityAtPosition(Vector vector, Vector position, bool impulse, bool local)
        {
            AddVelocity(vector, impulse, local);

            if (!local)
            {
                vector = entity.State.ToLocalDirection(vector);
                position = entity.State.ToLocalPoint(position);
            }

            if (!impulse)
            {
                vector = entity.Inertia.AccelerationToForce(vector);
            }

            AddAngularVelocity(position.Cross(vector), true, true);
        }

        public void AddCollision(Entity other, Vector position, Vector normal)
        {
            // TODO: handle collision in the middle of simulation instead of beginning
            Vector relativeVelocity = normal
                * (other.State.Velocity - entity.State.Velocity).Dot(normal);

            AddVelocityAtPosition(
                2 * relativeVelocity * (
                    entity.Inertia.Mass * other.Inertia.Mass
                        / (entity.Inertia.Mass + other.Inertia.Mass)
                ),
                position,
                true,
                false
            );
        }
    }
}
