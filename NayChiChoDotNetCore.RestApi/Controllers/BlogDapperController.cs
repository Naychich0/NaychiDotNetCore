﻿using Dapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Data.SqlClient;
using System.Reflection.Metadata;
using NayChiChoDotNetCore.RestApi.Models;


namespace NayChiChoDotNetCore.RestApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogDapperController : ControllerBase
    {
        private readonly SqlConnectionStringBuilder sqlConnectionStringBuilder;

        public BlogDapperController(SqlConnectionStringBuilder sqlConnectionStringBuilder)
        {
            sqlConnectionStringBuilder = new SqlConnectionStringBuilder
            {
                DataSource = ".",
                InitialCatalog = "NNCDotNetCore",
                UserID = "sa",
                Password = "sa@123"
            };
        }
        [HttpGet]
        public IActionResult GetBlogs()
        {
            String query = "select * from Tbl_Blog";
            using IDbConnection db = new SqlConnection(sqlConnectionStringBuilder.ConnectionString);
            List<BlogDataModel> lst = db.Query<BlogDataModel>(query).ToList();
            BlogListResponseModel model = new BlogListResponseModel()
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
            String query = "select * from Tbl_Blog where Blog_Id = @Blog_Id";
            using IDbConnection db = new SqlConnection(sqlConnectionStringBuilder.ConnectionString);
            BlogDataModel? item = db.Query<BlogDataModel>(query, new BlogDataModel {Blog_Id = id}).FirstOrDefault();
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
            String query = $@"INSERT INTO [dbo].[Tbl_Blog]
                  ([Blog_Title]
                   ,[Blog_Author]
                    ,[Blog_Content])
             VALUES
                  (@Blog_Title
                  ,@Blog_Author
                  ,@Blog_Content)";

            using IDbConnection db = new SqlConnection(sqlConnectionStringBuilder.ConnectionString);
            int result = db.Execute(query, blog);

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
            String query = "select * from Tbl_Blog where Blog_Id = @Blog_Id";
            using IDbConnection db = new SqlConnection(sqlConnectionStringBuilder.ConnectionString);
            BlogDataModel? item = db.Query<BlogDataModel>(query, new BlogDataModel { Blog_Id = id }).FirstOrDefault();
            if (item is null)
            {
                var response = new { IsSuccess = false, Message = "No data found." };
                return NotFound(response);
            }

            query = $@"INSERT INTO [dbo].[Tbl_Blog]
                  ([Blog_Title]
                   ,[Blog_Author]
                    ,[Blog_Content])
             VALUES
                  (@Blog_Title
                  ,@Blog_Author
                  ,@Blog_Content)";

            using IDbConnection db2 = new SqlConnection(sqlConnectionStringBuilder.ConnectionString);

            int result = db2.Execute(query, blog);

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
            string query = "select * from tbl_blog where Blog_Id = @Blog_Id";
            using IDbConnection db = new SqlConnection(sqlConnectionStringBuilder.ConnectionString);
            BlogDataModel? item = db.Query<BlogDataModel>(query, new BlogDataModel { Blog_Id = id }).FirstOrDefault();
            if (item is null)
            {
                var response = new { IsSuccess = false, Message = "No data found." };
                return NotFound(response);
            }

            string conditions = ""; //0

            if (!string.IsNullOrEmpty(blog.Blog_Title))
            {
                conditions += " [Blog_Title] = @Blog_Title, ";
                item.Blog_Title = blog.Blog_Title;
            }
            if (!string.IsNullOrEmpty(blog.Blog_Author))
            {
                conditions += " [Blog_Author] = @Blog_Author, ";
                item.Blog_Author = blog.Blog_Author;
            }
            if (!string.IsNullOrEmpty(blog.Blog_Content))
            {
                conditions += " [Blog_Content] = @Blog_Content, ";
                item.Blog_Content = blog.Blog_Content;
            }
            if (conditions.Length == 0)
            {
                var response = new { IsSuccess = false, Message = "No data to update." };
                return NotFound(response);
            }

            conditions = conditions.Substring(0, conditions.Length - 2);

            query = $@"UPDATE [dbo].[Tbl_Blog]
                            SET {conditions}
                            WHERE Blog_Id = @Blog_Id";

            using IDbConnection db2 = new SqlConnection(sqlConnectionStringBuilder.ConnectionString);
            int result = db2.Execute(query, blog);

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
            String query = "select * from Tbl_Blog where Blog_Id = @Blog_Id";
            using IDbConnection db = new SqlConnection(sqlConnectionStringBuilder.ConnectionString);
            BlogDataModel? item = db.Query<BlogDataModel>(query, new BlogDataModel { Blog_Id = id }).FirstOrDefault();
            if (item is null)
            {
                var response = new { IsSuccess = false, Message = "No data found." };
                return NotFound(response);
            }

            query = @"DELETE FROM [dbo].[Tbl_Blog]
                        WHERE Blog_Id =@Blog_Id;";
            using IDbConnection db2 = new SqlConnection(sqlConnectionStringBuilder.ConnectionString);
            int result = db2.Execute(query, new BlogDataModel { Blog_Id=id});

            BlogResponseModel model = new BlogResponseModel()
            {
                IsSuccess = result > 0,
                Message = result > 0 ? "Deleting Successful." : "Deleting Failed.",
            };
            return Ok(model);
        }
    }
}
