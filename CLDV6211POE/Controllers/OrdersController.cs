using CLDV6211POE.Data;
using CLDV6211POE.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using Newtonsoft.Json;
using System.Text;

namespace CLDV6211POE.Controllers
{
    //this controller approves or denies orders on the html
    public class OrdersController : Controller
    {
        private readonly ApplicationDbContext context;
        private readonly IWebHostEnvironment environment;
        private readonly HttpClient httpClient;

        public OrdersController(ApplicationDbContext context, IWebHostEnvironment environment , HttpClient httpClient)
        {
            this.context = context;
            this.environment = environment;
            this.httpClient = httpClient;
        }

        public IActionResult Index()
        {
            var orders = context.Orders.ToList();
            return View(orders);
        }

        public IActionResult Approve(int orderId, int productId)
        {
            var order = context.Orders.Find(orderId);
            if (order == null)
            {
                return RedirectToAction("Index", "Orders");
            }

            var product = context.Products.Find(productId);
            var previousOrder = context.PreviousOrders.FirstOrDefault(po => po.Id == order.OrderId);
            if (product == null)
            {
                return RedirectToAction("Error", "Orders");
            }
            else
            {
                if (product.ProductAvailability >= order.Quantity)
                {
                    product.ProductAvailability -= order.Quantity;
                    PlaceOrder(order);
                    order.Status = "Approved";
                    if (previousOrder == null)
                    {
                        previousOrder = new PreviousOrders
                        {
                            Status = "Approved"
                        };
                        context.PreviousOrders.Add(previousOrder);
                    }
                    else
                    {
                        previousOrder.Status = "Approved";
                    }
                }
                else
                {
                    return RedirectToAction("Error", "Orders");
                }
            }
            context.SaveChanges(true);
            return RedirectToAction("Index", "Orders");
        }

        [HttpPost]
        [Route("PlaceOrder")]
        public async Task<IActionResult> PlaceOrder(Orders orders)
        {
            var content = new StringContent(JsonConvert.SerializeObject(orders), Encoding.UTF8, "application/json");
            var response = await httpClient.PostAsync("https://cldv6211poe-ordermanagementfunction.azurewebsites.net/api/orchestrators/OrderManagementOrchestrator", content);

            if (response.IsSuccessStatusCode)
            {
                var instanceId = await response.Content.ReadAsStringAsync();
                return Ok(new { InstanceId = instanceId });
            }

            return StatusCode((int)response.StatusCode, response.ReasonPhrase);
        }


        public IActionResult Deny(int orderId, int productId)
        {
            var order = context.Orders.Find(orderId);

            if (order == null)
            {
                return RedirectToAction("Error", "Orders");
            }

            var product = context.Products.Find(productId);
            if (product == null)
            {
                return RedirectToAction("Error", "Orders");
            }
            product.ProductAvailability += order.Quantity;


            var previousOrder = context.PreviousOrders.FirstOrDefault(po => po.Id == order.OrderId);
            if (previousOrder == null)
            {
                previousOrder = new PreviousOrders
                {
                    Status = "Denied"
                };
                context.PreviousOrders.Add(previousOrder);
            }
            else
            {
                previousOrder.Status = "Denied";
            }

            order.Status = "Denied";
            context.SaveChanges(true);

            return RedirectToAction(nameof(Index));
        }

        public IActionResult Error()
        {
            return View();
        }
    }
}
