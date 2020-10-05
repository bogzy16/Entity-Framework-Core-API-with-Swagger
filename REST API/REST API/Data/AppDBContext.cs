using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using REST_API.Models;

namespace REST_API.Data
{
    public class AppDBContext: DbContext
    {
        public AppDBContext(DbContextOptions<AppDBContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new DBTableConfiguration());

            base.OnModelCreating(builder);
        }

        public DbSet<EmployeeRecord> EmployeeRecords { get; set; }

    }
}
