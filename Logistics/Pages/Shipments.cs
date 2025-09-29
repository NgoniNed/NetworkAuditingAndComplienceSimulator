using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Configuration;
using SharedLibrary.Data.Logistics;

namespace Logistics.Pages
{
    public partial class Shipments
    {
        [Inject]
        HttpClient Http
        {
            get;
            set;
        }
        [Inject]
        IConfiguration Configuration
        {
            get;
            set;
        }

        private List<Shipment> shipments = new List<Shipment>();
        private bool showCreateForm = false;
        private bool showEditForm = false;
        private Shipment selectedShipment;

        protected override async Task OnInitializedAsync()
        {
            var logisticssourceBaseUrl = Configuration["CentralServerBaseUrl"];
            logisticssourceBaseUrl = "https://localhost:57238";
            try
            {
                shipments = await Http.GetFromJsonAsync<List<Shipment>>($"{logisticssourceBaseUrl}/api/Logistics/GetShipment");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error fetching Shipments Logs: {ex.Message}");
            }
        }
    }
}