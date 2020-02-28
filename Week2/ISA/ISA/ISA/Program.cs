using System;

namespace ISA
{
    class Program
    {
        static void Main(string[] args)
        {
            int InputNum()
            {
                string input = Console.ReadLine();
                int num;
                if (!int.TryParse(input, out num))
                {
                    throw new Exception("Input is invalid.");
                }

                return num;
            }

            void ISA(int n)
            {
                if (n <= 1)
                    throw new Exception("Invalid Input");
                //声明变量
                int[] Num = new int[n + 1];
                bool[] Flag = new bool[n + 1];
                //初始化
                for (int i = 0; i < n + 1; i++) Num[i] = i;
                for (int i = 0; i < n + 1; i++) Flag[i] = true;
                Flag[0] = false; Flag[1] = false;
                //遍历
                for (int prime = 2; prime < n/2; prime++)
                {
                    if (Flag[prime])
                    {
                        for (int i = 2; i * prime <= n; i++)
                        {
                            Flag[i * prime] = false;
                        }
                    }
                }
                //打印
                for (int i = 2; i < n + 1; i++)
                {
                    if (Flag[i])
                        Console.Write(i + " ");
                }
            }
            //调用
            try
            {
                ISA(InputNum());
            }
            catch (Exception e)
            {
                Console.Write("Error:"+e);
            }
        }
    }
}
