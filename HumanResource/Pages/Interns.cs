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
                interns = await Http.GetFromJsonAsync<List<Intern>>($"{humanresourceBaseUrl}/api/HumanResources/GetIntern");
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

        private async void AddIntern(Intern newIntern)
        {
            interns.Add(newIntern);
            var humanresourcesourceBaseUrl = Configuration["CentralServerBaseUrl"];
            humanresourcesourceBaseUrl = "https://localhost:57238";
            Console.WriteLine($"Pushing to Human Resource API");
            try
            {

                Console.WriteLine($"Trying to Push to Human Resource API");
                using (HttpClient client = new HttpClient())
                {
                    var response = await client.PostAsJsonAsync<Intern>($"{humanresourcesourceBaseUrl}/api/HumanResources/PushIntern", newIntern);

                    Console.WriteLine(response.ReasonPhrase);
                    Console.WriteLine(response.StatusCode);
                    Console.WriteLine(response.RequestMessage);
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error posting {newIntern.GetType().Name}: {ex.Message}");
            }
            HideCreateForm();
        }

        private async Task UpdateInternAsync(Intern updatedIntern)
        {
            var existingIntern = interns.FirstOrDefault(e => e.InternId == updatedIntern.InternId);
            if (existingIntern != null)
            {
                existingIntern.Name = updatedIntern.Name;
                existingIntern.Department = updatedIntern.Department;
                existingIntern.Position = updatedIntern.Position;
                existingIntern.Email = updatedIntern.Email;
            }
            var humanresourcesourceBaseUrl = Configuration["CentralServerBaseUrl"];
            humanresourcesourceBaseUrl = "https://localhost:57238";
            Console.WriteLine($"Pushing UpdateIntern to Human Resource API");
            try
            {

                Console.WriteLine($"Trying to Push UpdateIntern to Human Resource API");
                using (HttpClient client = new HttpClient())
                {
                    var response = await client.PostAsJsonAsync<Intern>($"{humanresourcesourceBaseUrl}/api/HumanResources/UpdateIntern", updatedIntern);

                    Console.WriteLine(response.ReasonPhrase);
                    Console.WriteLine(response.StatusCode);
                    Console.WriteLine(response.RequestMessage);
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error updating {updatedIntern.GetType().Name}: {ex.Message}");
            }
            HideEditForm();
        }

        private async Task DeleteInternAsync(Intern internToDelete)
        {
            var humanresourcesourceBaseUrl = Configuration["CentralServerBaseUrl"];
            humanresourcesourceBaseUrl = "https://localhost:57238";
            Console.WriteLine($"Pushing DeleteIntern to Human Resource API");
            try
            {

                Console.WriteLine($"Trying to Push DeleteIntern to Human Resource API");
                using (HttpClient client = new HttpClient())
                {
                    var response = await client.PostAsJsonAsync<Intern>($"{humanresourcesourceBaseUrl}/api/HumanResources/DeleteIntern", internToDelete);

                    Console.WriteLine(response.ReasonPhrase);
                    Console.WriteLine(response.StatusCode);
                    Console.WriteLine(response.RequestMessage);
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error deleting {internToDelete.GetType().Name}: {ex.Message}");
            }
            interns.Remove(internToDelete);
        }
    }
}
