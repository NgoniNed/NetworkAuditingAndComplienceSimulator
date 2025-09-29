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
        public HttpClient Http { get; set; }
        [Inject]
        public IConfiguration Configuration { get; set; }

        private List<Shipment> shipments = new List<Shipment>();
        private bool showCreateForm = false;
        private bool showEditForm = false;
        private Shipment selectedShipment;

        protected override async Task OnInitializedAsync()
        {
            string baseUrl = Configuration["CentralServerBaseUrl"];
            // If running locally, adjust the URL if needed
            var apiUrl = $"{baseUrl}/api/Logistics/GetShipment";
            try
            {
                shipments = await Http.GetFromJsonAsync<List<Shipment>>(apiUrl);
            }
            catch (Exception ex)
            {
                // fallback or show error
                shipments = new List<Shipment>();
                Console.WriteLine($"Error fetching shipments: {ex.Message}");
            }
        }

    }
}