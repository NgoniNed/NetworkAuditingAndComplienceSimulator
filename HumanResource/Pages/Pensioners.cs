using System;
using System.Collections.Generic;
using System.Linq;
using SharedLibrary.Data.HumanResource;

namespace HumanResource.Pages
{
    public partial class Pensioners
    {
        private List<Pensioner> pensioners = new List<Pensioner>();

        private bool showCreateForm = false;
        private bool showEditForm = false;
        private Pensioner selectedPensioner;

        protected override void OnInitialized()
        {
            pensioners = GetPensioners();
        }
        private List<Pensioner> GetPensioners()
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

        private void AddPensioner(Pensioner newPensioner)
        {
            pensioners.Add(newPensioner);
            HideCreateForm();
        }

        private void UpdatePensioner(Pensioner updatedPensioner)
        {
            var existingPensioner = pensioners.FirstOrDefault(e => e.PensionerId == updatedPensioner.PensionerId);
            if (existingPensioner != null)
            {
                existingPensioner.Name = updatedPensioner.Name;
                existingPensioner.LastDepartment = updatedPensioner.LastDepartment;
                existingPensioner.LastPosition = updatedPensioner.LastPosition;
                existingPensioner.Email = updatedPensioner.Email;
            }
            HideEditForm();
        }

        private void DeletePensioner(Pensioner pensionerToDelete)
        {
            pensioners.Remove(pensionerToDelete);
        }
    }
}
