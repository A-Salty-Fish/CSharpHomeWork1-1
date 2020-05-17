using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFDemo2 {
    class DBService
    {
        public void Search(int id)
        {
            using (var context = new EForder())
            {
                var myOrder = context.my_order
                    .SingleOrDefault(b => b.order_id == id);
                if (myOrder != null) Console.WriteLine(myOrder);
            }
        }

        public void ShowAll()
        {
            using (var context = new EForder())
            {
                var orders = context.my_order.SqlQuery("SELECT * FROM myorders").ToList();
                foreach (var i in orders)
                {
                    Console.WriteLine(i);
                }
            }
        }

        public void Add(MyOrder myOrder)
        {
            using (var context = new EForder())
            {
                Console.WriteLine("Add:"+myOrder);
                context.my_order.Add(myOrder);
                context.SaveChanges();
            }
        }

        public void Delete(int id)
        {
            using (var context = new EForder())
            {
                var post = context.my_order.FirstOrDefault(p => p.order_id == id);
                if (post != null)
                {
                    Console.WriteLine("Delete:");
                    Console.WriteLine(post);
                    context.my_order.Remove(post);
                    context.SaveChanges();
                }
            }
        }


    }
  class Program {
      
    static void Main(string[] args)
    {
        DBService dbService=new DBService();
      using (var context = new EForder()) {
          dbService.ShowAll();
          Console.WriteLine("");
          MyOrder myOrder = new MyOrder("aaaaaa", 33, 99);
          dbService.Add(myOrder);
          Console.WriteLine("");
          dbService.Delete(33);
          dbService.ShowAll();
            }

            ////修改Id为newPostId的帖子(方法1)
            //using (var context = new EForder()) {
            //  var post = new Post() { PostId = newPostId, Title = "Test3",
            //    Content = "Hello world", BlogId = newBlogId,Comment = "just a test！" };
            //  context.Entry(post).State = EntityState.Modified;
            //  context.SaveChanges();
            //}

            ////修改Id为newPostId的帖子（方法2）
            //using (var context = new EForder()) {
            //  var post = context.Posts.FirstOrDefault(p => p.PostId == newPostId);
            //  if (post != null) {
            //    post.Content = "Hello world,EF!";
            //    post.Comment = "EF test！";
            //    context.SaveChanges();
            //  }
            //}

    }
  }



}
