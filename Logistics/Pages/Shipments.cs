using System;
using System.Collections.Generic;
using System.Linq;
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

        private void ShowCreateForm()
        {
            showCreateForm = true;
        }

        private void HideCreateForm()
        {
            showCreateForm = false;
        }

        private void ShowEditForm(Shipment shipment)
        {
            selectedShipment = shipment;
            showEditForm = true;
        }

        private void HideEditForm()
        {
            showEditForm = false;
            selectedShipment = null;
        }

        private void AddShipment(Shipment newShipment)
        {
            shipments.Add(newShipment);
            HideCreateForm();
        }

        private void UpdateShipment(Shipment updatedShipment)
        {
            var existingShipment = shipments.FirstOrDefault(e => e.ShipmentId == updatedShipment.ShipmentId);
            if (existingShipment != null)
            {
                existingShipment.ShipmentNumber = updatedShipment.ShipmentNumber;
                existingShipment.Origin = updatedShipment.Origin;
                existingShipment.Destination = updatedShipment.Destination;
                existingShipment.ExpectedDeliveryDate = updatedShipment.ExpectedDeliveryDate;
                existingShipment.CurrentStatus = updatedShipment.CurrentStatus;
            }
            HideEditForm();
        }

        private void DeleteShipment(Shipment shipmentToDelete)
        {
            shipments.Remove(shipmentToDelete);
        }
    }
}