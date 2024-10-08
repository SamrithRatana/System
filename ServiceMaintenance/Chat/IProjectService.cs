namespace ServiceMaintenance.Chat
{
    public interface IProjectService
    {
        Task<List<Project>> GetProjectsAsync();
        Task<List<SummaryCard>> GetSummaryCardsAsync();
    }

    // Project.cs
    public class Project
    {
        public string ProjectName { get; set; }
        public string CompanyName { get; set; }
        public string Logo { get; set; }
        public List<Member> Members { get; set; }
        public string Budget { get; set; }
        public int Completion { get; set; }
    }

    // Member.cs
    public class Member
    {
        public string Name { get; set; }
        public string Photo { get; set; }
    }

    // SummaryCard.cs
    public class SummaryCard
    {
        public string Title { get; set; }
        public string Value { get; set; }
        public string PercentageChange { get; set; }
        public string Icon { get; set; }
        public string IconClass { get; set; }
    }
}
