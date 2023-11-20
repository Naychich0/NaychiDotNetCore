using NaychiDotNetCore.ConsoleApp.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NaychiDotNetCore.ConsoleApp.EFCoreExamples
{
    public class EFCoreExample
    {
        private readonly AppDbContext _dbContext;

        public EFCoreExample()
        {
            _dbContext = new AppDbContext();
        }
        public void Run()
        {
            Read();
            Edit(2);
            Create("Test20", "NCCho", "CR");
        }
        public void Read()
        {
          List<BlogDataModel> lst= _dbContext.Blogs.ToList();

            foreach (var item in lst)
            {
                Console.WriteLine(item.Blog_Id);
                Console.WriteLine(item.Blog_Title);
                Console.WriteLine(item.Blog_Author);
                Console.WriteLine(item.Blog_Content);
            }
        }

        public void Edit(int id)
        {

            //var item = _dbContext.Blogs.Where(x=>x.Blog_Id == id).FirstOrDefault();
            var item = _dbContext.Blogs.FirstOrDefault(x => x.Blog_Id == id);

            if(item is null)
            {
                Console.Write("No data found.");
                return;
            }
            
            
                Console.WriteLine(item.Blog_Id);
                Console.WriteLine(item.Blog_Title);
                Console.WriteLine(item.Blog_Author);
                Console.WriteLine(item.Blog_Content);
            
        }


        private void Create(string title, string author, string content)
        {
            #region Create

            BlogDataModel blog = new BlogDataModel
            {
                Blog_Title = title,
                Blog_Author = author,
                Blog_Content = content
            };

           _dbContext.Blogs.Add(blog);
            int result= _dbContext.SaveChanges();
            String message = result > 0 ? "Saving Successful." : "Saving Failed.";
            Console.WriteLine(message);
            Console.WriteLine(blog.Blog_Id);
            #endregion
        }

        private void Update(int id,string title,string author,string content) 
        {
            #region Update
            var blog = _dbContext.Blogs.FirstOrDefault(x => x.Blog_Id == id);
            if(blog is null)
            {
                Console.WriteLine("No data found!");
                    return;
            }

            blog.Blog_Content = content;    
            blog.Blog_Title = title;    
            blog.Blog_Author = author;
            blog.Blog_Id = id;
            
            int result = _dbContext.SaveChanges();
            String message = result > 0 ? "Updating Successful." : "Updating Failed.";
            Console.WriteLine(message);
            #endregion
        }

        private void Delete(int id)
        {
            #region Create

            var blog = _dbContext.Blogs.FirstOrDefault(x => x.Blog_Id == id);
            if (blog is null)
            {
                Console.WriteLine("No data found!");
                return;
            }

            _dbContext.Blogs.Remove(blog);
            int result = _dbContext.SaveChanges();
            String message = result > 0 ? "Deleting successful" : "Deleting Failed.";
            Console.WriteLine(message);
            #endregion
        }

    }

}
