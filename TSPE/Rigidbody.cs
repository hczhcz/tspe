namespace TSPE
{
    using TSPE.Utils;
    using TSPE.Physics;

    public class Rigidbody
    {
        public readonly Manager Manager;

        public readonly Inertia Inertia;
        public Input Input;
        public State State;

        public double TimeDelta = 0.02;

        // TODO: constraints

        public Rigidbody(
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
                this,
                mass,
                inertiaTensor,
                inertiaTensorRotation
            );

            Input = new Input(
                this
            );

            State = new State(
                this,
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
