namespace TSPE
{
    using System;
    using System.Collections.Generic;
    using TSPE.Utils;
    using TSPE.Physics;

    public class Entity
    {
        public enum InputMode
        {
            record,
            overlay,
            replay
        }

        public readonly Inertia Inertia;

        public InputMode Mode { get; private set; }
        public IDictionary<long, Input> Inputs;
        public Input Input
        {
            get
            {
                if (Mode == InputMode.record || Mode == InputMode.overlay)
                {
                    return Inputs[Time.Frame];
                }

                throw new Exception();
            }
        }

        public Time Time { get; private set; }
        public State State { get; private set; }

        // TODO: constraints

        public Entity(
            double mass,
            Vector inertiaTensor,
            Quaternion inertiaTensorRotation,
            Vector velocity,
            Vector position,
            Vector angularVelocity,
            Quaternion rotation
        )
        {
            Inertia = new Inertia(
                mass,
                inertiaTensor,
                inertiaTensorRotation
            );

            Mode = InputMode.record;
            Inputs = new Dictionary<long, Input>();

            Time = new Time();
            State = new State(
                velocity,
                position,
                angularVelocity,
                rotation
            );

            Inputs[Time.Frame] = new Input(this);
        }

        public void Flip()
        {
            Time.Flip();
            State.Flip();
        }

        public void Prepare()
        {
            if (Mode == InputMode.record)
            {
                if (Inputs.ContainsKey(Time.Frame))
                {
                    throw new Exception();
                }

                Inputs[Time.Frame] = new Input(this);
            }
            else if (Mode == InputMode.overlay)
            {
                if (!Inputs.ContainsKey(Time.Frame))
                {
                    Inputs[Time.Frame] = new Input(this);
                }
            }
        }

        public void Simulate()
        {
            if (Inputs.ContainsKey(Time.Frame))
            {
                Input input = Inputs[Time.Frame];
                double timeStamp = Time.Step();

                State.Simulate(input, timeStamp);
            }
            else
            {
                Time.Step();
            }
        }
    }
}
