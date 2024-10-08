using Microsoft.AspNetCore.Components;
using ServiceMaintenance.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace ServiceMaintenance.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly HttpClient httpClient;

        public CategoryService(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        public async Task<Category> CreateData(Category newData)
        {
            try
            {
                var response = await httpClient.PostAsJsonAsync("api/Category", newData);
                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadFromJsonAsync<Category>();
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
                Console.WriteLine($"Exception in CreateCategory: {ex.Message}");
                throw;
            }
        }

        public async Task DeleteData(int id)
        {
            await httpClient.DeleteAsync($"api/Category/{id}");
        }

        public async Task<IEnumerable<Category>> GetData()
        {
            return await httpClient.GetFromJsonAsync<Category[]>("api/Category");
        }

        public async Task<Category> GetData(int id)
        {
            return await httpClient.GetFromJsonAsync<Category>($"api/Category/{id}");
        }

        public async Task<Category> UpdateData(Category updatedData)
        {
            var response = await httpClient.PutAsJsonAsync("api/Category", updatedData);
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<Category>();
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
