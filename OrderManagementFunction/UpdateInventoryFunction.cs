using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Microsoft.Azure.WebJobs.Extensions.DurableTask;
using CLDV6211POE.Models;

namespace OrderManagementFunction
{
    public static class UpdateInventoryFunction
    {
        [FunctionName("UpdateInventory")]
        public static async Task UpdateInventory(
            [ActivityTrigger] Orders orders, ILogger log)
        {
            log.LogInformation($"Updating inventory for {orders.ProductId}");
            await Task.CompletedTask;
        }
    }
}
