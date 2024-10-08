using Microsoft.AspNetCore.Components;
using ServiceMaintenance.Models;
using ServiceMaintenance.Services;

namespace ServiceMaintenance.Pages.Details
{
    public partial class ServiceReportDetail
    {
        public ServiceReportData reportDatas { get; set; } = new ServiceReportData();
        [Inject]
        public IReportDataService ReportDataService { get; set; }

        [Parameter]
        public string Id { get; set; }

        protected async override Task OnInitializedAsync()
        {
            Id = Id ?? "1";
            reportDatas = await ReportDataService.GetReport(int.Parse(Id));
        }
    }
}
