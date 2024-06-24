using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.DurableTask;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;
using CLDV6211POE.Models;

namespace OrderManagementFunction
{
    public static class OrderManagementOrchestrator
    {
        [FunctionName("OrderManagementOrchestrator")]
        public static async Task RunOrchestrator(
            [OrchestrationTrigger] IDurableOrchestrationContext context)
        {
            var orders = context.GetInput<Orders>();

            await context.CallActivityAsync("PlaceOrder", orders);
            await context.CallActivityAsync("UpdateInventory", orders);
            await context.CallActivityAsync("SendOrderConfirmation", orders);
        }
    }
}