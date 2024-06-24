using Microsoft.AspNetCore.Mvc;
using CLDV6211POE.Data;
using CLDV6211POE.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using System.Text;

namespace CLDV6211POE.Controllers
{
    //the shop controller for the customer
    [Authorize(Roles = "Customer")]
    public class MyWorkController : Controller
    {
        private readonly ApplicationDbContext context;
        private readonly IWebHostEnvironment environment;

        public MyWorkController(ApplicationDbContext context, IWebHostEnvironment environment)
        {
            this.context = context;
            this.environment = environment;
        }

        public IActionResult Index()
        {
            var products = context.Products.ToList();
            return View(products);
        }

        public IActionResult Details(int id)
        {
            var product = context.Products.Find(id);
            if (product == null)
            {
                return NotFound();
            }
            return View(product);
        }
    }
}
