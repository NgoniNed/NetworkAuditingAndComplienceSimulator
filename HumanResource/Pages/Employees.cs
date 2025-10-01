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
    public partial class Employees
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
        private List<Employee> employees = new List<Employee>();

        private bool showCreateForm = false;
        private bool showEditForm = false;
        private Employee selectedEmployee;

        protected override async Task OnInitializedAsync()
        {
            var humanresourceBaseUrl = Configuration["CentralServerBaseUrl"];
            humanresourceBaseUrl = "https://localhost:27078";
            try
            {
                employees = await Http.GetFromJsonAsync<List<Employee>>($"{humanresourceBaseUrl}/api/HumanResources/GetEmployee");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error fetching Departments: {ex.Message}");
            }
        }
        private List<Employee> GetEmployees()
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

        private void ShowEditForm(Employee employee)
        {
            selectedEmployee = employee;
            showEditForm = true;
        }

        private void HideEditForm()
        {
            showEditForm = false;
            selectedEmployee = null;
        }

        private async void AddEmployee(Employee newEmployee)
        {
            employees.Add(newEmployee);
            var humanresourcesourceBaseUrl = Configuration["CentralServerBaseUrl"];
            humanresourcesourceBaseUrl = "https://localhost:57238";
            Console.WriteLine($"Pushing to Human Resource API");
            try
            {

                Console.WriteLine($"Trying to Push to Human Resource API");
                using (HttpClient client = new HttpClient())
                {
                    var response = await client.PostAsJsonAsync<Employee>($"{humanresourcesourceBaseUrl}/api/HumanResources/PushEmployee", newEmployee);

                    Console.WriteLine(response.ReasonPhrase);
                    Console.WriteLine(response.StatusCode);
                    Console.WriteLine(response.RequestMessage);
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error posting {newEmployee.GetType().Name}: {ex.Message}");
            }
            HideCreateForm();
        }

        private void UpdateEmployee(Employee updatedEmployee)
        {
            var existingEmployee = employees.FirstOrDefault(e => e.EmployeeId == updatedEmployee.EmployeeId);
            if (existingEmployee != null)
            {
                existingEmployee.Name = updatedEmployee.Name;
                existingEmployee.Department = updatedEmployee.Department;
                existingEmployee.Position = updatedEmployee.Position;
                existingEmployee.Email = updatedEmployee.Email;
            }
            HideEditForm();
        }

        private void DeleteEmployee(Employee employeeToDelete)
        {
            employees.Remove(employeeToDelete);
        }

    }
}
