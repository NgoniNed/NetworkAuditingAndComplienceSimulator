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
            financeBaseUrl = "https://localhost:42442";
            Console.WriteLine($"Pushing AddPnL to Finance API");
            try
            {

                Console.WriteLine($"Trying to Push AddPnL to Finance API");
                using (HttpClient client = new HttpClient())
                {
                    var response = await client.PostAsJsonAsync<PnL>($"{financeBaseUrl}/api/Finance/PushPnL", newPnL);

                    Console.WriteLine(response.ReasonPhrase);
                    Console.WriteLine(response.StatusCode);
                    Console.WriteLine(response.RequestMessage);
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error adding {newPnL.GetType().Name}: {ex.Message}");
            }
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
            financeBaseUrl = "https://localhost:42442";
            Console.WriteLine($"Pushing UpdatePnL to Finance API");
            try
            {

                Console.WriteLine($"Trying to Push UpdatePnL to Finance API");
                using (HttpClient client = new HttpClient())
                {
                    var response = await client.PostAsJsonAsync<PnL>($"{financeBaseUrl}/api/Finance/UpdatePnL", editingPnL);

                    Console.WriteLine(response.ReasonPhrase);
                    Console.WriteLine(response.StatusCode);
                    Console.WriteLine(response.RequestMessage);
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error updating {editingPnL.GetType().Name}: {ex.Message}");
            }
            HideEditForm();
        }

        private void ConfirmDelete(PnL entry)
        {
            deletingPnL = entry;
            showDeleteConfirm = true;
        }

        private async Task DeletePnLConfirmed()
        {
            financeBaseUrl = "https://localhost:42442";
            Console.WriteLine($"Pushing DeletePnLConfirmed to Finance API");
            try
            {

                Console.WriteLine($"Trying to Push DeletePnLConfirmed to Finance API");
                using (HttpClient client = new HttpClient())
                {
                    var response = await client.PostAsJsonAsync<PnL>($"{financeBaseUrl}/api/Finance/DeleteTaxItem", deletingPnL);

                    Console.WriteLine(response.ReasonPhrase);
                    Console.WriteLine(response.StatusCode);
                    Console.WriteLine(response.RequestMessage);
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error deleting {deletingPnL.GetType().Name}: {ex.Message}");
            }
            pnl.Remove(deletingPnL);
            showDeleteConfirm = false;
        }

    }
}
