using System;

namespace MORF.Solution
{
    public static class Triangle
    {
        public static double CalculateArea(int a, int b, int c)
        {
            Validate(a, b, c);

            // see http://en.wikipedia.org/wiki/Triangle#Using_Heron.27s_formula
            var s = (a + b + c) / 2;
            return Math.Sqrt(s * (s - a) * (s - b) * (s - c));
        }

        private static void Validate(int a, int b, int c)
        {
            // Sorting makes the validation easier:
            var sortedEdges = new[] { a, b, c };
            Array.Sort(sortedEdges);

            if (sortedEdges[0] <= 0)
                throw new InvalidTriangleException(InvalidTriangleException.ErrorCode.EdgeMustBePositiveNumber);

            if (sortedEdges[0] + sortedEdges[1] <= sortedEdges[2])
            {
                // see http://en.wikipedia.org/wiki/Triangle_inequality
                throw new InvalidTriangleException(InvalidTriangleException.ErrorCode.TriangleInequalityFailed);
            }
        }
    }

    public class InvalidTriangleException : Exception
    {
        public enum ErrorCode
        {
            EdgeMustBePositiveNumber,
            TriangleInequalityFailed,
        }

        public InvalidTriangleException(ErrorCode code) : base(CreateMessage(code)) { }

        private static string CreateMessage(ErrorCode code)
        {
            switch (code)
            {
                case ErrorCode.EdgeMustBePositiveNumber:
                    // TODO: localize message
                    return "Triangle edges must be positive numbers";
                default:
                    // TODO: localize message
                    return "The triangle is not valid: the longest edge should be smaller than the sum of other two.";
            }
        }
    }
}
