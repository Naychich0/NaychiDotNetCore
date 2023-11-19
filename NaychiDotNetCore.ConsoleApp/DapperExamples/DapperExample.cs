using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using NaychiDotNetCore.ConsoleApp.Models;

namespace NaychiDotNetCore.ConsoleApp.DapperExamples
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
               //Create("Test1", "NCC", "Hello");
                Edit(6);
                Edit(30);
               //Update(5, "Test8.5", "Test8.5", "Test.7");
               //Delete(3);
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

                SqlConnection connection = new SqlConnection(sqlConnectionStringBuilder.ConnectionString);
                connection.Open();
                //         String query = $@"INSERT INTO [dbo].[Tbl_Blog]
                //      ([Blog_Title]
                //        ,[Blog_Author]
                //        ,[Blog_Content])
                //  VALUES
                //        ('{title}'
                //      ,'{author}'
                //      ,'{content}')";

                String query = @"INSERT INTO [dbo].[Tbl_Blog]
           ([Blog_Title]
           ,[Blog_Author]
           ,[Blog_Content])
     VALUES
           (@Blog_Title
           ,@Blog_Author
           ,@Blog_Content)";
                SqlCommand cmd = new SqlCommand(query, connection);
                //  int result = cmd.ExecuteNonQuery();
                cmd.Parameters.AddWithValue("@Blog_Title", title);
                cmd.Parameters.AddWithValue("@Blog_Author", author);
                cmd.Parameters.AddWithValue("@Blog_Content", content);

                int result = cmd.ExecuteNonQuery();
                connection.Close();

                String message = result > 0 ? "Saving Successful." : "Saving Failed.";
                Console.WriteLine(message);
                #endregion
            }

            private void Update(int id, string title, string author, string content)
            {
                #region Update

                SqlConnection connection = new SqlConnection(sqlConnectionStringBuilder.ConnectionString);
                connection.Open();
                //         String query = $@"INSERT INTO [dbo].[Tbl_Blog]
                //      ([Blog_Title]
                //        ,[Blog_Author]
                //        ,[Blog_Content])
                //  VALUES
                //        ('{title}'
                //      ,'{author}'
                //      ,'{content}')";

                String query = @"UPDATE [dbo].[Tbl_Blog]
   SET [Blog_Title] = @Blog_Title
      ,[Blog_Author] =@Blog_Author
      ,[Blog_Content] = @Blog_Content
 WHERE Blog_Id =@Blog_Id;";
                SqlCommand cmd = new SqlCommand(query, connection);
                //  int result = cmd.ExecuteNonQuery();
                cmd.Parameters.AddWithValue("@Blog_Id", id);
                cmd.Parameters.AddWithValue("@Blog_Title", title);
                cmd.Parameters.AddWithValue("@Blog_Author", author);
                cmd.Parameters.AddWithValue("@Blog_Content", content);

                int result = cmd.ExecuteNonQuery();
                connection.Close();

                String message = result > 0 ? "Updating Successful." : "Updating Failed.";
                Console.WriteLine(message);
                #endregion
            }

            private void Delete(int id)
            {
                #region Delete

                SqlConnection connection = new SqlConnection(sqlConnectionStringBuilder.ConnectionString);
                connection.Open();
                //         String query = $@"INSERT INTO [dbo].[Tbl_Blog]
                //      ([Blog_Title]
                //        ,[Blog_Author]
                //        ,[Blog_Content])
                //  VALUES
                //        ('{title}'
                //      ,'{author}'
                //      ,'{content}')";

                String query = @"DELETE FROM [dbo].[Tbl_Blog]
                        WHERE Blog_Id =@Blog_Id;";
                SqlCommand cmd = new SqlCommand(query, connection);
                //  int result = cmd.ExecuteNonQuery();
                cmd.Parameters.AddWithValue("@Blog_Id", id);


                int result = cmd.ExecuteNonQuery();
                connection.Close();

                String message = result > 0 ? "Deleting Successful." : "Deleting Failed.";
                Console.WriteLine(message);
                #endregion
            }
    }
}

