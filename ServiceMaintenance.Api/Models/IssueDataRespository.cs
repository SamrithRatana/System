using Microsoft.EntityFrameworkCore;
using ServiceMaintenance.Models;

namespace ServiceMaintenance.Api.Models
{
    public class IssueDataRespository : IIssueDataRespository
    {
        private readonly ServiceMantenanceContext _appDbContext;

        public IssueDataRespository(ServiceMantenanceContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<Issue> AddIssue(Issue report)
        {
            var result = await _appDbContext.Issues.AddAsync(report);
            await _appDbContext.SaveChangesAsync();
            return result.Entity;
        }

        public async Task<Issue> DeleteIssue(int ID)
        {
            var result = await _appDbContext.Issues
                          .FirstOrDefaultAsync(e => e.IssueID == ID);
            if (result != null)
            {
                _appDbContext.Issues.Remove(result);
                await _appDbContext.SaveChangesAsync();
                return result;
            }
            return null;
        }

        public async Task<IEnumerable<Issue>> GetIssue()
        {
            return await _appDbContext.Issues.ToListAsync();
        }

        public async Task<Issue> GetIssue(int ID)
        {
            return await _appDbContext.Issues
                       //.Include(e => e.Department)
                       .FirstOrDefaultAsync(e => e.IssueID == ID);
        }

        public async Task<Issue> UpdateIssue(Issue report)
        {
            var Issue = await _appDbContext.Issues
   .FirstOrDefaultAsync(e => e.IssueID == report.IssueID); // Adjust 'report.ID' if necessary

            if (Issue != null)
            {
                // Update properties with appropriate type conversions if needed
                Issue.CustomerId = report.CustomerId; // Ensure this property is of type 'string'
                Issue.ItemId = report.ItemId; // Ensure this property is of type 'string'
                Issue.IssueType = report.IssueType; // Ensure this property is of type 'DateTime'
                Issue.Date = report.Date; // Ensure this property is of type 'string'
                Issue.SolveDate = report.SolveDate; // Ensure this property is of type 'DateTime'
                Issue.Description = report.Description; // Ensure this property is of type 'string'
                Issue.Status = report.Status; // Ensure this property is of type 'string'
               

                await _appDbContext.SaveChangesAsync();

                return Issue;
            }

            return null;
        }
    }
}
