// Default URL for triggering event grid function in the local environment.
// http://localhost:7071/runtime/webhooks/EventGrid?functionName={functionname}
using System;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.EventGrid;
using Microsoft.Extensions.Logging;
using Azure.Messaging.EventGrid;
using Microsoft.Azure.WebJobs.Extensions.DurableTask;
using CLDV6211POE.Models;
using Newtonsoft.Json.Linq;
using System.Threading.Tasks;

namespace OrderManagementFunction
{
    public static class SendEmailConfirmation
    {
        [FunctionName("SendEmailConfirmation")]
        public static async Task Run([EventGridTrigger]EventGridEvent eventGridEvent, Orders orders, ILogger log)
        {
            var orderDetails = eventGridEvent.Data.ToObjectFromJson<Orders>();
            log.LogInformation($"Confirmation of order {orders.OrderId}, Email {orders.UserId}");

            await Task.CompletedTask;
        }
    }
}
