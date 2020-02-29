using System;

namespace Array
{
    //class MyArray<T>
    //    where T : struct, IComparable<T>
    //{
    //    private T[] Array;
    //    MyArray(T[] array)//构造函数
    //    {
    //        if (array.Length == 0)
    //        {
    //            throw new Exception("Empty Array.");
    //        }
    //        else
    //        {
    //            Array = new T[array.Length];
    //            for (int i = 0; i < array.Length; i++)
    //                Array[i] = array[i];
    //        }
    //    }
    //    public T MaxNum()//最大值
    //    {

    //        if (Array.Length == 0)
    //        {
    //            throw new Exception("Empty Array.");
    //        }

    //        T max = Array[0];
    //        foreach (var x in Array)
    //        {
    //            if (x.CompareTo(max) > 0) max = x;
    //        }

    //        return max;


    //    }

    //    public T MinNum()//最小值
    //    {

    //        if (Array.Length == 0)
    //        {
    //            throw new Exception("Empty Array.");
    //        }
    //        else
    //        {
    //            T min = Array[0];
    //            foreach (var x in Array)
    //            {
    //                if (x.CompareTo(min) < 0) min = x;
    //            }

    //            return min;
    //        }
    //    }

    //    public T Sum()//求和
    //    {

    //        if (Array.Length == 0)
    //        {
    //            throw new Exception("Empty Array");
    //        }

    //        dynamic sum = Array[0];
    //        for (int i = 1; i < Array.Length; i++)
    //        {
    //            dynamic num = Array[i];
    //            sum += num;
    //        }

    //        return (T)sum;

    //    }
    //}
    class MyArray
    {
        private int[] array;

        public MyArray(int[] num)
        {
            if (num.Length==0) throw new Exception("Empty Array.");
            array=new int[num.Length];
            for (int i = 0; i < num.Length; i++)
            {
                array[i] = num[i];
            }
        }


        public int Max()
        {
            if (array.Length == 0) throw new Exception("Empty Array.");
            int max = array[0];
            for (int i = 1; i < array.Length; i++)
            {
                if (array[i] > max) max = array[i];
            }

            return max;
        }

        public int Min()
        {
            if (array.Length == 0) throw new Exception("Empty Array.");
            int min = array[0];
            for (int i = 1; i < array.Length; i++)
            {
                if (array[i] < min) min = array[i];
            }

            return min;
        }

        public int Sum()
        {
            if (array.Length == 0) throw new Exception("Empty Array.");
            int sum = 0;
            foreach (int x in array)
            {
                sum += x;
            }

            return sum;
        }

        public double Average()
        {
            if (array.Length == 0) throw new Exception("Empty Array.");

            return (double)Sum() / array.Length;
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            int[] array = {1, 2, 3, 4, 5, 6, 7};
            try
            {
                MyArray myarray = new MyArray(array);
                Console.Write("MaxNum:" + myarray.Max());
                Console.Write("\nMinNum:" + myarray.Min());
                Console.Write("\nSum:" + myarray.Sum());
                Console.Write("\nAverage:" + myarray.Average());
            }
            catch (Exception e)
            {
                Console.Write("Error:"+e);
            }
        }
    }
}
