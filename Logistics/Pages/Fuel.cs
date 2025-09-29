using System;
using System.Collections.Generic;
using System.Linq;
using SharedLibrary.Data.Logistics;

namespace Logistics.Pages
{
    public partial class Fuel
    {
        private List<FuelLog> fuelLogs = new List<FuelLog>();

        private bool showCreateForm = false;
        private bool showEditForm = false;
        private FuelLog selectedFuelLog;

        protected override void OnInitialized()
        {
            fuelLogs = GetFuelLogs();
        }
        private List<FuelLog> GetFuelLogs()
        {
            throw new NotImplementedException();
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
