using System;
using Domain;
using Microsoft.EntityFrameworkCore;

namespace Persistance
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<Value> Values {get; set;}

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Value>()
                .HasData(
                    new Value{Id = 1, Name="Name Property 1"},
                    new Value{Id = 2, Name="Name Property 2"},
                    new Value{Id = 3, Name="Name Property 3"},
                    new Value{Id = 4, Name="Name Property 4"}

                );
        }
    }
}
