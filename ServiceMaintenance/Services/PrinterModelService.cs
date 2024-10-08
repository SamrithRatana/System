using Microsoft.AspNetCore.Components;
using ServiceMaintenance.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace ServiceMaintenance.Services
{
    public class PrinterModelService : IPrinterModelService
    {
        private readonly HttpClient httpClient;

        public PrinterModelService(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        public async Task<PrinterModel> CreateData(PrinterModel newData)
        {
            try
            {
                var response = await httpClient.PostAsJsonAsync("api/PrinterModel", newData);
                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadFromJsonAsync<PrinterModel>();
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
                Console.WriteLine($"Exception in CreatePrinterModel: {ex.Message}");
                throw;
            }
        }

        public async Task DeleteData(int id)
        {
            await httpClient.DeleteAsync($"api/PrinterModel/{id}");
        }

        public async Task<IEnumerable<PrinterModel>> GetData()
        {
            return await httpClient.GetFromJsonAsync<PrinterModel[]>("api/PrinterModel");
        }

        public async Task<PrinterModel> GetData(int id)
        {
            return await httpClient.GetFromJsonAsync<PrinterModel>($"api/PrinterModel/{id}");
        }

        public async Task<PrinterModel> UpdateData(PrinterModel updatedData)
        {
            var response = await httpClient.PutAsJsonAsync("api/PrinterModel", updatedData);
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<PrinterModel>();
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
