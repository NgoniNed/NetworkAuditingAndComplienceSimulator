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
                cases = await Http.GetFromJsonAsync<List<DisciplinaryCase>>($"{humanresourceBaseUrl}/api/HumanResource/GetDisciplinaryCase");
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

        private void AddCase(DisciplinaryCase newCase)
        {
            cases.Add(newCase);
            HideCreateForm();
        }

        private void UpdateCase(DisciplinaryCase updatedCase)
        {
            var existingCase = cases.FirstOrDefault(e => e.CaseId == updatedCase.CaseId);
            if (existingCase != null)
            {
                existingCase.EmployeeName = updatedCase.EmployeeName;
                existingCase.Date = updatedCase.Date;
                existingCase.Reason = updatedCase.Reason;
            }
            HideEditForm();
        }

        private void DeleteCase(DisciplinaryCase caseToDelete)
        {
            cases.Remove(caseToDelete);
        }
    }
}
