using Microsoft.EntityFrameworkCore;
using System;
using Microsoft.EntityFrameworkCore.Sqlite;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Models
{
    public class ContextDatabase : DbContext
    {
        private readonly string _databaseName;

        public DbSet<Operation> operations { get; set; }
        public DbSet<Record> records { get; set; }


        public ContextDatabase(string path): base() { 
        
           _databaseName = path;

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            
            optionsBuilder.UseSqlite($"Data Source={_databaseName}");
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<Operation>().HasKey(o => o.Id);
            modelBuilder.Entity<Operation>().Property(o => o.Amount).HasDefaultValue(0);
            // modelBuilder.Entity<Operation>().HasCheckConstraint("amount_check_constraint", "[Amount] > 0");

            modelBuilder.Entity<Record>().HasKey(o => o.Id);
            
            base.OnModelCreating(modelBuilder);
        }
    }
}
