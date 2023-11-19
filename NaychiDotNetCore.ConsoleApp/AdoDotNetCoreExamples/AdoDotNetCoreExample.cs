using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace NaychiDotNetCore.ConsoleApp.AdoDotNetCoreExamples
{
    public class AdoDotNetCoreExample
    {
        // private readonly SqlConnectionStringBuilder connectionStringBuilder = new SqlConnectionStringBuilder()
        // {
        //     DataSource = ".",
        //    InitialCatalog = "NNCDotNetCore",
        //     UserID = "sa",
        //     Password = "sa@123"
        // };
        private readonly SqlConnectionStringBuilder sqlConnectionStringBuilder;

        public AdoDotNetCoreExample()
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
            Create("Test1", "NCC", "Hello");
            Edit(6);
            Update(5, "Test8.5", "Test8.5", "Test.7");
            Delete(3);
        }
        private void Read()
        {
            #region read/retrieve


            SqlConnection connection = new SqlConnection(sqlConnectionStringBuilder.ConnectionString);
            connection.Open();
            Console.WriteLine("Opening...");

            String query = "select * from Tbl_Blog";
            SqlCommand cmd = new SqlCommand(query, connection);
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            adapter.Fill(dt);
            Console.WriteLine("Opened");

            connection.Close();
            foreach (DataRow row in dt.Rows)
            {
                Console.WriteLine(row["Blog_Id"]);
                Console.WriteLine(row["Blog_Title"]);
                Console.WriteLine(row["Blog_Author"]);
                Console.WriteLine(row["Blog_Content"]);

            }
            #endregion
        }

        private void Edit(int id)
        {
            #region Edit


            SqlConnection connection = new SqlConnection(sqlConnectionStringBuilder.ConnectionString);
            connection.Open();

            String query = "select * from Tbl_Blog where Blog_Id=@Blog_Id;";
            SqlCommand cmd = new SqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@Blog_Id", id);
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();

            adapter.Fill(dt);

            connection.Close();

            if (dt.Rows.Count == 0)
            {
                Console.WriteLine("Data not found!");
                return;
            }

            DataRow row = dt.Rows[0];

            Console.WriteLine(row["Blog_Id"]);
            Console.WriteLine(row["Blog_Title"]);
            Console.WriteLine(row["Blog_Author"]);
            Console.WriteLine(row["Blog_Content"]);

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
