using DevExpress.Blazor;
using DevExpress.XtraReports.UI;
using Microsoft.AspNetCore.Components;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.JSInterop;
using ServiceMaintenance.Models;
using ServiceMaintenance.ObjectModel;
using ServiceMaintenance.Services;

namespace ServiceMaintenance.Pages
{
    public partial class TechnicalServiceReport
    {
        private bool isLoading = true; // Initially set to true to show loading indicator

        bool isXSmallScreen;
      
        void ShowColumnChooser()
        {
            MyGrid.ShowColumnChooser(new DialogDisplayOptions($".flexGrid", HorizontalAlignment.Center, VerticalAlignment.Center));
        }

        bool GetExtraColumnsVisible() => !isXSmallScreen;
       
        bool UnVisible() => false;
     

        [Inject]
        public IReportDataService reportDataService { get; set; }
       
        public IEnumerable<ServiceReportData> reportDatas { get; set; }
        [Inject]
        public NavigationManager NavigationManager { get; set; }
        IGrid MyGrid { get; set; }
        const string ExportFileName = "ExportResult";
        bool EditItemsEnabled { get; set; }
        int FocusedRowVisibleIndex { get; set; }

        bool indicatorVisible = false;
        bool indicatorAreaVisible = true;
        int SelectedReportId { get; set; } // Add this property
        private ServiceReportData SelectedReportData { get; set; }




        protected override async Task OnInitializedAsync()
        {
            try
            {
                
                indicatorVisible = true;
                StateHasChanged();
                reportDatas = await reportDataService.GetReport();
                
                // Hide loading panel after data is loaded
                indicatorVisible = false;
                StateHasChanged();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading reports: {ex.Message}");
                ErrorToast(ex.Message);
            }
        }
       
        [Inject] IToastNotificationService ToastService { get; set; }

        ToastAnimationType Animation { get; set; } = ToastAnimationType.Slide;
        async Task OnEditModelSaving(GridEditModelSavingEventArgs e)
        {
            var editModel = (ServiceReportData)e.EditModel;

            try
            {
                if (e.IsNew)
                {
                    await reportDataService.CreateReport(editModel);
                    reportDatas = await reportDataService.GetReport();
                    UpdateEditItemsEnabled(true);
                    AddToast();
                }
                else
                {
                    await reportDataService.UpdateReport(editModel);
                    reportDatas = await reportDataService.GetReport();
                    UpdateEditItemsEnabled(true);
                    AddToast();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error saving report: {ex.Message}");
                ErrorToast(ex.Message);
            } 

        }
        
        async Task DeleteReportItem(GridDataItemDeletingEventArgs e)
        {
            var report = (ServiceReportData)e.DataItem;
            try
            {
                await reportDataService.DeleteReport(report.ID);
                reportDatas = await reportDataService.GetReport();
                UpdateEditItemsEnabled(true);
                AddToast();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error deleting report: {ex.Message}");
                ErrorToast(ex.Message);
            }
        }
        
        private void AddToast()
        {
            ToastService.ShowToast(new ToastOptions
            {
                ProviderName = "Customization",
                Title = "Process was successful",
                Text = "Remember the system is processing with data",
                ThemeMode = ToastThemeMode.Pastel,
                RenderStyle = ToastRenderStyle.Success

            });
        }
        private void ErrorToast(string errorMessage)
        {
            ToastService.ShowToast(new ToastOptions
            {
                ProviderName = "Error",
                Title = "An error occurred",
                Text = errorMessage, // Use the provided error message
                ThemeMode = ToastThemeMode.Light,
                RenderStyle = ToastRenderStyle.Danger
            });
        }
        void Grid_FocusedRowChanged(GridFocusedRowChangedEventArgs args)
        {
            FocusedRowVisibleIndex = args.VisibleIndex;
            SelectedReportData = (ServiceReportData)args.DataItem; // Assuming DataItem is the data type
            UpdateEditItemsEnabled(true);
            

        }
        void UpdateEditItemsEnabled(bool enabled)
        {
            EditItemsEnabled = enabled;
        }

        async Task NewItem_Click()
        {
            await MyGrid.StartEditNewRowAsync();
        }
        async Task EditItem_Click()
        {
            await MyGrid.StartEditRowAsync(FocusedRowVisibleIndex);
        }

        void DeleteItem_Click()
        {

            MyGrid.ShowRowDeleteConfirmation(FocusedRowVisibleIndex);

        }
        void ColumnChooserItem_Click(ToolbarItemClickEventArgs e)
        {
            MyGrid.ShowColumnChooser();
        }
        async Task ExportXlsxItem_Click()
        {
            await MyGrid.ExportToXlsxAsync(ExportFileName);
        }
        async Task ExportXlsItem_Click()
        {
            await MyGrid.ExportToXlsAsync(ExportFileName);
        }
        async Task ExportCsvItem_Click()
        {
            await MyGrid.ExportToCsvAsync(ExportFileName);
        }

        void PrintReport_Click()
        {
            // Ensure a row is selected
            if (SelectedReportData != null)
            {
                // Retrieve the selected ID
                int selectedId = SelectedReportData.ID; // Adjust based on your data model

                // Navigate to the report page with the selected ID
                NavigationManager.NavigateTo($"/report?id={selectedId}");
            }
            else
            {
                // Handle the case where no row is selected
                ErrorToast("Please select a row to print the report.");
            }
        }



    }

}