using Microsoft.AspNetCore.Components;
using ServiceMaintenance.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace ServiceMaintenance.Services
{
    public class ManufacturerService : IManufacturerService
    {
        private readonly HttpClient _httpClient;

        public ManufacturerService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<Manufacturer> CreateData(Manufacturer newData)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync("api/Manufacturer", newData);
                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadFromJsonAsync<Manufacturer>();
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
                Console.WriteLine($"Exception in CreateData: {ex.Message}");
                throw;
            }
        }

        public async Task DeleteData(int id)
        {
            try
            {
                var response = await _httpClient.DeleteAsync($"api/Manufacturer/{id}");
                response.EnsureSuccessStatusCode();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception in DeleteData: {ex.Message}");
                throw;
            }
        }

        public async Task<IEnumerable<Manufacturer>> GetData()
        {
            try
            {
                return await _httpClient.GetFromJsonAsync<Manufacturer[]>("api/Manufacturer");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception in GetData: {ex.Message}");
                throw;
            }
        }

        public async Task<Manufacturer> GetData(int id)
        {
            try
            {
                return await _httpClient.GetFromJsonAsync<Manufacturer>($"api/Manufacturer/{id}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception in GetData(int id): {ex.Message}");
                throw;
            }
        }

        public async Task<Manufacturer> UpdateData(Manufacturer updatedData)
        {
            try
            {
                var response = await _httpClient.PutAsJsonAsync($"api/Manufacturer", updatedData);
                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadFromJsonAsync<Manufacturer>();
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
                Console.WriteLine($"Exception in UpdateData: {ex.Message}");
                throw;
            }
        }
    }
}
