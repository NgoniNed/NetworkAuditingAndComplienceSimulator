using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Configuration;
using SharedLibrary.Data.HumanResource;

namespace HumanResource.Pages
{
    public partial class Pensioners
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
        private List<Pensioner> pensioners = new List<Pensioner>();

        private bool showCreateForm = false;
        private bool showEditForm = false;
        private Pensioner selectedPensioner;

        protected override async Task OnInitializedAsync()
        {
            var humanresourceBaseUrl = Configuration["CentralServerBaseUrl"];
            humanresourceBaseUrl = "https://localhost:27078";
            try
            {
                pensioners = await Http.GetFromJsonAsync<List<Pensioner>>($"{humanresourceBaseUrl}/api/HumanResources/GetPensioner");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error fetching Pensioners: {ex.Message}");
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

        private void ShowEditForm(Pensioner pensioner)
        {
            selectedPensioner = pensioner;
            showEditForm = true;
        }

        private void HideEditForm()
        {
            showEditForm = false;
            selectedPensioner = null;
        }

        private void AddPensioner(Pensioner newPensioner)
        {
            pensioners.Add(newPensioner);
            HideCreateForm();
        }

        private void UpdatePensioner(Pensioner updatedPensioner)
        {
            var existingPensioner = pensioners.FirstOrDefault(e => e.PensionerId == updatedPensioner.PensionerId);
            if (existingPensioner != null)
            {
                existingPensioner.Name = updatedPensioner.Name;
                existingPensioner.LastDepartment = updatedPensioner.LastDepartment;
                existingPensioner.LastPosition = updatedPensioner.LastPosition;
                existingPensioner.Email = updatedPensioner.Email;
            }
            HideEditForm();
        }

        private void DeletePensioner(Pensioner pensionerToDelete)
        {
            pensioners.Remove(pensionerToDelete);
        }
    }
}
