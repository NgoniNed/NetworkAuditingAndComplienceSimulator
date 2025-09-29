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

        private void AddFuel(FuelLog newFuel)
        {
            fuelLogs.Add(newFuel);
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
