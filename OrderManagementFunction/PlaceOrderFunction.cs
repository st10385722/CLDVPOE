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
    public static class PlaceOrderFunction
    {
        [FunctionName("PlaceOrder")]
        public static async Task PlaceOrder(
            [ActivityTrigger] Orders order, ILogger log)
        {
            log.LogInformation($"Placing order for {order.OrderId}");
            await Task.CompletedTask;
        }
    }
}
