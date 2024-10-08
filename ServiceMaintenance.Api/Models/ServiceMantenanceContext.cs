using Microsoft.EntityFrameworkCore;
using ServiceMaintenance.Models;
using System;

namespace ServiceMaintenance.Api.Models
{
    public class ServiceMantenanceContext : DbContext
    {
        public ServiceMantenanceContext(DbContextOptions<ServiceMantenanceContext> options)
            : base(options)
        {
        }

        public DbSet<ServiceReportData> ReportDatas { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<PersonalInfoTable> PersonalInfoTable { get; set; }
        public DbSet<Issue> Issues { get; set; }
        public DbSet<PrinterModel> PrinterModels { get; set; }
        
        public DbSet<SparePart> SpareParts { get; set; }
        public DbSet<Manufacturer> Manufacturers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

         /*   modelBuilder.Entity<Customer>()
                .HasKey(c => c.ID); // Specify the primary key
            modelBuilder.Entity<ServiceReportData>()
                .HasKey(c => c.ID);
            modelBuilder.Entity<Issue>()
                .HasKey(c => c.IssueID);
            modelBuilder.Entity<Item>()
                .HasKey(c => c.ItemId);*/

            modelBuilder.Entity<PrinterModel>()
                .HasKey(pm => pm.PrinterModelID);
/*
            modelBuilder.Entity<Category>()
                .HasKey(c => c.CategoryID);

            modelBuilder.Entity<SparePart>()
                .HasKey(sp => sp.SparePartID);*/
            modelBuilder.Entity<Manufacturer>()
                .HasKey(sp => sp.ManufacturerID);


          
        }
    }
}
