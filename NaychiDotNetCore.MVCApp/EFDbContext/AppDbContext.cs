using Microsoft.EntityFrameworkCore;
using NaychichoDotNetCore.MVCApp.EFDbContext;
using NaychichoDotNetCore.MVCApp.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NaychichoDotNetCore.MVCApp.EFDbContext
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<BlogDataModel> Blogs { get; set; }
    }
}
