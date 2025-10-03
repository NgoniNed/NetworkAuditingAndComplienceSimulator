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
            financeBaseUrl = "https://localhost:42442";
            Console.WriteLine($"Pushing AddTaxItem to AddAsset API");
            try
            {

                Console.WriteLine($"Trying to Push AddTaxItem to AddAsset API");
                using (HttpClient client = new HttpClient())
                {
                    var response = await client.PostAsJsonAsync<TaxItem>($"{financeBaseUrl}/api/Finance/PushTaxItem", newTaxItem);

                    Console.WriteLine(response.ReasonPhrase);
                    Console.WriteLine(response.StatusCode);
                    Console.WriteLine(response.RequestMessage);
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error adding {newTaxItem.GetType().Name}: {ex.Message}");
            }
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
            financeBaseUrl = "https://localhost:42442";
            Console.WriteLine($"Pushing UpdateTaxItem to Finance API");
            try
            {

                Console.WriteLine($"Trying to Push UpdateTaxItem to Finance API");
                using (HttpClient client = new HttpClient())
                {
                    var response = await client.PostAsJsonAsync<TaxItem>($"{financeBaseUrl}/api/Finance/UpdateTaxItem", editingTaxItem);

                    Console.WriteLine(response.ReasonPhrase);
                    Console.WriteLine(response.StatusCode);
                    Console.WriteLine(response.RequestMessage);
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error updating {editingTaxItem.GetType().Name}: {ex.Message}");
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
            financeBaseUrl = "https://localhost:42442";
            Console.WriteLine($"Pushing DeleteTaxItemConfirmed to Finance API");
            try
            {

                Console.WriteLine($"Trying to Push DeleteTaxItemConfirmed to Finance API");
                using (HttpClient client = new HttpClient())
                {
                    var response = await client.PostAsJsonAsync<TaxItem>($"{financeBaseUrl}/api/Finance/DeleteTaxItem", deletingTaxItem);

                    Console.WriteLine(response.ReasonPhrase);
                    Console.WriteLine(response.StatusCode);
                    Console.WriteLine(response.RequestMessage);
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error deleting {deletingTaxItem.GetType().Name}: {ex.Message}");
            }
            taxItems.Remove(deletingTaxItem);
            showDeleteConfirm = false;
        }
    }
}
