using System;
using System.Collections.Generic;
using System.Linq;
using SharedLibrary.Data.Logistics;

using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Configuration;
using System.Text;
using System.Text.Json;

namespace Logistics.Pages
{
    public partial class Costs
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

        private List<CostLog> costs = new List<CostLog>();

        private bool showCreateForm = false;
        private bool showEditForm = false;
        private CostLog selectedCost;

        
        protected override async Task OnInitializedAsync()
        {
            var logisticssourceBaseUrl = Configuration["CentralServerBaseUrl"];
            logisticssourceBaseUrl = "https://localhost:57238";
            try
            {
                costs = await Http.GetFromJsonAsync<List<CostLog>>($"{logisticssourceBaseUrl}/api/Logistics/GetCostLog");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error fetching Costs Logs: {ex.Message}");
            }
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

        private async Task AddCostAsync(CostLog newCost)
        {
            costs.Add(newCost);
            //API push request
            var logisticssourceBaseUrl = Configuration["CentralServerBaseUrl"];
            logisticssourceBaseUrl = "https://localhost:57238";
            Console.WriteLine($"Pushing to Logistics API");
            try
            {
                
                Console.WriteLine($"Trying to Push to Logistics API");
                using (HttpClient client = new HttpClient())
                {
                    var response = await client.PostAsJsonAsync<CostLog>($"{logisticssourceBaseUrl}/api/Logistics/PushCostLog", newCost);

                    Console.WriteLine(response.ReasonPhrase);
                    Console.WriteLine(response.StatusCode);
                    Console.WriteLine(response.RequestMessage);
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error posting {newCost.GetType().Name}: {ex.Message}");
            }
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
