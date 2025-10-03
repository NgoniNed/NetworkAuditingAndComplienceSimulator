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
                departments = await Http.GetFromJsonAsync<List<Department>>($"{humanresourceBaseUrl}/api/HumanResources/GetDepartment");
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

        private async void AddDepartment(Department newDepartment)
        {
            departments.Add(newDepartment);
            var humanresourcesourceBaseUrl = Configuration["CentralServerBaseUrl"];
            humanresourcesourceBaseUrl = "https://localhost:57238";
            Console.WriteLine($"Pushing to Human Resource API");
            try
            {

                Console.WriteLine($"Trying to Push to Human Resource API");
                using (HttpClient client = new HttpClient())
                {
                    var response = await client.PostAsJsonAsync<Department>($"{humanresourcesourceBaseUrl}/api/HumanResources/PushDepartment", newDepartment);

                    Console.WriteLine(response.ReasonPhrase);
                    Console.WriteLine(response.StatusCode);
                    Console.WriteLine(response.RequestMessage);
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error posting {newDepartment.GetType().Name}: {ex.Message}");
            }
            HideCreateForm();
        }

        private async Task UpdateDepartmentAsync(Department updatedDepartment)
        {
            var existingDepartment = departments.FirstOrDefault(e => e.DepartmentId == updatedDepartment.DepartmentId);
            if (existingDepartment != null)
            {
                existingDepartment.Name = updatedDepartment.Name;
                existingDepartment.Description = updatedDepartment.Description;
            }
            var humanresourcesourceBaseUrl = Configuration["CentralServerBaseUrl"];
            humanresourcesourceBaseUrl = "https://localhost:57238";
            Console.WriteLine($"Pushing UpdateDepartment to Human Resource API");
            try
            {

                Console.WriteLine($"Trying to Push UpdateDepartment to Human Resource API");
                using (HttpClient client = new HttpClient())
                {
                    var response = await client.PostAsJsonAsync<Department>($"{humanresourcesourceBaseUrl}/api/HumanResources/UpdateDepartment", updatedDepartment);

                    Console.WriteLine(response.ReasonPhrase);
                    Console.WriteLine(response.StatusCode);
                    Console.WriteLine(response.RequestMessage);
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error updating {updatedDepartment.GetType().Name}: {ex.Message}");
            }
            HideEditForm();
        }

        private async Task DeleteDepartmentAsync(Department departmentToDelete)
        {
            var humanresourcesourceBaseUrl = Configuration["CentralServerBaseUrl"];
            humanresourcesourceBaseUrl = "https://localhost:57238";
            Console.WriteLine($"Pushing DeleteDepartment to Human Resource API");
            try
            {

                Console.WriteLine($"Trying to Push DeleteDepartment to Human Resource API");
                using (HttpClient client = new HttpClient())
                {
                    var response = await client.PostAsJsonAsync<Department>($"{humanresourcesourceBaseUrl}/api/HumanResources/DeleteDepartment", departmentToDelete);

                    Console.WriteLine(response.ReasonPhrase);
                    Console.WriteLine(response.StatusCode);
                    Console.WriteLine(response.RequestMessage);
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error deleting {departmentToDelete.GetType().Name}: {ex.Message}");
            }
            departments.Remove(departmentToDelete);
        }

    }
}
