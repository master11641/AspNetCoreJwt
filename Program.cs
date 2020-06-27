using System;
using System.Linq;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;

namespace core
{
    public class Program
    {
        public static void Main(string[] args)
        {
            //using (var db = new BloggingContext())
           // {
                // Create
                // Console.WriteLine("Inserting a new blog");
                // db.Add(new Blog { Url = "http://blogs.msdn.com/adonet" });
                // db.SaveChanges();

                // // Read
                // Console.WriteLine("Querying for a blog");
                // var blog = db.Blogs
                //     .OrderBy(b => b.BlogId)
                //     .FirstOrDefault();               
                // Console.WriteLine("Updating the blog and adding a post");               
                // blog.Posts.Add(
                //     new Post
                //     {
                //         Title = "Hello World",
                //         Content = "I wrote an app using EF Core!",
                //         //BlogId = blog.BlogId
                //     });
                // db.SaveChanges();

                // Delete
                //Console.WriteLine("Delete the blog");
               // db.Remove(blog);
               // db.SaveChanges();
           // }
            BuildWebHost(args).Run();
        }

        public static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .UseUrls("http://localhost:5000", "http://192.168.1.113:5000")
                .Build();
    }
}
