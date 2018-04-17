namespace TSPE.Utils
{
    using System;
    using JetBrains.Annotations;

    public struct Vector
    {
        public readonly double X;
        public readonly double Y;
        public readonly double Z;

        public Vector(double x, double y, double z)
        {
            X = x;
            Y = y;
            Z = z;
        }

        [Pure]
        public Vector Add(Vector vector)
        {
            return new Vector(
                X + vector.X,
                Y + vector.Y,
                Z + vector.Z
            );
        }

        [Pure]
        public Vector Scale(double scalar)
        {
            return new Vector(
                X * scalar,
                Y * scalar,
                Z * scalar
            );
        }

        [Pure]
        public Vector Flip()
        {
            return new Vector(
                -X,
                -Y,
                -Z
            );
        }

        [Pure]
        public Vector Inverse()
        {
            return new Vector(
                1 / X,
                1 / Y,
                1 / Z
            );
        }

        [Pure]
        public double Length()
        {
            return Math.Sqrt(X * X + Y * Y + Z * Z);
        }

        [Pure]
        public Vector Normalize()
        {
            return Scale(1 / Length());
        }

        [Pure]
        public double Dot(Vector vector)
        {
            return X * vector.X + Y * vector.Y + Z * vector.Z;
        }

        [Pure]
        public Vector Cross(Vector vector)
        {
            return new Vector(
                Y * vector.Z - Z * vector.Y,
                Z * vector.X - X * vector.Z,
                X * vector.Y - Y * vector.X
            );
        }
    }
}
