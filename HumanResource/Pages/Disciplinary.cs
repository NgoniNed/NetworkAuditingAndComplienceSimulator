using System;
using System.Collections.Generic;
using System.Linq;
using SharedLibrary.Data.HumanResource;

namespace HumanResource.Pages
{
    public partial class Disciplinary
    {
        private List<DisciplinaryCase> cases = new List<DisciplinaryCase>();

        private bool showCreateForm = false;
        private bool showEditForm = false;
        private DisciplinaryCase selectedCase;

        protected override void OnInitialized()
        {
            cases = GetCases();
        }
        private List<DisciplinaryCase> GetCases()
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
