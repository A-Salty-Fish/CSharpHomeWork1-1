using System;
using System.Runtime.Serialization.Formatters;
using Shapes;
using Factory;

namespace Program
{
    class Program
    {
        static double SumArea()
        {
            double sum = 0;
            for (int i = 0; i < 20; i++)
            {
                Console.Write($"{i}: ");
                Shape shape = Factory.Factory.RamdonCreateShape();
                sum += shape.GetArea();
            }

            return sum;
        }
        static void Main(string[] args)
        {
            try
            {
                SumArea();
            }
            catch (Exception e)
            {
                Console.Write(e);
            }
        }
    }
}
