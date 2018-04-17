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
            double length = Length();

            return new Quaternion(W / length, Direction / length);
        }

        [Pure]
        public Quaternion Inverse()
        {
            return new Quaternion(W, -Direction);
        }

        [Pure]
        public Vector Transform(Vector vector)
        {
            return Direction.Cross(vector) + vector * W;
        }

        [Pure]
        public Quaternion Transform(Quaternion quaternion)
        {
            return new Quaternion(
                W * quaternion.W - Direction.Dot(quaternion.Direction),
                Direction.Cross(quaternion.Direction)
                    + Direction * quaternion.W
                    + quaternion.Direction * W
            );
        }
    }
}
