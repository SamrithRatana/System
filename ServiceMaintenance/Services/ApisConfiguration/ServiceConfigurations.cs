using Microsoft.AspNetCore.Http.Features;

namespace ServiceMaintenance.Services.ApisConfiguration
{
    public static class ServiceConfigurations
    {
        public static void ConfigureHttpClients(IServiceCollection services, string baseAddress)
        {
            services.AddHttpClient<IReportDataService, ReportDataService>(client =>
            {
                client.BaseAddress = new Uri(baseAddress);
            });
            services.AddHttpClient<ICustomerService, CustomerService>(client =>
            {
                client.BaseAddress = new Uri(baseAddress);
            });
            services.AddHttpClient<ISparePartService, SparePartService>(client =>
            {
                client.BaseAddress = new Uri(baseAddress);
            });
            services.AddHttpClient<IPrinterModelService, PrinterModelService>(client =>
            {
                client.BaseAddress = new Uri(baseAddress);
            });
            services.AddHttpClient<IManufacturerService, ManufacturerService>(client =>
            {
                client.BaseAddress = new Uri(baseAddress);
            });
        }

        public static void ConfigureFormOptions(IServiceCollection services)
        {
            services.Configure<FormOptions>(options =>
            {
                options.MultipartBodyLengthLimit = long.MaxValue; // Adjust as necessary
            });
        }
    }
}
