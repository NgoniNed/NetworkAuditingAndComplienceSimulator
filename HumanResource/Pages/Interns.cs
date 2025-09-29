using System;
using System.Collections.Generic;
using System.Linq;
using SharedLibrary.Data.HumanResource;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Configuration;
using System.Net.Http;

namespace HumanResource.Pages
{
    public partial class Interns
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
        private List<Intern> interns = new List<Intern>();

        private bool showCreateForm = false;
        private bool showEditForm = false;
        private Intern selectedIntern;

        protected override async Task OnInitializedAsync()
        {
            var humanresourceBaseUrl = Configuration["CentralServerBaseUrl"];
            humanresourceBaseUrl = "https://localhost:27078";
            try
            {
                interns = await Http.GetFromJsonAsync<List<Intern>>($"{humanresourceBaseUrl}/api/HumanResource/GetIntern");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error fetching Interns: {ex.Message}");
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

        private void ShowEditForm(Intern intern)
        {
            selectedIntern = intern;
            showEditForm = true;
        }

        private void HideEditForm()
        {
            showEditForm = false;
            selectedIntern = null;
        }

        private void AddIntern(Intern newIntern)
        {
            interns.Add(newIntern);
            HideCreateForm();
        }

        private void UpdateIntern(Intern updatedIntern)
        {
            var existingIntern = interns.FirstOrDefault(e => e.InternId == updatedIntern.InternId);
            if (existingIntern != null)
            {
                existingIntern.Name = updatedIntern.Name;
                existingIntern.Department = updatedIntern.Department;
                existingIntern.Position = updatedIntern.Position;
                existingIntern.Email = updatedIntern.Email;
            }
            HideEditForm();
        }

        private void DeleteIntern(Intern internToDelete)
        {
            interns.Remove(internToDelete);
        }
    }
}
