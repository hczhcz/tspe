namespace TSPE.Physics
{
    using JetBrains.Annotations;
    using TSPE.Utils;

    public class Time
    {
        public readonly double TimeDelta = 0.02;

        public bool Forward { get; private set; }
        public double Timestamp { get; private set; }
        public long Frame { get; private set; }

        public Time()
        {
            Forward = true;
            Timestamp = 0;
            Frame = 0;
        }

        public void Flip()
        {
            Forward = !Forward;
        }

        public double Step()
        {
            Timestamp += Forward ? TimeDelta : -TimeDelta;
            Frame += Forward ? 1 : -1;

            return TimeDelta;
        }
    }
}
