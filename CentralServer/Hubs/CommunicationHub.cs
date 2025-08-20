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
            // Basic validation (add more as needed)
            if (string.IsNullOrEmpty(message.SenderDepartment) || string.IsNullOrEmpty(message.MessageContent))
            {
                return; // Or throw an exception
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
                    // Send to all departments.  Consider a more efficient approach if needed.
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