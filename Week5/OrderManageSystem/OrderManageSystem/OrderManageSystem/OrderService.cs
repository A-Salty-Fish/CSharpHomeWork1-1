using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
//写一个订单管理的控制台程序，能够实现添加订单、删除订单、修改订单、查询订单（按照订单号、商品名称、客户等字段进行查询）功能。
//提示：主要的类有Order（订单）、OrderItem（订单明细项），OrderService（订单服务），订单数据可以保存在OrderService中一个List中。
//在Program里面可以调用OrderService的方法完成各种订单操作。
//要求：
//（1）使用LINQ语言实现各种查询功能，查询结果按照订单总金额排序返回。订单号√
//（2）在订单删除、修改失败时，能够产生异常并显示给客户错误信息。√
//（3）作业的订单和订单明细类需要重写Equals方法，确保添加的订单不重复，每个订单的订单明细不重复。 √
//（4）订单、订单明细、客户、货物等类添加ToString方法，用来显示订单信息。√
//（5）OrderService提供排序方法对保存的订单进行排序。默认按照订单号排序，也可以使用Lambda表达式进行自定义排序。√
namespace OrderManageSystem
{
    enum State
    {
        Add,
        Delete,
        Modify,
        Show,
        Search
    };
    class OrderService //订单服务
    {
        private List<Order> orderList;
        private IServiceIO serviceIo;//

        public OrderService()
        {
            orderList = new List<Order>();
            serviceIo=new CMDServiceIO();//客户端命令行交互接口
        }

        public void StartMenu()
        {
            serviceIo.StartMenu();
        }

        public void AddOrder()
        {
            bool isRepeat = false;
            Order order;
            while (true)
            {
                order = new Order();
                foreach (Order x in orderList)
                {
                    if (x.Equals(order)) isRepeat = true;
                }
                if (!isRepeat) break;
            }
            //输入订单
            serviceIo.InputOrderIO(order);
            orderList.Add(order);
            //打印订单
            Console.ForegroundColor = ConsoleColor.Red;
            serviceIo.ShowOrderIO(order,State.Add);
        }

        public void ModifyOrderByOrderNum()//通过订单号修改订单 不改变订单号与客户
        {
            serviceIo.ModifyOrderIO(orderList);
        }

        public void DeleteOrderByOrderNum()//通过订单号删除订单
        {
            serviceIo.DeleteOrderIO(orderList);
        }

        public void ShowAllOrders()//打印所有的Order
        {
            serviceIo.ShowAllOrdersIO(orderList);
        }

        public void SearchBySomeWay()//搜索
        {
            int input = serviceIo.SearchBySomeWayIO();
            switch (input)
            {
                case 1: serviceIo.SearchByOrderNumIO(orderList);break;//通过订单号搜索
                case 2:serviceIo.SearchByCustomerName(orderList);break;//通过客户名搜索
            }
            
        }

        public void SortBySomeWay() //排序方式
        {
            Console.ForegroundColor = ConsoleColor.DarkGray;

            int input = serviceIo.SortBySomeWayIO();
            switch (input)
            {
                case 1: orderList.Sort((order1,order2)=>(int)(order1.OrderNum-order2.OrderNum));break;
                case 2: orderList.Sort((order1, order2) => DateTime.Compare(order1.orderTime, order2.orderTime));break;
                case 3:orderList.Sort((order1, order2) => order1.TotalSum.CompareTo(order2.TotalSum) ); break;
                default: throw new Exception("Invalid Input"); 
            }
        }

    }
}
