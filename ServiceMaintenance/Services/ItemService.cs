using ServiceMaintenance.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

public class ItemService
{
    private readonly HttpClient _httpClient;
    private const string BaseUrl = "https://bis.com.kh";
    private const string ApiUrl = "/api/items?api-version=1.0";

    public ItemService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    private string GetFullUrl(string relativeUrl)
    {
        return $"{BaseUrl}{relativeUrl}";
    }

    public async Task<IEnumerable<Repairs>> GetItemsAsync()
    {
        var fullUrl = GetFullUrl(ApiUrl);
        return await _httpClient.GetFromJsonAsync<IEnumerable<Repairs>>(fullUrl);
    }

    public async Task<Repairs> GetItemByIdAsync(Guid id)
    {
        var fullUrl = GetFullUrl($"{ApiUrl}/{id}");
        return await _httpClient.GetFromJsonAsync<Repairs>(fullUrl);
    }

    public async Task CreateItemAsync(Repairs item)
    {
        if (!await IsSerialNumberUniqueAsync(item.SerialNumber))
        {
            throw new InvalidOperationException("Serial number must be unique.");
        }

        var fullUrl = GetFullUrl(ApiUrl);
        var response = await _httpClient.PostAsJsonAsync(fullUrl, item);
        response.EnsureSuccessStatusCode();
    }

    public async Task UpdateItemAsync(Repairs item)
    {
        if (!await IsSerialNumberUniqueAsync(item.SerialNumber, item.Id))
        {
            throw new InvalidOperationException("Serial number must be unique.");
        }

        var fullUrl = GetFullUrl($"{ApiUrl}/{item.Id}");
        var response = await _httpClient.PutAsJsonAsync(fullUrl, item);
        response.EnsureSuccessStatusCode();
    }

    public async Task DeleteItemAsync(Guid id)
    {
        var fullUrl = GetFullUrl($"/api/items/{id}?api-version=1.0");
        var response = await _httpClient.DeleteAsync(fullUrl);
        if (!response.IsSuccessStatusCode)
        {
            var responseContent = await response.Content.ReadAsStringAsync();
            // Log or handle the response content
            throw new HttpRequestException($"Delete request failed: {responseContent}");
        }
    }
    private async Task<bool> IsSerialNumberUniqueAsync(string serialNumber, Guid? itemId = null)
    {
        // Fetch existing items to check for uniqueness
        var existingItems = await GetItemsAsync();
        return existingItems.All(i => i.SerialNumber != serialNumber || i.Id == itemId);
    }
}
