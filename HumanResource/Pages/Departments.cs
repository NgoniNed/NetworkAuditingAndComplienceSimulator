using System;
using System.Collections.Generic;
using System.Linq;
using SharedLibrary.Data.HumanResource;

namespace HumanResource.Pages
{
    public partial class Departments
    {
        private List<Department> departments = new List<Department>();

        private bool showCreateForm = false;
        private bool showEditForm = false;
        private Department selectedDepartment;

        protected override void OnInitialized()
        {
            departments = GetDepartments();
        }
        private List<Department> GetDepartments()
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
