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

        private async void AddPensioner(Pensioner newPensioner)
        {
            pensioners.Add(newPensioner);
            var humanresourcesourceBaseUrl = Configuration["CentralServerBaseUrl"];
            humanresourcesourceBaseUrl = "https://localhost:57238";
            Console.WriteLine($"Pushing to Human Resource API");
            try
            {

                Console.WriteLine($"Trying to Push to Human Resource API");
                using (HttpClient client = new HttpClient())
                {
                    var response = await client.PostAsJsonAsync<Pensioner>($"{humanresourcesourceBaseUrl}/api/HumanResources/PushPensioner", newPensioner);

                    Console.WriteLine(response.ReasonPhrase);
                    Console.WriteLine(response.StatusCode);
                    Console.WriteLine(response.RequestMessage);
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error posting {newPensioner.GetType().Name}: {ex.Message}");
            }
            HideCreateForm();
        }

        private async Task UpdatePensionerAsync(Pensioner updatedPensioner)
        {
            var existingPensioner = pensioners.FirstOrDefault(e => e.PensionerId == updatedPensioner.PensionerId);
            if (existingPensioner != null)
            {
                existingPensioner.Name = updatedPensioner.Name;
                existingPensioner.LastDepartment = updatedPensioner.LastDepartment;
                existingPensioner.LastPosition = updatedPensioner.LastPosition;
                existingPensioner.Email = updatedPensioner.Email;
            }
            var humanresourcesourceBaseUrl = Configuration["CentralServerBaseUrl"];
            humanresourcesourceBaseUrl = "https://localhost:57238";
            Console.WriteLine($"Pushing UpdatePensionerAsync to Human Resource API");
            try
            {

                Console.WriteLine($"Trying to Push UpdatePensionerAsync to Human Resource API");
                using (HttpClient client = new HttpClient())
                {
                    var response = await client.PostAsJsonAsync<Pensioner>($"{humanresourcesourceBaseUrl}/api/HumanResources/UpdatePensioner", updatedPensioner);

                    Console.WriteLine(response.ReasonPhrase);
                    Console.WriteLine(response.StatusCode);
                    Console.WriteLine(response.RequestMessage);
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error updating {updatedPensioner.GetType().Name}: {ex.Message}");
            }
            HideEditForm();
        }

        private async Task DeletePensionerAsync(Pensioner pensionerToDelete)
        {
            var humanresourcesourceBaseUrl = Configuration["CentralServerBaseUrl"];
            humanresourcesourceBaseUrl = "https://localhost:57238";
            Console.WriteLine($"Pushing DeletePensionerAsync to Human Resource API");
            try
            {

                Console.WriteLine($"Trying to Push DeletePensionerAsync to Human Resource API");
                using (HttpClient client = new HttpClient())
                {
                    var response = await client.PostAsJsonAsync<Pensioner>($"{humanresourcesourceBaseUrl}/api/HumanResources/DeletePensioner", pensionerToDelete);

                    Console.WriteLine(response.ReasonPhrase);
                    Console.WriteLine(response.StatusCode);
                    Console.WriteLine(response.RequestMessage);
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error deleting {pensionerToDelete.GetType().Name}: {ex.Message}");
            }
            pensioners.Remove(pensionerToDelete);
        }
    }
}
