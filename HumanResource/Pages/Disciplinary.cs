using System;
using System.Collections.Generic;
using System.Linq;
using SharedLibrary.Data.HumanResource;

using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Configuration;

namespace HumanResource.Pages
{
    public partial class Disciplinary
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
        private List<DisciplinaryCase> cases = new List<DisciplinaryCase>();

        private bool showCreateForm = false;
        private bool showEditForm = false;
        private DisciplinaryCase selectedCase;

        
        protected override async Task OnInitializedAsync()
        {
            var humanresourceBaseUrl = Configuration["CentralServerBaseUrl"];
            humanresourceBaseUrl = "https://localhost:27078";
            try
            {
                cases = await Http.GetFromJsonAsync<List<DisciplinaryCase>>($"{humanresourceBaseUrl}/api/HumanResources/GetDisciplinaryCase");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error fetching Departments: {ex.Message}");
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

        private void ShowEditForm(DisciplinaryCase caseItem)
        {
            selectedCase = caseItem;
            showEditForm = true;
        }

        private void HideEditForm()
        {
            showEditForm = false;
            selectedCase = null;
        }

        private async void AddCase(DisciplinaryCase newCase)
        {
            cases.Add(newCase);
            var humanresourcesourceBaseUrl = Configuration["CentralServerBaseUrl"];
            humanresourcesourceBaseUrl = "https://localhost:57238";
            Console.WriteLine($"Pushing to Human Resource API");
            try
            {

                Console.WriteLine($"Trying to Push to Human Resource API");
                using (HttpClient client = new HttpClient())
                {
                    var response = await client.PostAsJsonAsync<DisciplinaryCase>($"{humanresourcesourceBaseUrl}/api/HumanResources/PushDisciplinaryCase", newCase);

                    Console.WriteLine(response.ReasonPhrase);
                    Console.WriteLine(response.StatusCode);
                    Console.WriteLine(response.RequestMessage);
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error posting {newCase.GetType().Name}: {ex.Message}");
            }
            HideCreateForm();
        }

        private async Task UpdateCaseAsync(DisciplinaryCase updatedCase)
        {
            var existingCase = cases.FirstOrDefault(e => e.CaseId == updatedCase.CaseId);
            if (existingCase != null)
            {
                existingCase.EmployeeName = updatedCase.EmployeeName;
                existingCase.Date = updatedCase.Date;
                existingCase.Reason = updatedCase.Reason;
            }
            var humanresourcesourceBaseUrl = Configuration["CentralServerBaseUrl"];
            humanresourcesourceBaseUrl = "https://localhost:57238";
            Console.WriteLine($"Pushing UpdateCase to Human Resource API");
            try
            {

                Console.WriteLine($"Trying to Push UpdateCase to Human Resource API");
                using (HttpClient client = new HttpClient())
                {
                    var response = await client.PostAsJsonAsync<DisciplinaryCase>($"{humanresourcesourceBaseUrl}/api/HumanResources/PushDisciplinaryCase", updatedCase);

                    Console.WriteLine(response.ReasonPhrase);
                    Console.WriteLine(response.StatusCode);
                    Console.WriteLine(response.RequestMessage);
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error updating {updatedCase.GetType().Name}: {ex.Message}");
            }
            HideEditForm();
        }

        private async Task DeleteCaseAsync(DisciplinaryCase caseToDelete)
        {
            var humanresourcesourceBaseUrl = Configuration["CentralServerBaseUrl"];
            humanresourcesourceBaseUrl = "https://localhost:57238";
            Console.WriteLine($"Pushing DeleteCase to Human Resource API");
            try
            {

                Console.WriteLine($"Trying to Push DeleteCase to Human Resource API");
                using (HttpClient client = new HttpClient())
                {
                    var response = await client.PostAsJsonAsync<DisciplinaryCase>($"{humanresourcesourceBaseUrl}/api/HumanResources/PushDisciplinaryCase", caseToDelete);

                    Console.WriteLine(response.ReasonPhrase);
                    Console.WriteLine(response.StatusCode);
                    Console.WriteLine(response.RequestMessage);
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error deleting {caseToDelete.GetType().Name}: {ex.Message}");
            }
            cases.Remove(caseToDelete);
        }
    }
}
