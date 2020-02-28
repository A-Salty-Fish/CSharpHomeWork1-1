using System;

namespace PrimeNumber
{
    class Program
    {
        static void Main(string[] args)
        {
            long BeginTime = DateTime.Now.Ticks;
            try
            {
                string input = Console.ReadLine();
                int num = int.Parse(input);
                if (num <= 0)
                {
                    throw new Exception("Input must be positive.");
                }
                int prime = 2;
                while (num != 1)
                {
                    if (num % prime == 0)
                    {
                        num /= prime;
                        Console.Write(prime+" ");
                    }
                    else
                    {
                        prime++;
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("解析错误:" + e.Message);
            }

            long EndTime = DateTime.Now.Ticks;
            Console.Write("\n");
            Console.Write(EndTime-BeginTime);
        }
    }
}
