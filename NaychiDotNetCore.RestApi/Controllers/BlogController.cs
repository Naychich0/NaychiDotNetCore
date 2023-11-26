﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NaychiDotNetCore.RestApi.Models;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace NaychiDotNetCore.RestApi.Controllers
{
    //api/blog
    [Route("api/[controller]")]
    [ApiController]
    public class BlogController : ControllerBase
    {
        [HttpGet]
        public IActionResult GetBlogs()
        {

            //           try
            //           {
            //               AppDbContext db = new AppDbContext();
            //               List<BlogDataModel> lst = db.Blogs.ToList();
            //              BlogListResponseModel model = new BlogListResponseModel();
            //model.IsSuccess = true;
            //model.Message = "Success";


            //  BlogListResponseModel model = new BlogListResponseModel
            //{
            //IsSuccess = true,
            //Message = "Success",
            //Data = lst
            // };
            //return Ok(model);
            //}         
            //catch (Exception ex) 
            //{
            //return Ok(new BlogListResponseModel
            //{
            //IsSuccess = false,
            //Message = ex.Message,
            //});
            //}

            AppDbContext db = new AppDbContext();
            List<BlogDataModel> lst = db.Blogs.ToList();
            BlogListResponseModel model = new BlogListResponseModel
            {
                IsSuccess = true,
                Message = "Success",
                Data = lst,
            };
            return Ok(model);

        }

        [HttpGet("{id}")]
        public IActionResult GetBlog(int id)
        {
            AppDbContext db = new AppDbContext();
            var item = db.Blogs.FirstOrDefault(x => x.Blog_Id == id);
            if (item is null)
            {
                var response = new { IsSuccess = false, Message = "No data found." };
                return NotFound(response);
            }
            return Ok(item);
        }

        [HttpPost]
        public IActionResult CreateBlog(BlogDataModel blog)
        {
            AppDbContext db = new AppDbContext();
            db.Blogs.Add(blog);
            var result = db.SaveChanges();

            BlogResponseModel model = new BlogResponseModel()
            {
                IsSuccess = result > 0,
                Message = result > 0 ? "Saving Successful." : "Saving Failed.",
                Data = blog
            };
            return Ok(model);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateBlog(int id, BlogDataModel blog)
        {
            AppDbContext db = new AppDbContext();
            var item = db.Blogs.FirstOrDefault(x => x.Blog_Id == id);
            if (item is null)
            {
                var response = new { IsSuccess = false, Message = "No data found." };
                return NotFound(response);
            }

            //item = blog;

            item.Blog_Title = blog.Blog_Title;
            item.Blog_Author = blog.Blog_Author;
            item.Blog_Content = blog.Blog_Content;

            var result = db.SaveChanges();

            BlogResponseModel model = new BlogResponseModel()
            {
                IsSuccess = result > 0,
                Message = result > 0 ? "Updating Successful." : "Updating Failed.",
                Data = item
            };
            return Ok(model);
        }

        [HttpPatch("{id}")]
        public IActionResult PatchBlog(int id, BlogDataModel blog)
        {
            AppDbContext db = new AppDbContext();
            var item = db.Blogs.FirstOrDefault(x => x.Blog_Id == id);
            if (item is null)
            {
                var response = new { IsSuccess = false, Message = "No data found." };
                return NotFound(response);
            }

            if (!string.IsNullOrEmpty(blog.Blog_Title))
            {
                item.Blog_Title = blog.Blog_Title;
            }
            if (!string.IsNullOrEmpty(blog.Blog_Author))
            {
                item.Blog_Author = blog.Blog_Author;
            }
            if (!string.IsNullOrEmpty(blog.Blog_Content))
            {
                item.Blog_Content = blog.Blog_Content;
            }

            var result = db.SaveChanges();

            BlogResponseModel model = new BlogResponseModel()
            {
                IsSuccess = result > 0,
                Message = result > 0 ? "Updating Successful." : "Updating Failed.",
                Data = item
            };
            return Ok(model);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteBlog(int id)
        {
            AppDbContext db = new AppDbContext();
            var item = db.Blogs.FirstOrDefault(x => x.Blog_Id == id);
            if (item is null)
            {
                var response = new { IsSuccess = false, Message = "No data found." };
                return NotFound(response);
            }

            db.Blogs.Remove(item);
            var result = db.SaveChanges();

            BlogResponseModel model = new BlogResponseModel()
            {
                IsSuccess = result > 0,
                Message = result > 0 ? "Deleting Successful." : "Deleting Failed.",
            };
            return Ok(model);
        }

    }
}
