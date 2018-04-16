namespace TSPE.Utils
{
    using System;

    public struct Vector
    {
        public double X;
        public double Y;
        public double Z;

        public Vector(double x, double y, double z)
        {
            X = x;
            Y = y;
            Z = z;
        }

        public Vector Add(Vector vector)
        {
            return new Vector(
                X + vector.X,
                Y + vector.Y,
                Z + vector.Z
            );
        }

        public Vector Scale(double scalar)
        {
            return new Vector(
                X * scalar,
                Y * scalar,
                Z * scalar
            );
        }

        public double Length()
        {
            return Math.Sqrt(X * X + Y * Y + Z * Z);
        }

        public Vector Normalize()
        {
            return Scale(1 / Length());
        }

        public double Dot(Vector vector)
        {
            return X * vector.X + Y * vector.Y + Z * vector.Z;
        }

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
