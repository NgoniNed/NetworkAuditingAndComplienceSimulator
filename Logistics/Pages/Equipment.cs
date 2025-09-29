using System;
using System.Collections.Generic;
using System.Linq;

using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Configuration;
namespace Logistics.Pages
{
    public partial class Equipment
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
        private List<SharedLibrary.Data.Logistics.Equipment> equipments = new List<SharedLibrary.Data.Logistics.Equipment>();

        private bool showCreateForm = false;
        private bool showEditForm = false;
        private SharedLibrary.Data.Logistics.Equipment selectedEquipment;

        

        protected override async Task OnInitializedAsync()
        {
            var logisticssourceBaseUrl = Configuration["CentralServerBaseUrl"];
            logisticssourceBaseUrl = "https://localhost:57238";
            try
            {
                equipments = await Http.GetFromJsonAsync<List<SharedLibrary.Data.Logistics.Equipment>>($"{logisticssourceBaseUrl}/api/Logistics/GetEquipment");
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

        private void ShowEditForm(SharedLibrary.Data.Logistics.Equipment equipment)
        {
            selectedEquipment = equipment;
            showEditForm = true;
        }

        private void HideEditForm()
        {
            showEditForm = false;
            selectedEquipment = null;
        }

        private void AddEquipment(SharedLibrary.Data.Logistics.Equipment newEquipment)
        {
            equipments.Add(newEquipment);
            HideCreateForm();
        }

        private void UpdateEquipment(SharedLibrary.Data.Logistics.Equipment updatedEquipment)
        {
            var existingEquipment = equipments.FirstOrDefault(e => e.EquipmentId == updatedEquipment.EquipmentId);
            if (existingEquipment != null)
            {
                existingEquipment.Name = updatedEquipment.Name;
                existingEquipment.Type = updatedEquipment.Type;
                existingEquipment.Make = updatedEquipment.Make;
                existingEquipment.Model = updatedEquipment.Model;
                existingEquipment.NextServiceDate = updatedEquipment.NextServiceDate;
            }
            HideEditForm();
        }

        private void DeleteEquipment(SharedLibrary.Data.Logistics.Equipment equipmentToDelete)
        {
            equipments.Remove(equipmentToDelete);
        }
    }
}
