using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Configuration;

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
    }
}