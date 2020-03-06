using System;
using System.Collections.Generic;
using System.Text;
using Shapes;
namespace Factory
{
    class Factory
    {
        private const int KIND = 3;
        public static Shape RamdonCreateShape()
        {
            Random rd = new Random();
            Shape shape;
            if (rd.Next() % KIND == 0)
            {
                while (true)
                {
                    double x = Math.Abs(rd.Next()) % 20;
                    double y = Math.Abs(rd.Next()) % 20;
                    double z = Math.Abs(rd.Next()) % 20;
                    shape = new TriAngle(x, y, z);
                    if (shape.IsShapeLegal())
                    {
                        Console.Write($"TriAngel:({x},{y},{z}), Area:{shape.GetArea()}\n");
                        return shape;
                    }
                }
            }
            else if (rd.Next() % KIND == 1)
            {
                while (true)
                {
                    double x = Math.Abs(rd.Next()) % 20;
                    double y = Math.Abs(rd.Next()) % 20;
                    shape = new RectTangle(x, y);
                    if (shape.IsShapeLegal())
                    {
                        Console.Write($"Rectangle:({x},{y}), Area:{shape.GetArea()}\n");
                        return shape;
                    }
                }
            }
            else
            {
                while (true)
                {
                    double x = Math.Abs(rd.Next()) % 20;
                    shape = new Square(x);
                    if (shape.IsShapeLegal())
                    {
                        Console.Write($"Square:({x}), Area:{shape.GetArea()}\n");
                        return shape;
                    }
                }
            }
        }
    }
}
