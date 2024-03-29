﻿using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using NayChiChoDotNetCore.ConsoleApp.Models;

namespace NayChiChoDotNetCore.ConsoleApp.DapperExamples
{
    public class DapperExample
    {
        
        
            // private readonly SqlConnectionStringBuilder connectionStringBuilder = new SqlConnectionStringBuilder()
            // {
            //     DataSource = ".",
            //    InitialCatalog = "NNCDotNetCore",
            //     UserID = "sa",
            //     Password = "sa@123"
            // };
            private readonly SqlConnectionStringBuilder sqlConnectionStringBuilder;

            public DapperExample()
            {
                sqlConnectionStringBuilder = new SqlConnectionStringBuilder
                {
                    DataSource = ".",
                    InitialCatalog = "NNCDotNetCore",
                    UserID = "sa",
                    Password = "sa@123"
                };
            }

            public void Run()

            {
               Read();
               Create("Test101", "Nay", "Hi");
               Edit(6);
               Edit(30);
               Update(5, "Test6.5", "Test6.5", "Test.7");
               Delete(3);
            }
            private void Read()
            {
            #region read/retrieve

            String query = "select * from Tbl_Blog";
            using IDbConnection db = new SqlConnection(sqlConnectionStringBuilder.ConnectionString);
            //List<dynamic> lst = db.Query(query).ToList();
            List<BlogDataModel> lst = db.Query<BlogDataModel>(query).ToList();

            foreach (var item in lst)
            {
                Console.WriteLine(item.Blog_Id);
                Console.WriteLine(item.Blog_Title);
                Console.WriteLine(item.Blog_Author);
                Console.WriteLine(item.Blog_Content);

            }
            #endregion
        }

            private void Edit(int id)
            {
            #region Edit

            BlogDataModel blog = new BlogDataModel
            {
                Blog_Id = id
            };
            String query = "select * from Tbl_Blog where Blog_Id = @Blog_Id";
            using IDbConnection db = new SqlConnection(sqlConnectionStringBuilder.ConnectionString);
            //List<dynamic> lst = db.Query(query).ToList();
            //List<BlogDataModel> lst = db.Query<BlogDataModel>(query,new {Blog_Id = id}).ToList();
            // List<BlogDataModel> lst = db.Query<BlogDataModel>(query,blog).ToList();
            BlogDataModel? item = db.Query<BlogDataModel>(query, blog).FirstOrDefault();

            //if(item is null)
            if(item == null)
            {
                Console.WriteLine("No data found.");
                return;
            }
            
            
                Console.WriteLine(item.Blog_Id);
                Console.WriteLine(item.Blog_Title);
                Console.WriteLine(item.Blog_Author);
                Console.WriteLine(item.Blog_Content);
            
            #endregion

        }

            private void Create(string title, string author, string content)
        {
            #region Create

            BlogDataModel blog = new BlogDataModel { 

                Blog_Title = title,
                Blog_Author = author,
                Blog_Content = content
            };

            String query = $@"INSERT INTO [dbo].[Tbl_Blog]
                  ([Blog_Title]
                   ,[Blog_Author]
                    ,[Blog_Content])
             VALUES
                  (@Blog_Title
                  ,@Blog_Author
                  ,@Blog_Content)";

            using IDbConnection db = new SqlConnection(sqlConnectionStringBuilder.ConnectionString);
        
                int result = db.Execute(query,blog);

                String message = result > 0 ? "Saving Successful." : "Saving Failed.";
                Console.WriteLine(message);
                #endregion
            }

            private void Update(int id, string title, string author, string content)
            {
            #region Update

            BlogDataModel blog = new BlogDataModel
            {
                Blog_Id = id,
                Blog_Title = title,
                Blog_Author = author,
                Blog_Content = content
            };

            String query = @"UPDATE [dbo].[Tbl_Blog]
            SET [Blog_Title] = @Blog_Title
                ,[Blog_Author] =@Blog_Author
                ,[Blog_Content] = @Blog_Content
                WHERE Blog_Id =@Blog_Id;";

            using IDbConnection db = new SqlConnection(sqlConnectionStringBuilder.ConnectionString);

                int result = db.Execute(query,blog);   
                String message = result > 0 ? "Updating Successful." : "Updating Failed.";
                Console.WriteLine(message);
                #endregion
            }

            private void Delete(int id)
            {
            #region Delete

            BlogDataModel blog = new BlogDataModel
            {
                Blog_Id = id
            };
            String query = @"DELETE FROM [dbo].[Tbl_Blog]
                        WHERE Blog_Id =@Blog_Id;";

            using IDbConnection db = new SqlConnection(sqlConnectionStringBuilder.ConnectionString);

                int result = db.Execute(query,blog);

                String message = result > 0 ? "Deleting Successful." : "Deleting Failed.";
                Console.WriteLine(message);
                #endregion
            }
    }
}

