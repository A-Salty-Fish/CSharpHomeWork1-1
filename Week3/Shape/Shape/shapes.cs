using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;
using System.Text;

namespace Shapes
{
    
    interface IGetArea
    {
        public double GetArea();
    }

    interface IIsShapeLegal
    {
        public bool IsShapeLegal();
    }

    abstract class Shape : IGetArea, IIsShapeLegal
    {
        protected int SideNum; //边数
        protected double[] SideLength; //边长

        public Shape()
        {
            SideNum = 0;
            SideLength = null;
        }

        public virtual double GetArea()
        {
            return 0;
        }

        public virtual bool IsShapeLegal()
        {
            return false;
        }
    }

    class TriAngle : Shape
    {
        public double this[int n]
        {
            get
            {
                if (n > 2||n<0)
                    throw new Exception("Invalid Index");
                else 
                    return SideLength[n];
            }
        }
        public TriAngle(double[] length)
        {
            SideNum = 3;
            if (length.Length != 3) throw new Exception("Number of Length should be 3.");
            SideLength = new double[3];
            for (int i = 0; i < 3; i++)
                SideLength[i] = length[i];
        }

        public TriAngle(double x, double y, double z)
        {
            SideNum = 3;
            SideLength = new double[3];
            SideLength[0] = x;
            SideLength[1] = y;
            SideLength[2] = z;
        }

        public override bool IsShapeLegal()
        {
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    for (int k = 0; k < 3; k++)
                    {
                        if (base.SideLength[k] <= 0 || SideLength[i] + SideLength[j] <= SideLength[k])
                            return false;
                    }
                }
            }

            return true;
        }

        public override double GetArea()
        {
            if (IsShapeLegal())
            {
                double p = 0;
                foreach (double length in SideLength)
                {
                    p += length;
                }

                p = p / 2;

                double S = p;

                foreach (double length in SideLength)
                {
                    S = S * (p - length);
                }

                S = Math.Sqrt(S);

                return S;
            }

            return 0;
        }
    }

    class RectTangle : Shape
    {
        public double width
        {
            get { return SideLength[0] > SideLength[2] ? SideLength[2] : SideLength[0]; }
        }

        public double length
        {
            get { return SideLength[0] > SideLength[2] ? SideLength[0] : SideLength[2]; }
        }

        public RectTangle(double[] length)
        {
            if (length.Length == 2)
            {
                SideNum = 4;
                SideLength = new double[4];
                for (int i = 0; i < 4; i++)
                    SideLength[i] = length[i / 2];
            }
            else if (length.Length == 4)
            {
                SideNum = 4;
                SideLength = new double[4];
                for (int i = 0; i < 4; i++)
                    SideLength[i] = length[i];
            }
        }

        public RectTangle(double x, double y)
        {
            SideLength = new double[4];
            SideNum = 4;
            SideLength[0] = x;
            SideLength[1] = x;
            SideLength[2] = y;
            SideLength[3] = y;
        }

        public override bool IsShapeLegal()
        {
            if (SideLength[0] <= 0 || SideLength[1] <= 0 || SideLength[2] <= 0 || SideLength[3] <= 0)
                    return false;
            if (Math.Abs(SideLength[0] - SideLength[1]) > 0.000000005|| Math.Abs(SideLength[2] - SideLength[3]) > 0.000000005)
                    return false;
            return true;
        }

        public override double GetArea()
        {
            if (IsShapeLegal())
                return SideLength[1] * SideLength[2];
            else return 0;
        }
    }

    class Square : Shape
    {
        public double length
        {
            get { return SideLength[0]; }
        }
        public Square(double length)
        {
            SideNum = 4;
            SideLength=new double[4];
            SideLength[0] = SideLength[1] = SideLength[2] = SideLength[3] = length;
        }

        public override bool IsShapeLegal()
        {
            return SideLength[0] > 0;
        }

        public override double GetArea()
        {
            return SideLength[0] * SideLength[0];
        }

    }
}
