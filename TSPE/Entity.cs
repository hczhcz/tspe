namespace TSPE
{
    using TSPE.Utils;
    using TSPE.Physics;

    public class Entity
    {
        public readonly Manager Manager;

        public readonly Inertia Inertia;
        public Input Input { get; private set; }

        public Time Time { get; private set; }
        public State State { get; private set; }

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

            Time = new Time();
            State = new State(
                velocity,
                position,
                angularVelocity,
                rotation
            );
        }

        public void Simulate()
        {
            State.Simulate(Input, Time);
            Input = new Input(this);
        }
    }
}
