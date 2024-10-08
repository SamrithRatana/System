// ProjectService.cs

using ServiceMaintenance.Chat;

public class ProjectService : IProjectService
{
    public async Task<List<Project>> GetProjectsAsync()
    {
        // Simulate an API call
        await Task.Delay(100); // Simulate a delay for async operation

        return new List<Project>
        {
            new Project
            {
                ProjectName = "Material XD Version",
                CompanyName = "xd",
                Logo = "../assets/img/small-logos/logo-xd.svg",
                Members = new List<Member>
                {
                    new Member { Name = "Ryan Tompson", Photo = "../assets/img/team-1.jpg" },
                    new Member { Name = "Romina Hadid", Photo = "../assets/img/team-2.jpg" },
                    new Member { Name = "Alexander Smith", Photo = "../assets/img/team-3.jpg" },
                    new Member { Name = "Jessica Doe", Photo = "../assets/img/team-4.jpg" }
                },
                Budget = "14,000",
                Completion = 60
            },
            new Project
            {
                ProjectName = "Add Progress Track",
                CompanyName = "atlassian",
                Logo = "../assets/img/small-logos/logo-atlassian.svg",
                Members = new List<Member>
                {
                    new Member { Name = "Romina Hadid", Photo = "../assets/img/team-2.jpg" },
                    new Member { Name = "Jessica Doe", Photo = "../assets/img/team-4.jpg" }
                },
                Budget = "3,000",
                Completion = 10
            }
            // Add more projects as needed
        };
    }

    public async Task<List<SummaryCard>> GetSummaryCardsAsync()
    {
        // Simulate an API call
        await Task.Delay(100); // Simulate a delay for async operation

        return new List<SummaryCard>
        {
            new SummaryCard { Title = "Today's Money", Value = "$53k", PercentageChange = "+55%", Icon = "weekend", IconClass = "bg-gradient-dark shadow-dark" },
            new SummaryCard { Title = "Today's Users", Value = "2,300", PercentageChange = "+3%", Icon = "person", IconClass = "bg-gradient-primary shadow-primary" },
            new SummaryCard { Title = "New Clients", Value = "3,462", PercentageChange = "-2%", Icon = "person", IconClass = "bg-gradient-success shadow-success" },
            new SummaryCard { Title = "Sales", Value = "$103,430", PercentageChange = "+5%", Icon = "weekend", IconClass = "bg-gradient-info shadow-info" }
        };
    }
}
