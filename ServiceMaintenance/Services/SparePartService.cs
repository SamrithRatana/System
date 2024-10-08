using Microsoft.AspNetCore.Components;
using ServiceMaintenance.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace ServiceMaintenance.Services
{
    public class SparePartService : ISparePartService
    {
        private readonly HttpClient httpClient;

        public SparePartService(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        public async Task<SparePart> CreateData(SparePart newData)
        {
            try
            {
                var response = await httpClient.PostAsJsonAsync("api/SparePart", newData);
                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadFromJsonAsync<SparePart>();
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
                var response = await httpClient.DeleteAsync($"api/SparePart/{id}");
                response.EnsureSuccessStatusCode();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception in DeleteData: {ex.Message}");
                throw;
            }
        }

        public async Task<IEnumerable<SparePart>> GetData()
        {
            try
            {
                return await httpClient.GetFromJsonAsync<SparePart[]>("api/SparePart");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception in GetData: {ex.Message}");
                throw;
            }
        }

        public async Task<SparePart> GetData(int id)
        {
            try
            {
                return await httpClient.GetFromJsonAsync<SparePart>($"api/SparePart/{id}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception in GetData(int id): {ex.Message}");
                throw;
            }
        }

        public async Task<SparePart> UpdateData(SparePart updatedData)
        {
            try
            {
                var response = await httpClient.PutAsJsonAsync($"api/SparePart", updatedData);
                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadFromJsonAsync<SparePart>();
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
