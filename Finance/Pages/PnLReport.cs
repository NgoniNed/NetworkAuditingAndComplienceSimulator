using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Configuration;
using SharedLibrary.Data.Finance;

namespace Finance.Pages
{
    public partial class PnLReport
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

        private List<PnL> pnl = new List<PnL>();
        private string financeBaseUrl;
        private PnL newPnL = new();
        private PnL editingPnL;
        private PnL deletingPnL;
        private bool showCreateForm = false;
        private bool showEditForm = false;
        private bool showDeleteConfirm = false;

        protected override async Task OnInitializedAsync()
        {

            financeBaseUrl = Configuration["CentralServerBaseUrl"];
            Console.WriteLine($"Finance is on: {Configuration["Finance:applicationUrl"]}");
            financeBaseUrl = "https://localhost:42442";
            try
            {
                pnl = await Http.GetFromJsonAsync<List<PnL>>($"{financeBaseUrl}/api/Finance/GetPnL");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error fetching Balance Sheets: {ex.Message}");
            }
        }

        private void ShowCreateForm()
        {
            showCreateForm = true;
            newPnL = new PnL();
        }
        private void HideCreateForm()
        {
            showCreateForm = false;
        }

        private void HideEditForm()
        {
            showEditForm = false;
        }
        private void HideDeleteConfirm()
        {
            showDeleteConfirm = false;
        }

        private async Task AddPnL()
        {
            pnl.Add(newPnL);
            HideCreateForm();
        }

        private void ShowEditForm(PnL entry)
        {
            editingPnL = new PnL
            {
                Id = entry.Id,
                Revenue = entry.Revenue,
                Expenses = entry.Expenses
            };
            showEditForm = true;
        }

        private async Task UpdatePnL()
        {
            var idx = pnl.FindIndex(e => e.Id == editingPnL.Id);
            if (idx >= 0) pnl[idx] = editingPnL;
            HideEditForm();
        }

        private void ConfirmDelete(PnL entry)
        {
            deletingPnL = entry;
            showDeleteConfirm = true;
        }

        private async Task DeletePnLConfirmed()
        {
            pnl.Remove(deletingPnL);
            showDeleteConfirm = false;
        }

    }
}
