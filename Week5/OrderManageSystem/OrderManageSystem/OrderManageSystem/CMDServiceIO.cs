using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace OrderManageSystem
{
    interface IServiceIO
    {
        public abstract void InputOrderItemIO(int i, OrderItem orderItem);
        public abstract void InputOrderIO(Order order);
        public abstract void DeleteOrderIO(List<Order> orderlList);
        public abstract void ModifyOrderIO(List<Order> orderlList);
        public abstract void ShowAllOrdersIO(List<Order> orderlList);
        public abstract void ShowOrderIO(Order order, State state);
        public abstract int SearchBySomeWayIO();
        public abstract void SearchByOrderNumIO(List<Order> orderList);
        public abstract void SearchByCustomerName(List<Order> orderList);
        public abstract int SortBySomeWayIO();
        public abstract void StartMenu();
    }
    class CMDServiceIO: IServiceIO //客户端命令行交互接口
    {
        public void InputOrderItemIO(int i,OrderItem orderItem)
        {
            string goodsName;
            double goodsPrice;
            int goodsNum;
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Write("----------\nPlease enter the goods" + (i + 1) + "'s name:");
            goodsName = Console.ReadLine();
            Console.Write("Please enter the goods" + (i + 1) + "'s price:");
            goodsPrice = double.Parse(Console.ReadLine());
            Console.Write("Please enter the goods" + (i + 1) + "'s number:");
            goodsNum = int.Parse(Console.ReadLine());
            orderItem.goods = new Goods(goodsName, goodsPrice, goodsNum);
        }

        public void InputOrderIO(Order order)
        {
            //输入用户名
            string customerName;
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("----------\nPlease enter the Custormer's name:");
            customerName = Console.ReadLine();
            if (customerName == null) throw new Exception("name can't be empty.\n");
            Custormer custormer = new Custormer(customerName);
            order.custormer = custormer;
            //创建订单项目
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("----------\nPlease enter the order item's num:");
            int orderItemNum = 0;
            orderItemNum = int.Parse(Console.ReadLine());
            if (orderItemNum < 0) throw new Exception("Num must be positive.\n");
            else
            {
                for (int i = 0; i < orderItemNum; i++)//循环添加item
                {
                    //读取商品信息
                    OrderItem orderItem = new OrderItem();
                    //新建订单项目
                    InputOrderItemIO(i,orderItem);
                    order.AddItem(orderItem);
                    //添加订单项
                    Console.Write('\n');
                }
            }
        }

        public void DeleteOrderIO(List<Order> orderlList)
        {
            Console.Write("----------\nPlease enter the Order Num you want to delete:");
            long OrderNum = -1;
            OrderNum = long.Parse(Console.ReadLine());
            var result = orderlList.Where(x => x.OrderNum == OrderNum);
            Order order = result.FirstOrDefault();
            if (order == null) throw new Exception("Invalid OrderNum\n");
            else
            {
                orderlList.Remove(order);
                ShowOrderIO(order,State.Delete);
            }
        }

        public void ModifyOrderIO(List<Order> orderlList)
        {
            long OrderNum = -1;
            Console.Write("----------\nPlease enter the Order Num you want to modify:");
            OrderNum = long.Parse(Console.ReadLine());
            var result = orderlList.Where(x => x.OrderNum == OrderNum);
            Order order = result.FirstOrDefault();
            if (order == null) throw new Exception("Invalid OrderNum\n");
            else
            {
                Console.Write("----------\nThe Original order:");
                Console.Write(order);
                Console.Write("----------\nNow Modify it:\n");
                order.DeleteItem(); //删除订单项
                InputOrderIO(order); //输入订单
                //打印修改后的订单
                
            }
        }

        public void ShowOrderIO(Order order,State state)
        {
            switch (state)
            {
                case State.Add:
                    Console.Write("----------\nYou add the Order:\n");
                    Console.Write(order);
                    break;
                case State.Delete:
                    Console.Write("\nYou delete the order:");
                    Console.Write(order);
                    break;
                case State.Modify:
                    Console.Write("----------\nThe Modified order:");
                    Console.Write(order);
                    break;
                case State.Search:
                    Console.Write("----------\nThe Search result:");
                    Console.Write(order);
                    break;
            }
        }

        public void ShowAllOrdersIO(List<Order>orderlList)
        {
            Console.ForegroundColor = ConsoleColor.DarkRed;
            if (orderlList.Count == 0)//空表
                Console.Write("----------\nNone order.\n");
            else//非空则遍历
            {
                Console.Write("----------\nHere are the all orders:\n");
                int i = 1;
                foreach (var x in orderlList)
                {
                    Console.Write("----------\nOrder " + i + ":\n");
                    Console.Write(x);
                    i++;
                }
            }
        }

        public int SearchBySomeWayIO()
        {
            Console.Write("----------\n1 to search by ordernum; 2 to search by name:");
            int input = -1;
            input = int.Parse(Console.ReadLine());
            return input;
        }

        public void SearchByOrderNumIO(List<Order> orderList)
        {
            Console.ForegroundColor = ConsoleColor.DarkBlue;
            long OrderNum = -1;
            Console.Write("----------\nPlease enter the Order Num you want to search:");
            OrderNum = long.Parse(Console.ReadLine());
            var result = orderList.Where(x => x.OrderNum == OrderNum);
            Order order = result.FirstOrDefault();
            if (order != null)
            {
                //ShowOrderIO(order,State.Search);
                Console.Write("----------\nThe Search result:");
                Console.Write(order);
            }
            else throw new Exception("Invalid OrderNum:" + OrderNum + '\n');
        }

        public void SearchByCustomerName(List<Order> orderList)//通过用户名搜索 价格升序
        {
            Console.ForegroundColor = ConsoleColor.DarkBlue;
            string CustormerName;
            Console.Write("----------\nPlease enter the Customer Name you want to search:");
            CustormerName = Console.ReadLine();
            var result = from x in orderList
                where x.custormer.Name == CustormerName
                orderby x.OrderNum
                select x;
            if (result.FirstOrDefault() != null)
            {
                foreach (var x in result)
                {
                    Console.Write(x);
                }
            }
            else Console.Write("\nNo result\n");
        }

        public int SortBySomeWayIO()
        {
            Console.Write("Please enter the way to sort. 1 by orderNum, 2 by data, 3 by sum : ");
            int input = 0;
            input = int.Parse(Console.ReadLine());
            return input;
        }

        public void StartMenu()
        {
            OrderService orderService = new OrderService();
            int num = -1;
            while (true)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write(
                    "----------\nPlease enter a number.\n 1 to add;   2 to delete;\n 3 to modify;4 to show all;\n 5 to search; 6 to Sort;\nothers to quit:");
                num = int.Parse(Console.ReadLine());
                switch (num)
                {
                    case 1:
                        orderService.AddOrder();
                        break;
                    case 2:
                        orderService.DeleteOrderByOrderNum();
                        break;
                    case 3:
                        orderService.ModifyOrderByOrderNum();
                        break;
                    case 4:
                        orderService.ShowAllOrders();
                        break;
                    case 5:
                        orderService.SearchBySomeWay();
                        break;
                    case 6:
                        orderService.SortBySomeWay();
                        break;
                    default: return;
                }
            }
        }
    }
}
