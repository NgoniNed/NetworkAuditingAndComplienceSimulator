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
    public partial class AssetManager
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

        private List<Asset> assets =new List<Asset>();
        private string financeBaseUrl;
        private Asset newAsset = new();
        private Asset editingAsset;
        private Asset deletingAsset;
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
                assets = await Http.GetFromJsonAsync<List<Asset>>($"{financeBaseUrl}/api/Finance/GetAsset");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error fetching assets: {ex.Message}");
            }
        }
        private void ShowCreateForm()
        {
            showCreateForm = true;
            newAsset = new Asset();
        }
        private void HideCreateForm()
        {
            showCreateForm = false;
        }

        private void HideEditForm()
        {
            showEditForm = false;
        }

        private async Task AddAsset()
        {
            assets.Add(newAsset);
            var humanresourcesourceBaseUrl = Configuration["CentralServerBaseUrl"];
            humanresourcesourceBaseUrl = "https://localhost:57238";
            Console.WriteLine($"Pushing AddAsset to Finance API");
            try
            {

                Console.WriteLine($"Trying to Push AddAsset to Finance API");
                using (HttpClient client = new HttpClient())
                {
                    var response = await client.PostAsJsonAsync<Asset>($"{humanresourcesourceBaseUrl}/api/Finance/PushAsset", newAsset);

                    Console.WriteLine(response.ReasonPhrase);
                    Console.WriteLine(response.StatusCode);
                    Console.WriteLine(response.RequestMessage);
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error adding {newAsset.GetType().Name}: {ex.Message}");
            }
            HideCreateForm();
        }
        private void ShowEditForm(Asset asset)
        {
            editingAsset = new Asset
            {
                Id = asset.Id,
                Name = asset.Name,
                AcquisitionDate = asset.AcquisitionDate,
                Cost = asset.Cost,
                RunningCosts = asset.RunningCosts,
                MaintenanceCosts = asset.MaintenanceCosts,
                Depreciation = asset.Depreciation,
                ProfitLoss = asset.ProfitLoss
                
            };
            showEditForm = true;
        }

        private async Task UpdateAsset()
        {
            var idx = assets.FindIndex(a => a.Id == editingAsset.Id);
            if (idx >= 0)
            {
                assets[idx] = editingAsset;
            }
            var humanresourcesourceBaseUrl = Configuration["CentralServerBaseUrl"];
            humanresourcesourceBaseUrl = "https://localhost:57238";
            Console.WriteLine($"Pushing UpdateAsset to Finance API");
            try
            {

                Console.WriteLine($"Trying to Push UpdateAsset to Finance API");
                using (HttpClient client = new HttpClient())
                {
                    var response = await client.PostAsJsonAsync<Asset>($"{humanresourcesourceBaseUrl}/api/Finance/UpdateAsset", editingAsset);

                    Console.WriteLine(response.ReasonPhrase);
                    Console.WriteLine(response.StatusCode);
                    Console.WriteLine(response.RequestMessage);
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error updating {editingAsset.GetType().Name}: {ex.Message}");
            }
            HideEditForm();
        }

        private void ConfirmDelete(Asset asset)
        {
            deletingAsset = asset;
            showDeleteConfirm = true;
        }

        private void HideDeleteConfirm()
        {
            showDeleteConfirm = false;
        }

        private async Task DeleteAssetConfirmed()
        {
            var humanresourcesourceBaseUrl = Configuration["CentralServerBaseUrl"];
            humanresourcesourceBaseUrl = "https://localhost:57238";
            Console.WriteLine($"Pushing DeleteAssetConfirmed to Finance API");
            try
            {

                Console.WriteLine($"Trying to Push DeleteAssetConfirmed to Finance API");
                using (HttpClient client = new HttpClient())
                {
                    var response = await client.PostAsJsonAsync<Asset>($"{humanresourcesourceBaseUrl}/api/Finance/DeleteAsset", deletingAsset);

                    Console.WriteLine(response.ReasonPhrase);
                    Console.WriteLine(response.StatusCode);
                    Console.WriteLine(response.RequestMessage);
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error deleting {deletingAsset.GetType().Name}: {ex.Message}");
            }
            assets.Remove(deletingAsset);
            showDeleteConfirm = false;
        }
    }
}
