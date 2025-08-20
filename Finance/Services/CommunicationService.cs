using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.Extensions.Configuration;
using System;
using System.Threading.Tasks;
using Finance.Data;

namespace Finance.Services
{
    public class CommunicationService : IAsyncDisposable
{
    private readonly string _hubUrl;
    private HubConnection _hubConnection;
    private readonly string _departmentName;
    private readonly IConfiguration _configuration;
    public event Action<Message> MessageReceived;

    public CommunicationService(IConfiguration configuration)
    {
        _configuration = configuration;
        _departmentName = _configuration["DepartmentName"];
        _hubUrl = "http://127.0.0.1:8000/communicationHub";

        _hubConnection = new HubConnectionBuilder()
            .WithUrl(_hubUrl)
            .Build();

        _hubConnection.On<Message>("ReceiveMessage", (message) =>
        {
            MessageReceived?.Invoke(message);
        });
    }

    public async Task StartConnection()
    {
        try
        {
            await _hubConnection.StartAsync();
            Console.WriteLine($"SignalR connection started for {_departmentName}.");
            await _hubConnection.InvokeAsync("JoinDepartmentGroup", _departmentName);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error starting SignalR connection: {ex.Message}");
        }
    }

    public async Task SendMessage(string recipientDepartment, string messageContent, MessageType messageType)
    {
        if (_hubConnection.State != HubConnectionState.Connected)
        {
            Console.WriteLine("SignalR connection is not active.");
            return;
        }

        Message message = new Message
        {
            SenderDepartment = _departmentName,
            RecipientDepartment = recipientDepartment,
            MessageContent = messageContent,
            Type = messageType
        };

        try
        {
            await _hubConnection.InvokeAsync("SendMessage", message);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error sending message: {ex.Message}");
        }
    }

    public async ValueTask DisposeAsync()
    {
        if (_hubConnection is not null)
        {
            await _hubConnection.DisposeAsync();
        }
    }
}
}