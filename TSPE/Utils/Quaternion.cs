namespace TSPE.Utils
{
    using System;
    using JetBrains.Annotations;

    public struct Quaternion
    {
        public readonly double W;
        public readonly Vector Direction;

        public Quaternion(double w, Vector direction)
        {
            W = w;
            Direction = direction;
        }

        [Pure]
        public Quaternion Scale(double scalar)
        {
            return new Quaternion(W * scalar, Direction.Scale(scalar));
        }

        [Pure]
        public Quaternion Flip()
        {
            return new Quaternion(W, Direction.Flip());
        }

        [Pure]
        public double Length()
        {
            return Math.Sqrt(
                W * W
                    + Direction.X * Direction.X
                    + Direction.Y * Direction.Y
                    + Direction.Z * Direction.Z
            );
        }

        [Pure]
        public Quaternion Normalize()
        {
            return Scale(1 / Length());
        }

        [Pure]
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
