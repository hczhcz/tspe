namespace TSPE.Utils
{
    using System;

    public struct Quaternion
    {
        public readonly double W;
        public readonly Vector Direction;

        public Quaternion(double w, Vector direction)
        {
            W = w;
            Direction = direction;
        }

        public Quaternion Scale(double scalar)
        {
            return new Quaternion(W * scalar, Direction.Scale(scalar));
        }

        public double Length()
        {
            return Math.Sqrt(
                W * W
                    + Direction.X * Direction.X
                    + Direction.Y * Direction.Y
                    + Direction.Z * Direction.Z
            );
        }

        public Quaternion Normalize()
        {
            return Scale(1 / Length());
        }

        public Quaternion Flip()
        {
            return new Quaternion(W, Direction.Scale(-1));
        }

        public Quaternion Transform(Quaternion quaternion)
        {
            return new Quaternion(
                W * quaternion.W - Direction.Dot(quaternion.Direction),
                Direction.Cross(quaternion.Direction)
                    .Add(Direction.Scale(quaternion.W))
                    .Add(quaternion.Direction.Scale(W))
            );
        }
    }
}
