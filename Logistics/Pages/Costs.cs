using System;
using System.Collections.Generic;
using System.Linq;
using SharedLibrary.Data.Logistics;

namespace Logistics.Pages
{
    public partial class Costs
    {
        private List<CostLog> costs = new List<CostLog>();

        private bool showCreateForm = false;
        private bool showEditForm = false;
        private CostLog selectedCost;

        protected override void OnInitialized()
        {
            costs = GetCosts();
        }
        private List<CostLog> GetCosts()
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

        private void ShowEditForm(CostLog cost)
        {
            selectedCost = cost;
            showEditForm = true;
        }

        private void HideEditForm()
        {
            showEditForm = false;
            selectedCost = null;
        }

        private void AddCost(CostLog newCost)
        {
            costs.Add(newCost);
            HideCreateForm();
        }

        private void UpdateCost(CostLog updatedCost)
        {
            var existingCost = costs.FirstOrDefault(e => e.CostLogId == updatedCost.CostLogId);
            if (existingCost != null)
            {
                existingCost.Date = updatedCost.Date;
                existingCost.Description = updatedCost.Description;
                existingCost.Amount = updatedCost.Amount;
                existingCost.Category = updatedCost.Category;
            }
            HideEditForm();
        }

        private void DeleteCost(CostLog costToDelete)
        {
            costs.Remove(costToDelete);
        }
    }
}
