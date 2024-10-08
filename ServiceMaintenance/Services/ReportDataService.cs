 using Microsoft.AspNetCore.Components;
using ServiceMaintenance.Models;
using System.Net.Http;

namespace ServiceMaintenance.Services
{
    public class ReportDataService : IReportDataService
    {
        private readonly HttpClient httpClient;
        public ReportDataService(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }
        public async Task<ServiceReportData> CreateReport(ServiceReportData newReport)
        {
            try
            {
                var response = await httpClient.PostAsJsonAsync("api/ReportData", newReport);
                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadFromJsonAsync<ServiceReportData>();
                }
                else
                {
                    var responseContent = await response.Content.ReadAsStringAsync();
                    Console.WriteLine($"Bad Request: {responseContent}");
                    throw new HttpRequestException($"Bad Request: {responseContent}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception in CreateEmployee: {ex.Message}");
                throw;
            }
        }
        public async Task<byte[]> GenerateReportAsync(int id)
        {
            var response = await httpClient.GetAsync($"api/reports/generate-report?id={id}");
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadAsByteArrayAsync();
            }
            else
            {
                throw new HttpRequestException($"Error fetching report: {response.ReasonPhrase}");
            }
        }
        public async Task DeleteReport(int id)
        {
            await httpClient.DeleteAsync($"api/ReportData/{id}");
        }

        public async Task<IEnumerable<ServiceReportData>> GetReport()
        {
            return await httpClient.GetJsonAsync<ServiceReportData[]>("api/ReportData");
        }

        public async Task<ServiceReportData> GetReport(int id)
        {
            return await httpClient.GetJsonAsync<ServiceReportData>($"api/ReportData/{id}");
        }

        public async Task<ServiceReportData> UpdateReport(ServiceReportData updatedReport)
        {
            var response = await httpClient.PutAsJsonAsync("api/ReportData", updatedReport);
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<ServiceReportData>();
            }
            else
            {
                var responseContent = await response.Content.ReadAsStringAsync();
                Console.WriteLine($"Bad Request: {responseContent}");
                throw new HttpRequestException($"Bad Request: {responseContent}");
            }
        }
    }
}
