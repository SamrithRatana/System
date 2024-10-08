
using Microsoft.EntityFrameworkCore;
using ServiceMaintenance.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ServiceMaintenance.Api.Models
{
    public class ServiceReportDataRespository : IServiceReportDataRespository
    {
        private readonly ServiceMantenanceContext appDbContext;
        public ServiceReportDataRespository(ServiceMantenanceContext appDbContext)
        {
            this.appDbContext = appDbContext;
        }
        public async Task<ServiceReportData> AddReport(ServiceReportData report)
        {
            var result = await appDbContext.ReportDatas.AddAsync(report);
            await appDbContext.SaveChangesAsync();
            return result.Entity;
        }

        public async Task<ServiceReportData> DeleteReport(int ID)
        {
            var result = await appDbContext.ReportDatas
               .FirstOrDefaultAsync(e => e.ID == ID);
            if (result != null)
            {
                appDbContext.ReportDatas.Remove(result);
                await appDbContext.SaveChangesAsync();
                return result;
            }
            return null;
        }

        public async Task<IEnumerable<ServiceReportData>> GetReport()
        {
            return await appDbContext.ReportDatas.ToListAsync();
        }

        public async Task<ServiceReportData> GetReport(int ID)
        {
            return await appDbContext.ReportDatas
             //.Include(e => e.Department)
             .FirstOrDefaultAsync(e => e.ID == ID);
        }
        public async Task<ServiceReportData> UpdateReport(ServiceReportData report)
        {
            var result = await appDbContext.ReportDatas
               .FirstOrDefaultAsync(e => e.ID == report.ID);

            if (result != null)
            {
                result.ReportID = report.ReportID;
                result.CompanyName = report.CompanyName;
                result.Attention = report.Attention;
                result.Atten = report.Atten;
                result.Address = report.Address;
                result.Mobile = report.Mobile;
                result.OfficeTel = report.OfficeTel;
                result.ProductName = report.ProductName;
                result.Instrument = report.Instrument;
                result.SerialNumber = report.SerialNumber;
                result.BranchSerial = report.BranchSerial;
                result.CustomerReq = report.CustomerReq;
                result.ActionTaken = report.ActionTaken;
                result.Solution = report.Solution;
                result.Onsite = report.Onsite;
                result.CompanyService = report.CompanyService;
                result.ServiceContract = report.ServiceContract;
                result.Datestart = report.Datestart;
                result.DateFinish = report.DateFinish;
                result.Engineer = report.Engineer;
                result.Verify = report.Verify;
                result.Customer = report.Customer;
                await appDbContext.SaveChangesAsync();

                return result;
            }

            return null;
        }
    }
}
