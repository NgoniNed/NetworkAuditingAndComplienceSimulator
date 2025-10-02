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
    public partial class TaxManager
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

        private List<TaxItem> taxItems = new List<TaxItem>();
        private string financeBaseUrl;
        private TaxItem newTaxItem = new();
        private TaxItem editingTaxItem;
        private TaxItem deletingTaxItem;
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
                taxItems = await Http.GetFromJsonAsync<List<TaxItem>>($"{financeBaseUrl}/api/Finance/GetTaxItem");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error fetching Balance Sheets: {ex.Message}");
            }
        }
        private void ShowCreateForm()
        {
            showCreateForm = true;
            newTaxItem = new TaxItem();
        }
        private void HideCreateForm()
        {
            showCreateForm = false;
        }

        private void HideEditForm()
        {
            showEditForm = false;
        }

        private async Task AddTaxItem()
        {
            taxItems.Add(newTaxItem);
            HideCreateForm();
        }
        private void ShowEditForm(TaxItem taxItem)
        {
            editingTaxItem = new TaxItem
            {
                Id = taxItem.Id,
                Name = taxItem.Name

            };
            showEditForm = true;
        }

        private async Task UpdateTaxItem()
        {
            var idx = taxItems.FindIndex(a => a.Id == editingTaxItem.Id);
            if (idx >= 0)
            {
                taxItems[idx] = editingTaxItem;
            }
            HideEditForm();
        }

        private void ConfirmDelete(TaxItem taxItem)
        {
            deletingTaxItem = taxItem;
            showDeleteConfirm = true;
        }

        private void HideDeleteConfirm()
        {
            showDeleteConfirm = false;
        }

        private async Task DeleteTaxItemConfirmed()
        {
            taxItems.Remove(deletingTaxItem);
            showDeleteConfirm = false;
        }
    }
}
