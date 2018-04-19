namespace TSPE
{
    using TSPE.Utils;
    using TSPE.Physics;

    public class Entity
    {
        public readonly Manager Manager;

        public readonly Inertia Inertia;
        public Input Input { get; private set; }
        public State State { get; private set; }

        public double TimeDelta = 0.02;

        // TODO: constraints

        public Entity(
            Manager manager,
            double mass,
            Vector inertiaTensor,
            Quaternion inertiaTensorRotation,
            Vector velocity,
            Vector position,
            Vector angularVelocity,
            Quaternion rotation
        )
        {
            Manager = manager;

            Inertia = new Inertia(
                mass,
                inertiaTensor,
                inertiaTensorRotation
            );

            Input = new Input(
                this
            );

            State = new State(
                velocity,
                position,
                angularVelocity,
                rotation
            );
        }

        public void Simulate()
        {
            State.Simulate(Input, TimeDelta);
            Input = new Input(this);
        }
    }
}
