using Microsoft.EntityFrameworkCore;
using ServiceMaintenance.Models;
using System;
using System.Reflection;

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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Customer>()
        .HasKey(c => c.ID); // Specify the primary key
            modelBuilder.Entity<ServiceReportData>()
       .HasKey(c => c.ID);

            // Seed data
            /*modelBuilder.Entity<ServiceReportData>().HasData(
                new ServiceReportData
                {
                    ID = 1,
                    Code = "Code1",
                    ReportID = DateTime.Now,
                    CompanyName = "Company A",
                    Attention = "Attention A",
                    Atten = "Atten A",
                    Address = "Address A",
                    Mobile = "1234567890",
                    OfficeTel = "0987654321",
                    ProductName = "Product A",
                    Instrument = "Instrument A",
                    SerialNumber = "SN123",
                    BranchSerial = "BS123",
                    CustomerReq = "Requirement A",
                    ActionTaken = "Action A",
                    Solution = "Solution A",
                    Onsite = true,
                    CompanyService = true,
                    ServiceContract = false,
                    Datestart = DateTime.Now.AddDays(-2),
                    DateFinish = DateTime.Now.AddDays(-1),
                    Engineer = "Engineer A",
                    Verify = "Verified",
                    Customer = "Customer A"
                },
                new ServiceReportData
                {
                    ID = 2,
                    Code = "Code2",
                    ReportID = DateTime.Now,
                    CompanyName = "Company B",
                    Attention = "Attention B",
                    Atten = "Atten B",
                    Address = "Address B",
                    Mobile = "1234567891",
                    OfficeTel = "0987654322",
                    ProductName = "Product B",
                    Instrument = "Instrument B",
                    SerialNumber = "SN124",
                    BranchSerial = "BS124",
                    CustomerReq = "Requirement B",
                    ActionTaken = "Action B",
                    Solution = "Solution B",
                    Onsite = false,
                    CompanyService = false,
                    ServiceContract = true,
                    Datestart = DateTime.Now.AddDays(-3),
                    DateFinish = DateTime.Now.AddDays(-2),
                    Engineer = "Engineer B",
                    Verify = "Verified",
                    Customer = "Customer B"
                }
            );*/
        }
    }
}

