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
    public partial class BalanceSheet
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

        private List<SharedLibrary.Data.Finance.BalanceSheet> balancesheet = new List<SharedLibrary.Data.Finance.BalanceSheet>();
        private string financeBaseUrl;
        private SharedLibrary.Data.Finance.BalanceSheet newSheet = new();
        private SharedLibrary.Data.Finance.BalanceSheet editingSheet;
        private SharedLibrary.Data.Finance.BalanceSheet deletingSheet;
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
                balancesheet = await Http.GetFromJsonAsync<List<SharedLibrary.Data.Finance.BalanceSheet>>($"{financeBaseUrl}/api/Finance/GetBalanceSheet");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error fetching Balance Sheets: {ex.Message}");
            }
            //Console.WriteLine(balancesheet[0]);
        }
        private void ShowCreateForm()
        {
            showCreateForm = true;
            newSheet = new SharedLibrary.Data.Finance.BalanceSheet();
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

        private async Task AddBalanceSheet()
        {
            balancesheet.Add(new SharedLibrary.Data.Finance.BalanceSheet());
            HideCreateForm();
        }

        private void ShowEditForm(SharedLibrary.Data.Finance.BalanceSheet sheet)
        {
            editingSheet = new SharedLibrary.Data.Finance.BalanceSheet
            {
                Id = sheet.Id,
                Assets = sheet.Assets,
                Liabilities = sheet.Liabilities
            };
            showEditForm = true;
        }

        private async Task UpdateBalanceSheet()
        {
            var idx = balancesheet.FindIndex(b => b.Id == editingSheet.Id);
            if (idx >= 0) balancesheet[idx] = editingSheet;
            HideEditForm();
        }

        private void ConfirmDelete(SharedLibrary.Data.Finance.BalanceSheet sheet)
        {
            deletingSheet = sheet;
            showDeleteConfirm = true;
        }

        private async Task DeleteBalanceSheetConfirmed()
        {
            balancesheet.Remove(deletingSheet);
            showDeleteConfirm = false;
        }

    }
}