using Microsoft.AspNetCore.Components;
using ServiceMaintenance.Models;

namespace ServiceMaintenance.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly HttpClient httpClient;
        public CustomerService(HttpClient httpClient) {
            this.httpClient = httpClient;
        }

        public async Task<Customer> CreateReport(Customer newReport)
        {

            try
            {
                var response = await httpClient.PostAsJsonAsync("api/Customer", newReport);
                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadFromJsonAsync<Customer>();
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
                Console.WriteLine($"Exception in CreateCustomer: {ex.Message}");
                throw;
            }
        }

        public async Task DeleteReport(int id)
        {
            await httpClient.DeleteAsync($"api/Customer/{id}");
        }
       

        public async Task<IEnumerable<Customer>> GetReport()
        {
            return await httpClient.GetJsonAsync<Customer[]>("api/Customer");
        }

        public async Task<Customer> GetReport(int id)
        {
            return await httpClient.GetFromJsonAsync<Customer>($"api/Customer/{id}");
        }

        public async Task<Customer> UpdateReport(Customer updatedReport)
        {
            var response = await httpClient.PutAsJsonAsync("api/Customer", updatedReport);
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<Customer>();
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
