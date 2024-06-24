using CLDV6211POE.Data;
using Microsoft.AspNetCore.Mvc;

namespace CLDV6211POE.Controllers
{
    //this controller get previous order history
    public class PreviousOrdersController : Controller
    {
        private readonly ApplicationDbContext context;
        private readonly IWebHostEnvironment environment;

        public PreviousOrdersController(ApplicationDbContext context, IWebHostEnvironment environment)
        {
            this.context = context;
            this.environment = environment;
        }

        public IActionResult ViewOrderHistory()
        {
            var orders = context.PreviousOrders.ToList();
            return View(orders);
        }
    }
}
