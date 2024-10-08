using ServiceMaintenance.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading.Tasks;

public class RepairItemService
{
    private readonly HttpClient _httpClient;
    private const string BaseUrl = "https://bis.com.kh";
    private const string ApiUrl = "/api/repairservices?api-version=1.0";

    public RepairItemService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    private string GetFullUrl(string relativeUrl)
    {
        return $"{BaseUrl}{relativeUrl}";
    }

    public async Task<IEnumerable<ServiceType>> GetServiceTypesAsync()
    {
        var fullUrl = GetFullUrl("/api/repairservices/servicetypes?api-version=1.0");
        return await _httpClient.GetFromJsonAsync<IEnumerable<ServiceType>>(fullUrl);
    }

    public async Task<IEnumerable<ServicePriority>> GetServicePrioritiesAsync()
    {
        var fullUrl = GetFullUrl("/api/repairservices/servicepriorities?api-version=1.0");
        return await _httpClient.GetFromJsonAsync<IEnumerable<ServicePriority>>(fullUrl);
    }

    public async Task<IEnumerable<ServiceStatus>> GetServiceStatusesAsync()
    {
        var fullUrl = GetFullUrl("/api/repairservices/servicestatuses?api-version=1.0");
        return await _httpClient.GetFromJsonAsync<IEnumerable<ServiceStatus>>(fullUrl);
    }

    public async Task<IEnumerable<Repairs>> GetRepairsAsync()
    {
        
        return await _httpClient.GetFromJsonAsync<IEnumerable<Repairs>>("https://bis.com.kh/api/items?api-version=1.0\r\n");
    }

    public async Task<List<RepairServices>> GetAllRepairServicesAsync()
    {
        var fullUrl = GetFullUrl("/api/repairservices?api-version=1.0");
        return await _httpClient.GetFromJsonAsync<List<RepairServices>>(fullUrl);
    }

    public async Task<RepairServices> GetRepairServiceByIdAsync(Guid id)
    {
        var fullUrl = GetFullUrl($"/api/repairservices/{id}?api-version=1.0");
        var response = await _httpClient.GetAsync(fullUrl);
        if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
        {
            // Handle 404 error, e.g., log it or notify the user
            return null;
        }
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<RepairServices>();
    }

    public async Task CreateRepairServiceAsync(RepairServices repairService)
    {
        var fullUrl = GetFullUrl("/api/repairservices?api-version=1.0");
        var response = await _httpClient.PostAsJsonAsync(fullUrl, repairService);
        response.EnsureSuccessStatusCode();
    }

    public async Task UpdateRepairServiceAsync(RepairServices repairService)
    {
        // Use the base URL for the update endpoint
        var fullUrl = GetFullUrl(ApiUrl);

        // Send the PUT request with the repairService object
        var response = await _httpClient.PutAsJsonAsync(fullUrl, repairService);

        // Ensure the response status code indicates success
        response.EnsureSuccessStatusCode();
    }


    public async Task DeleteRepairServiceAsync(Guid id)
    {
        var fullUrl = GetFullUrl($"/api/repairservices/{id}?api-version=1.0");
        var response = await _httpClient.DeleteAsync(fullUrl);
        response.EnsureSuccessStatusCode();
    }
   


}
