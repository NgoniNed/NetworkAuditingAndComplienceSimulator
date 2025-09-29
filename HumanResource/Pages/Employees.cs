using System;
using System.Collections.Generic;
using System.Linq;
using SharedLibrary.Data.HumanResource;

namespace HumanResource.Pages
{
    public partial class Employees
    {
        private List<Employee> employees = new List<Employee>();

        private bool showCreateForm = false;
        private bool showEditForm = false;
        private Employee selectedEmployee;

        protected override void OnInitialized()
        {
            employees = GetEmployees();
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

        private void AddEmployee(Employee newEmployee)
        {
            employees.Add(newEmployee);
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
