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
    public partial class Departments
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
        private List<Department> departments = new List<Department>();

        private bool showCreateForm = false;
        private bool showEditForm = false;
        private Department selectedDepartment;

        protected override async Task OnInitializedAsync()
        {
            var humanresourceBaseUrl = Configuration["CentralServerBaseUrl"];
            humanresourceBaseUrl = "https://localhost:27078";
            try
            {
                departments = await Http.GetFromJsonAsync<List<Department>>($"{humanresourceBaseUrl}/api/HumanResource/GetDepartment");
            }
            catch(Exception ex)
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

        private void ShowEditForm(Department department)
        {
            selectedDepartment = department;
            showEditForm = true;
        }

        private void HideEditForm()
        {
            showEditForm = false;
            selectedDepartment = null;
        }

        private void AddDepartment(Department newDepartment)
        {
            departments.Add(newDepartment);
            HideCreateForm();
        }

        private void UpdateDepartment(Department updatedDepartment)
        {
            var existingDepartment = departments.FirstOrDefault(e => e.DepartmentId == updatedDepartment.DepartmentId);
            if (existingDepartment != null)
            {
                existingDepartment.Name = updatedDepartment.Name;
                existingDepartment.Description = updatedDepartment.Description;
            }
            HideEditForm();
        }

        private void DeleteDepartment(Department departmentToDelete)
        {
            departments.Remove(departmentToDelete);
        }

    }
}
