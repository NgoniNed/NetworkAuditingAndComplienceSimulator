using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;
using CentralServer.Data;
using Microsoft.Extensions.Configuration;

namespace CentralServer.Hubs
{
    public class CommunicationHub : Hub
    {
        private readonly IConfiguration _configuration;

        public CommunicationHub(IConfiguration configuration)
        {
            _configuration = configuration;
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
    }
}