namespace TSPE.Utils
{
    using JetBrains.Annotations;

    public struct Matrix
    {
        public readonly Vector U;
        public readonly Vector V;
        public readonly Vector W;

        public Matrix(Vector u, Vector v, Vector w)
        {
            U = u;
            V = v;
            W = w;
        }

        [Pure]
        public Matrix Transpose()
        {
            return new Matrix(
                new Vector(U.X, V.X, W.X),
                new Vector(U.Y, V.Y, W.Y),
                new Vector(U.Z, V.Z, W.Z)
            );
        }

        [Pure]
        public double Determinant()
        {
            return U.Dot(V.Cross(W));
        }

        [Pure]
        public Matrix Inverse()
        {
            Matrix transpose = Transpose();
            double determinant = Determinant();

            return new Matrix(
                transpose.V.Cross(transpose.W) / determinant,
                transpose.W.Cross(transpose.U) / determinant,
                transpose.U.Cross(transpose.V) / determinant
            ).Transpose();
        }

        [Pure]
        public Vector Transform(Vector vector)
        {
            return new Vector(
                U.Dot(vector),
                V.Dot(vector),
                W.Dot(vector)
            );
        }

        [Pure]
        public Matrix Transform(Matrix matrix)
        {
            Matrix transpose = matrix.Transpose();

            return new Matrix(
                Transform(transpose.U),
                Transform(transpose.V),
                Transform(transpose.W)
            ).Transpose();
        }
    }
}
