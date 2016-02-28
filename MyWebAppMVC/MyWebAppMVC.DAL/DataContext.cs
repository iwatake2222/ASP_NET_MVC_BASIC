using MyWebAppMVC.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyWebAppMVC.DAL
{
    public class DataContext : DbContext
    {
        public DataContext() : base("DefaultConnection") { }
        public DbSet<Product> Products { get; set; }
        public DbSet<Order> Orders { get; set; }
    }
}
