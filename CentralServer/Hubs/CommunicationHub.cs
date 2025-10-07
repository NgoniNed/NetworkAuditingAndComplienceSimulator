using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;
using CentralServer.Data;
using Microsoft.Extensions.Configuration;
using CentralServer.Services;

namespace CentralServer.Hubs
{
    public class CommunicationHub : Hub
    {
        private readonly IConfiguration _configuration;
        private readonly DataService _dataService;

        public CommunicationHub(IConfiguration configuration, DataService dataService)
        {
            _configuration = configuration;
            _dataService = dataService;
        }

        public async Task SendMessage(Message message)
        {
            if (string.IsNullOrEmpty(message.SenderDepartment) || string.IsNullOrEmpty(message.MessageContent))
            {
                return; 
            }

            //Routing logic
            switch (message.Type)
            {
                case MessageType.Department:
                    await Clients.Group(message.RecipientDepartment).SendAsync("ReceiveMessage", message);
                    break;
                case MessageType.CrossDepartment:
                    await Clients.Group(message.RecipientDepartment).SendAsync("ReceiveMessage", message);
                    break;
                case MessageType.DepartmentAnnouncement:
                    await Clients.Group(message.SenderDepartment).SendAsync("ReceiveMessage", message);
                    break;
                case MessageType.OrganizationAnnouncement:
                    string[] allowedDepartments = _configuration.GetSection("AllowedDepartments").Get<string[]>();

                    if (allowedDepartments != null)
                    {
                        foreach (var department in allowedDepartments)
                        {
                            await Clients.Group(department).SendAsync("ReceiveMessage", message);
                        }
                    }

                    break;
            }
        }

        public async Task JoinDepartmentGroup(string departmentName)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, departmentName);
        }

        public async Task RegisterDepartment(BaseLibrary.DataModels.Department department)
        {
            System.Console.WriteLine($"Central Server SignalR Hub Connected with {department.Name}");
            department.Status = "Active";
            department.DepartmentStatus = BaseLibrary.DataModels.DepartmentStatus.Active;
            _dataService.AddDepartmentLog(department);
            _dataService.AddEventMessage("Startup", $"{department.Name} is now online at {department.URL}:{department.Port}", department.Name);
            _dataService.AddLogMessage($"{department.Name} @ {department.URL}:{department.Port}", $"Central Server Communication Hub", department.DepartmentStatus.ToString(),"SignalR Hub");
            await Groups.AddToGroupAsync(Context.ConnectionId, department.Name);
        }

    }
}