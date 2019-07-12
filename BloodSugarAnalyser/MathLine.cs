using System.Data;
using System.Drawing;

namespace BloodSugarAnalyser
{
    /// <summary>
    /// Class for object representing a straight line in a 2 dimensional coordinate system.
    /// </summary>
    /// <remarks>
    /// This class is a lite copy of the TrendLine class in the CardReaderLibrary repository.
    /// https://github.com/klanderfri/CardReaderLibrary/tree/master/CardReaderLibrary
    /// </remarks>
    class MathLine
    {
        /// <summary>
        /// The slope of the line.
        /// </summary>
        public float Slope { get; private set; }

        /// <summary>
        /// The offset of the line.
        /// </summary>
        public float Offset { get; private set; }

        /// <summary>
        /// Creates an object representing a straight line in a 2 dimensional coordinate system.
        /// </summary>
        /// <param name="slope">The slope of the line.</param>
        /// <param name="offset">The offset of the line.</param>
        public MathLine(float slope, float offset)
        {
            Slope = slope;
            Offset = offset;
        }

        /// <summary>
        /// Creates an object representing a straight line in a 2 dimensional coordinate system.
        /// </summary>
        /// <param name="p1">The first point the line should intersect.</param>
        /// <param name="p2">The second point the line should intersect.</param>
        public MathLine(PointF p1, PointF p2)
        {
            Slope = (p2.Y - p1.Y) / (p2.X - p1.X);
            Offset = p1.Y - Slope * p1.X;
        }

        /// <summary>
        /// Gets the Y coordinate from the X coordinate.
        /// </summary>
        /// <param name="x">The X coordinate.</param>
        /// <returns>The Y coordinate.</returns>
        public float GetY(float x)
        {

            return Slope * x + Offset;
        }

        /// <summary>
        /// Gets the X coordinate from the Y coordinate.
        /// </summary>
        /// <param name="y">The Y coordinate.</param>
        /// <returns>The X coordinate.</returns>
        public float GetX(float y)
        {

            return (y - Offset) / Slope;
        }

        /// <summary>
        /// Gets the point in which two lines intersect.
        /// </summary>
        /// <param name="lineA">The first line.</param>
        /// <param name="lineB">The second line.</param>
        /// <returns>The point in which the two lines intersect.</returns>
        public static PointF GetIntersectionPoint(MathLine lineA, MathLine lineB)
        {
            //Implemented according to:
            //https://en.wikipedia.org/w/index.php?title=Line%E2%80%93line_intersection&oldid=815048738#Given_the_equations_of_the_lines

            if (lineA.Slope == lineB.Slope)
            {
                throw new ConstraintException("The slopes are identical, meaning the lines are parallel and has no intersection point!");
            }

            float x = (lineB.Offset - lineA.Offset) / (lineA.Slope - lineB.Slope); //Will never be division by zero due to the exception above.
            float y = lineA.GetY(x);

            PointF intersection = new PointF(x, y);

            return intersection;
        }
    }
}
