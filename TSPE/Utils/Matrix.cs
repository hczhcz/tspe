﻿namespace TSPE.Utils
{
    public struct Matrix
    {
        public Vector U;
        public Vector V;
        public Vector W;

        public Matrix(Vector u, Vector v, Vector w)
        {
            U = u;
            V = v;
            W = w;
        }

        public Matrix Add(Matrix matrix)
        {
            return new Matrix(
                U.Add(matrix.U),
                V.Add(matrix.V),
                W.Add(matrix.W)
            );
        }

        public Matrix Scale(double scalar)
        {
            return new Matrix(
                U.Scale(scalar),
                V.Scale(scalar),
                W.Scale(scalar)
            );
        }

        public Matrix Transpose()
        {
            return new Matrix(
                new Vector(U.X, V.X, W.X),
                new Vector(U.Y, V.Y, W.Y),
                new Vector(U.Z, V.Z, W.Z)
            );
        }

        public Vector Apply(Vector vector)
        {
            return new Vector(
                U.Dot(vector),
                V.Dot(vector),
                W.Dot(vector)
            );
        }

        public Matrix Transform(Matrix matrix)
        {
            Matrix tMatrix = matrix.Transpose();

            return new Matrix(
                Apply(tMatrix.U),
                Apply(tMatrix.V),
                Apply(tMatrix.W)
            ).Transpose();
        }

        public double Determinant()
        {
            return U.Dot(V.Cross(W));
        }

        public Matrix Inverse()
        {
            Matrix t = Transpose();

            return new Matrix(
                t.V.Cross(t.W),
                t.W.Cross(t.U),
                t.U.Cross(t.V)
            ).Transpose().Scale(1 / Determinant());
        }
    }
}
