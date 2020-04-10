using OrderManageSystem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinFormOrderSystem
{
    class WinFormServiceIO : IServiceIO
    {
        public long DeleteOrderIn()
        {
            throw new NotImplementedException();
        }

        public string GetImportFileName()
        {
            return null;
        }
        public string GetExportFileName()
        {
            throw new NotImplementedException();
        }
        public void InputOrderIO(Order order)
        {
            throw new NotImplementedException();
        }

        public void InputOrderItemIO(int i, OrderItem orderItem)
        {
            throw new NotImplementedException();
        }

        public long ModifyOrderByNumIn()
        {
            throw new NotImplementedException();
        }

        public string SearchByCustomerNameIO()
        {
            throw new NotImplementedException();
        }

        public long SearchByOrderNumIO()
        {
            throw new NotImplementedException();
        }

        public SearchWay SearchBySomeWayIO()
        {
            throw new NotImplementedException();
        }

        public string ShowAllOrdersOut(List<Order> orderlList)
        {
            string result = "";
            if (orderlList.Count == 0) //空表
                result += "----------\r\nNone order.\r\n";
            else //非空则遍历
            {
                result += "----------\r\nHere are the all orders:\r\r\n";
                int i = 1;
                foreach (var x in orderlList)
                {
                    result += "----------\r\nOrder " + i + ":\r\n";
                    result += x.ToString();
                    i++;
                }
            }

            return result;
        }

        public void ShowOrderOut(Order order, ShowWay showWay, int n = 0)
        {
            throw new NotImplementedException();
        }

        public SortWay SortBySomeWayIO()
        {
            throw new NotImplementedException();
        }

        public void StartMenu()
        {
            throw new NotImplementedException();
        }
    }
}
