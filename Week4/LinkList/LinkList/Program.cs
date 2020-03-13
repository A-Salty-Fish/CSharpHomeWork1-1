using System;
//链表节点类
public class Node<T>
{
    public  Node<T> Next { get; set; }
    public T Data { get; set; }

    public Node(T t)
    {
        Next = null;
        Data = t;
    }
}
//泛型链表类
public class GenericList<T>
{
    private Node<T> head;
    private Node<T> tail;

    public GenericList()
    {
        tail = head = null;
    }

    public Node<T> Head
    {
        get => head;
    }

    public void Add(T t)
    {
        Node<T> n=new Node<T>(t);
        if (tail == null)
        {
            head = tail = n;
        }
        else
        {
            tail.Next = n;
            tail = n;
        }
    }

    public void ForEach(Action<T> action)
    {
        Node<T> h;
        h = head;
        if (h == null) { throw new Exception("Empty List."); }
        else
        {
            while (h != null)
            {
                action(h.Data);
                h = h.Next;
            }
        }
    }
}

namespace LinkList
{
    class Program
    {
        static void Main(string[] args)
        {
            GenericList<int> intList=new GenericList<int>();
            for (int i=0;i<10;i++) intList.Add(i);
            int max = Int32.MinValue; 
            int min=Int32.MaxValue;
            int sum = 0;
            intList.ForEach(s => { max = max < s ? s : max;});
            intList.ForEach(s => { min = min > s ? s : min;});
            intList.ForEach(s => { sum += s;});
            Console.Write("Max:"+max+'\n');
            Console.Write("Min:"+min+'\n');
            Console.Write("Sum:"+sum+'\n');
        }
    }
}
