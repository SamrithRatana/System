using Microsoft.AspNetCore.SignalR.Client;
using System;
using System.Threading.Tasks;

public class SignalRService
{
    private readonly HubConnection _hubConnection;

    public SignalRService(HubConnection hubConnection)
    {
        _hubConnection = hubConnection;

        // Handle received messages
        _hubConnection.On<string>("ReceiveMessage", (message) =>
        {
            // Handle received messages here
        });

        // Handle connection state changes
        _hubConnection.Closed += async (error) =>
        {
            // Handle connection closed
            Console.WriteLine("Connection closed: " + error?.Message);
            await Task.Delay(new Random().Next(0, 5) * 1000); // Optional: Wait before reconnecting
            await StartAsync(); // Try to reconnect
        };

        _hubConnection.Reconnected += (connectionId) =>
        {
            // Handle reconnection
            Console.WriteLine("Reconnected with connectionId: " + connectionId);
            return Task.CompletedTask;
        };

        _hubConnection.Reconnecting += (error) =>
        {
            // Handle reconnecting
            Console.WriteLine("Reconnecting: " + error?.Message);
            return Task.CompletedTask;
        };
    }

    public async Task StartAsync()
    {
        await _hubConnection.StartAsync();
    }

    public async Task SendMessageAsync(string message)
    {
        await _hubConnection.SendAsync("SendMessage", message);
    }
}
