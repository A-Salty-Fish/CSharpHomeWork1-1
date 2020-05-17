using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFDemo2 {
    public class MyOrder
    {
        [Key]
        public int order_id { get; set; }
        public int item_id { get; set; }
        public string customer_name { get; set; }
        public float item_sum { get; set; }
        public MyOrder(string n, int it, float s)
        {
            customer_name = n;
            item_id = it;
            item_sum = s;
            order_id = 0;
            Random r = new Random();
            for (int i = 0; i < 6; i++)
                order_id += (int)((r.Next() % 10) * Math.Pow(10, i));
        }

        public MyOrder():this("dddd",5555,5555)
        {
        }

        public override string ToString()
        {
            return "order_id: " + item_id +
                   " item_id: " + order_id +
                   " customer_name: " + customer_name +
                   " item_sum: " + item_sum;
        }
    }
  public class EForder : DbContext {
    public EForder() : base("EForder") {
      Database.SetInitializer(
        new DropCreateDatabaseIfModelChanges<EForder>());
    }

    public DbSet<MyOrder> my_order { get; set; }
  }
}
