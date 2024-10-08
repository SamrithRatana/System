using Microsoft.EntityFrameworkCore;
using ServiceMaintenance.Models;
using System;

namespace ServiceMaintenance.Api.Models
{
    public class CustomerServiceRespository : ICustomerServiceRespository
    {
        private readonly ServiceMantenanceContext _appDbContext;

        public CustomerServiceRespository(ServiceMantenanceContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<Customer> AddReport(Customer report)
        {
            var result = await _appDbContext.Customers.AddAsync(report);
            await _appDbContext.SaveChangesAsync();
            return result.Entity;
        }

        public async Task<Customer> DeleteReport(int ID)
        {
            var result = await _appDbContext.Customers
                          .FirstOrDefaultAsync(e => e.ID == ID);
            if (result != null)
            {
                _appDbContext.Customers.Remove(result);
                await _appDbContext.SaveChangesAsync();
                return result;
            }
            return null;
        }

        public async Task<IEnumerable<Customer>> GetReport()
        {
            return await _appDbContext.Customers.ToListAsync();
        }

        public async Task<Customer> GetReport(int ID)
        {
            return await _appDbContext.Customers
                        //.Include(e => e.Department)
                        .FirstOrDefaultAsync(e => e.ID == ID);
        }

        public async Task<Customer> UpdateReport(Customer report)
        {
            var customer = await _appDbContext.Customers
    .FirstOrDefaultAsync(e => e.ID == report.ID); // Adjust 'report.ID' if necessary

            if (customer != null)
            {
                // Update properties with appropriate type conversions if needed
                customer.ListId = report.ListId; // Ensure this property is of type 'string'
                customer.CreatedBy = report.CreatedBy; // Ensure this property is of type 'string'
                customer.CreatedAt = report.CreatedAt; // Ensure this property is of type 'DateTime'
                customer.ModifyBy = report.ModifyBy; // Ensure this property is of type 'string'
                customer.ModifiedAt = report.ModifiedAt; // Ensure this property is of type 'DateTime'
                customer.CompanyName = report.CompanyName; // Ensure this property is of type 'string'
                customer.Address = report.Address; // Ensure this property is of type 'string'
                customer.ContactName = report.ContactName; // Ensure this property is of type 'string'
                customer.Phone = report.Phone; // Ensure this property is of type 'string'
                customer.Email = report.Email; // Ensure this property is of type 'string'
                customer.CustomerTypeListId = report.CustomerTypeListId; // Ensure this property is of type 'string'
                customer.VATTIN = report.VATTIN; // Ensure this property is of type 'string'
                customer.BankAccountListId = report.BankAccountListId; // Ensure this property is of type 'string'
                customer.InvoiceType = report.InvoiceType; // Handle type conversion from byte if necessary
                customer.SalesRepListId = report.SalesRepListId; // Ensure this property is of type 'string'
                customer.TermListId = report.TermListId; // Ensure this property is of type 'string'
                customer.IsActive = report.IsActive; // Ensure this property is of type 'bool'

                await _appDbContext.SaveChangesAsync();

                return customer;
            }

            return null;
        }
    }
}
