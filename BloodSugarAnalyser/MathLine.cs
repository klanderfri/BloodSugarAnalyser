using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Data;

namespace BloodSugarAnalyser
{
    class MathLine
    {
        public float Slope { get; private set; }
        public float Offset { get; private set; }

        public MathLine(float slope, float offset)
        {
            Slope = slope;
            Offset = offset;
        }

        public MathLine(PointF p1, PointF p2)
        {
            Slope = (p2.Y - p1.Y) / (p2.X - p1.X);
            Offset = p1.Y - Slope * p1.X;
        }

        public float GetY(float x)
        {

            return Slope * x + Offset;
        }

        public float GetX(float y)
        {

            return (y - Offset) / Slope;
        }

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
