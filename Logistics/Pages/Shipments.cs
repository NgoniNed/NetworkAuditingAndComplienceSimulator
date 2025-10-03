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

        private async void AddShipment(Shipment newShipment)
        {
            shipments.Add(newShipment);
            var logisticssourceBaseUrl = Configuration["CentralServerBaseUrl"];
            logisticssourceBaseUrl = "https://localhost:57238";
            Console.WriteLine($"Pushing to Logistics API");
            try
            {

                Console.WriteLine($"Trying to Push to Logistics API");
                using (HttpClient client = new HttpClient())
                {
                    var response = await client.PostAsJsonAsync<Shipment>($"{logisticssourceBaseUrl}/api/Logistics/PushShipment", newShipment);

                    Console.WriteLine(response.ReasonPhrase);
                    Console.WriteLine(response.StatusCode);
                    Console.WriteLine(response.RequestMessage);
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error posting {newShipment.GetType().Name}: {ex.Message}");
            }
            HideCreateForm();
        }

        private async Task UpdateShipmentAsync(Shipment updatedShipment)
        {
            /*var existingShipment = shipments.FirstOrDefault(e => e.ShipmentId == updatedShipment.ShipmentId);
            if (existingShipment != null)
            {
                existingShipment.ShipmentNumber = updatedShipment.ShipmentNumber;
                existingShipment.Origin = updatedShipment.Origin;
                existingShipment.Destination = updatedShipment.Destination;
                existingShipment.ExpectedDeliveryDate = updatedShipment.ExpectedDeliveryDate;
                existingShipment.CurrentStatus = updatedShipment.CurrentStatus;
            }*/
            var logisticssourceBaseUrl = Configuration["CentralServerBaseUrl"];
            logisticssourceBaseUrl = "https://localhost:57238";
            Console.WriteLine($"Pushing UpdateShipmentAsync to Logistics API");
            try
            {

                Console.WriteLine($"Trying to Push UpdateShipmentAsync to Logistics API");
                using (HttpClient client = new HttpClient())
                {
                    var response = await client.PostAsJsonAsync<Shipment>($"{logisticssourceBaseUrl}/api/Logistics/UpdateShipment", updatedShipment);

                    Console.WriteLine(response.ReasonPhrase);
                    Console.WriteLine(response.StatusCode);
                    Console.WriteLine(response.RequestMessage);
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error Updating {updatedShipment.GetType().Name}: {ex.Message}");
            }
            HideEditForm();
        }

        private async Task DeleteShipmentAsync(Shipment shipmentToDelete)
        {
            var logisticssourceBaseUrl = Configuration["CentralServerBaseUrl"];
            logisticssourceBaseUrl = "https://localhost:57238";
            Console.WriteLine($"Pushing DeleteShipmentAsync to Logistics API");
            try
            {

                Console.WriteLine($"Trying to Push DeleteShipmentAsync to Logistics API");
                using (HttpClient client = new HttpClient())
                {
                    var response = await client.PostAsJsonAsync<Shipment>($"{logisticssourceBaseUrl}/api/Logistics/DeleteShipment", shipmentToDelete);

                    Console.WriteLine(response.ReasonPhrase);
                    Console.WriteLine(response.StatusCode);
                    Console.WriteLine(response.RequestMessage);
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error Deleting {shipmentToDelete.GetType().Name}: {ex.Message}");
            }
            shipments.Remove(shipmentToDelete);
        }
    }
}