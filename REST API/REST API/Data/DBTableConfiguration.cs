using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using REST_API.Models;


namespace REST_API.Data
{
    public class DBTableConfiguration : IEntityTypeConfiguration<EmployeeRecord>
    {
        public void Configure(EntityTypeBuilder<EmployeeRecord> builder)
        {
            builder.ToTable("Employees");

            builder.HasKey(p => p.EmployeeNumber);

            builder.Property(p => p.EmployeeNumber).HasColumnType("int").IsRequired().ValueGeneratedNever();
            builder.Property(p => p.FistName).HasColumnType("nvarchar(max)").IsRequired();
            builder.Property(p => p.LastName).HasColumnType("nvarchar(max)").IsRequired();
            builder.Property(p => p.Temperature).HasColumnType("decimal").IsRequired();
            builder.Property(p => p.isActive).HasColumnType("bit").HasDefaultValue(1);
            builder.Property(p => p.RecordDate).HasColumnType("datetime").HasDefaultValueSql("CONVERT(date, GETDATE())");
            builder.Property(p => p.ModifiedDate).HasColumnType("datetime").HasDefaultValueSql("CONVERT(date, GETDATE())");
        }
    }
}
