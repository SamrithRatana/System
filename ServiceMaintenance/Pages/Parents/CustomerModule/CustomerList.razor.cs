using DevExpress.Blazor;
using Microsoft.AspNetCore.Components;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using ServiceMaintenance.Models;
using ServiceMaintenance.Services;

namespace ServiceMaintenance.Pages.Parents.CustomerModule
{
    public partial class CustomerList
    {


        bool isXSmallScreen;

        void ShowColumnChooser()
        {
            MyGrid.ShowColumnChooser(new DialogDisplayOptions($".flexGrid", HorizontalAlignment.Center, VerticalAlignment.Center));
        }

        bool GetExtraColumnsVisible() => !isXSmallScreen;

        bool UnVisible() => false;


        [Inject]
        public ICustomerService customerService { get; set; }

        public IEnumerable<Customer> customers { get; set; }
        [Inject]
        public NavigationManager NavigationManager { get; set; }
        IGrid MyGrid { get; set; }
        const string ExportFileName = "ExportResult";
        bool EditItemsEnabled { get; set; }
        int FocusedRowVisibleIndex { get; set; }

        bool indicatorVisible = false;
        bool indicatorAreaVisible = true;
        int SelectedReportId { get; set; } // Add this property
        private Customer selectedReport;



        protected override async Task OnInitializedAsync()
        {
            try
            {

                indicatorVisible = true;
                StateHasChanged();
                customers = await customerService.GetReport();

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
            var editModel = (Customer)e.EditModel;

            try
            {
                if (e.IsNew)
                {
                    await customerService.CreateReport(editModel);
                    customers = await customerService.GetReport();
                    UpdateEditItemsEnabled(true);
                    AddToast();
                }
                else
                {
                    await customerService.UpdateReport(editModel);
                    customers = await customerService.GetReport();
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
                await customerService.DeleteReport(report.ID);
                customers = await customerService.GetReport();
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
            UpdateEditItemsEnabled(true);
            selectedReport = (Customer)args.DataItem;
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

        private void PrintReport_Click()
        {
            // Data to be passed can be appended as query parameters
            //string reportId = "123"; // Example data
            NavigationManager.NavigateTo("/delivery");
        }
        private void ShowCreateProductModal()
        {
           
        }

        private void ShowEditProductModal()
        {
            // Logic to show the edit product modal
        }

        private void ShowDeleteProductModal()
        {

        }

        private void ViewProducts()
        {
            // Logic to navigate to the view products page
            
        }
    }
}
