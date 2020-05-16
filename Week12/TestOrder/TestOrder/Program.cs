using System;
using MySql.Data.MySqlClient;

namespace TestOrder
{
    class Order
    {

        public int Id { get; set; }
        public string name { get; set; }
        public int ItemId { get; set; }
        public float ItemSum { get; set; }

        public Order(string n, int it, float s)
        {
            name = n;
            ItemId = it;
            ItemSum = s;
            Id = 0;
            Random r=new Random();
            for (int i = 0; i < 6; i++)
                Id += (int) ((r.Next() % 10) * Math.Pow(10, i));
        }

    }

    class DataBaseService
    {
        private MySqlConnection conn;

        public DataBaseService(MySqlConnection c)
        {
            conn = c;
        }
        public void Search()
        {
            string sql = "select * from myorder";
            MySqlCommand cmd = new MySqlCommand(sql, conn);
            using (MySqlDataReader reader = cmd.ExecuteReader())//执行ExecuteReader()返回一个MySqlDataReader对象
            {
                while (reader.Read())//初始索引是-1，执行读取下一行数据，返回值是bool
                {
                    Console.WriteLine("ID: " + reader.GetInt32("order_id") +
                                      " Name: " + reader.GetString("customer_name") +
                                      " Sum: " + reader.GetString("item_sum"));
                }
            }
        }

        public void Add(Order order)
        {
            using (MySqlCommand cmd = new MySqlCommand("",conn))
            {
                cmd.CommandText = "insert into myorder" +
                                  "(order_id, customer_name, item_id, item_sum)" +
                                  "VALUES" +
                                  "(@order_id,@customer_name,@item_id,@item_sum);"
                    ;
                cmd.Parameters.AddWithValue("@order_id", order.Id);
                cmd.Parameters.AddWithValue("@customer_name", order.name);
                cmd.Parameters.AddWithValue("@item_id", order.ItemId);
                cmd.Parameters.AddWithValue("@item_sum", order.ItemSum);
                cmd.Prepare();
                cmd.ExecuteNonQuery();
            }
        }

        public void Delete(int id)
        {
            using (MySqlCommand cmd = new MySqlCommand("", conn))
            {
                cmd.CommandText = "delete from myorder " +
                                  " where order_id=@order_id"
                    ;
                cmd.Parameters.AddWithValue("@order_id", id);
                cmd.Prepare();
                if (cmd.ExecuteNonQuery() == 0)
                    Console.WriteLine("no this id");
            }
        }

        public void Modify(int id, Order order)
        {
            using (MySqlCommand cmd = new MySqlCommand("", conn))
            {
                cmd.CommandText = "update myorder " +
                                  "set customer_name = @customer_name," +
                                  "item_id = @item_id, item_sum=@item_sum " +
                                  "where order_id = @order_id"
                ;
                cmd.Parameters.AddWithValue("@order_id", id);
                cmd.Parameters.AddWithValue("@customer_name", order.name);
                cmd.Parameters.AddWithValue("@item_id", order.ItemId);
                cmd.Parameters.AddWithValue("@item_sum", order.ItemSum);
                cmd.Prepare();
                if (cmd.ExecuteNonQuery()==0)
                    Console.WriteLine("no this id");
            }
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            String connetStr = "server=127.0.0.1;port=3306;user=root;password=; database=testorder;";
            using (MySqlConnection conn = new MySqlConnection(connetStr) )
            {
                try//测试
                {
                    conn.Open();//打开通道，建立连接，可能出现异常,使用try catch语句
                    Console.WriteLine("已经建立连接");
                    DataBaseService dataBaseService = new DataBaseService(conn);
                    dataBaseService.Search();

                    Console.Write("\n");
                    Order order=new Order("d",1002,(float)340.5);
                    //dataBaseService.Add(order);
                    //dataBaseService.Search();

                    Console.Write("\n");
                    dataBaseService.Delete(645847);
                    dataBaseService.Search();

                    Console.Write("\n");
                    Order newOrder = new Order("z",1003, (float)350.5);
                    dataBaseService.Modify(461892,newOrder);
                    dataBaseService.Search();

                }
                catch (MySqlException ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }

    }
}
