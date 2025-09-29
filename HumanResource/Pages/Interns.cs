using System;
using System.Collections.Generic;
using System.Linq;
using SharedLibrary.Data.HumanResource;

namespace HumanResource.Pages
{
    public partial class Interns
    {
        private List<Intern> interns = new List<Intern>();

        private bool showCreateForm = false;
        private bool showEditForm = false;
        private Intern selectedIntern;

        protected override void OnInitialized()
        {
            interns = GetInterns();
        }
        private List<Intern> GetInterns()
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
