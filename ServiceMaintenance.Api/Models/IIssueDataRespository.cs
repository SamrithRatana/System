using ServiceMaintenance.Models;

namespace ServiceMaintenance.Api.Models
{
    public interface IIssueDataRespository
    {
        Task<IEnumerable<Issue>> GetIssue();
        Task<Issue> GetIssue(int ID);
        Task<Issue> AddIssue(Issue report);
        Task<Issue> UpdateIssue(Issue report);
        Task<Issue> DeleteIssue(int ID);
    }
}
