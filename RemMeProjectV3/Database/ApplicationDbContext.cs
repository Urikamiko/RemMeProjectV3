using Microsoft.EntityFrameworkCore;
using RemMeProjectV3.Database.Model;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RemMeProjectV3.Database
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Note> Notes { get; set; }
        public DbSet<Key> Keys { get; set; }
        public DbSet<KeyNote> KeyNotes { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string useConnection = ConfigurationManager.AppSettings["UseConnection"];
            string connectionString = ConfigurationManager.ConnectionStrings[useConnection].ConnectionString;
            optionsBuilder.UseSqlServer(connectionString);
        }
    }
}
