using System;
using System.Collections.Generic;
using System.Linq;
using SharedLibrary.Data.Logistics;

using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Configuration;

namespace Logistics.Pages
{
    public partial class Fuel
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
        private List<FuelLog> fuelLogs = new List<FuelLog>();

        private bool showCreateForm = false;
        private bool showEditForm = false;
        private FuelLog selectedFuelLog;

        

        protected override async Task OnInitializedAsync()
        {
            var logisticssourceBaseUrl = Configuration["CentralServerBaseUrl"];
            logisticssourceBaseUrl = "https://localhost:57238";
            try
            {
                fuelLogs = await Http.GetFromJsonAsync<List<FuelLog>>($"{logisticssourceBaseUrl}/api/Logistics/GetFuelLog");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error fetching Costs Logs: {ex.Message}");
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

        private void ShowEditForm(FuelLog fuelLog)
        {
            selectedFuelLog = fuelLog;
            showEditForm = true;
        }

        private void HideEditForm()
        {
            showEditForm = false;
            selectedFuelLog = null;
        }

        private async void AddFuel(FuelLog newFuel)
        {
            fuelLogs.Add(newFuel);
            var logisticssourceBaseUrl = Configuration["CentralServerBaseUrl"];
            logisticssourceBaseUrl = "https://localhost:57238";
            Console.WriteLine($"Pushing to Logistics API");
            try
            {

                Console.WriteLine($"Trying to Push to Logistics API");
                using (HttpClient client = new HttpClient())
                {
                    var response = await client.PostAsJsonAsync<FuelLog>($"{logisticssourceBaseUrl}/api/Logistics/PushFuelLog", newFuel);

                    Console.WriteLine(response.ReasonPhrase);
                    Console.WriteLine(response.StatusCode);
                    Console.WriteLine(response.RequestMessage);
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error posting {newFuel.GetType().Name}: {ex.Message}");
            }
            HideCreateForm();
        }

        private void UpdateFuel(FuelLog updatedFuel)
        {
            var existingFuel = fuelLogs.FirstOrDefault(e => e.FuelLogId == updatedFuel.FuelLogId);
            if (existingFuel != null)
            {
                existingFuel.Date = updatedFuel.Date;
                existingFuel.EquipmentId = updatedFuel.EquipmentId;
                existingFuel.LitersFilled = updatedFuel.LitersFilled;
                existingFuel.Cost = updatedFuel.Cost;
                existingFuel.OdometerReading = updatedFuel.OdometerReading;
            }
            HideEditForm();
        }

        private void DeleteFuel(FuelLog fuelToDelete)
        {
            fuelLogs.Remove(fuelToDelete);
        }
    }
}
