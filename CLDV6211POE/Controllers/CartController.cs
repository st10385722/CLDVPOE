using CLDV6211POE.Data;
using CLDV6211POE.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace CLDV6211POE.Controllers
{
    //this controller is used by a user with a role customer
    // it is unique to every user, as in someone cannot see the cart of this user while logged in to another user
    //this is done using HttpContext session, to get and set the Json data
    [Authorize(Roles ="Customer")]
    public class CartController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly ApplicationDbContext _context;
        private List<ShoppingCart> _items;

        public CartController(ApplicationDbContext context, UserManager<IdentityUser> userManager)
        {
            _userManager = userManager;
            _context = context;
            _items = new List<ShoppingCart>();
        }

        //add the item to a cart, 1 at a time so that it can be approved seperately
        public IActionResult AddToCart(int id)
        {
            var productToAdd = _context.Products.Find(id);

            var items = HttpContext.Session.Get<List<ShoppingCart>>("Cart") ?? new List<ShoppingCart>();

            var existingItem = items.FirstOrDefault(item => item.Products.ProductId == id);

            if (existingItem != null)
            {
                existingItem.Quantity++;
            }
            else
            {
                items.Add(new ShoppingCart
                {
                    Products = productToAdd,
                    Quantity = 1
                });
            }

            HttpContext.Session.Set("Cart", items);

            //reading
            TempData["CartMessage"] = $"{productToAdd.ProductName} has been added to the cart";
            return RedirectToAction("ViewCart");
        }

        //[HttpGet]
        //viewing the card using HttpContext, and also a temp message depending on the action of adding or removing from cart
        public IActionResult ViewCart()
        {
            var items = HttpContext.Session.Get<List<ShoppingCart>>("Cart") ?? new List<ShoppingCart>();

            var cartViewModel = new ShoppingCartViewModel
            {
                CartItems = items,
                TotalPrice = items.Sum(item => item.Products.ProductPrice * item.Quantity)
            };

            ViewBag.CartMessage = TempData["CartMessage"]; //temp data writing
            return View(cartViewModel);
        }

        public IActionResult RemoveItem(int id)
        {
            var items = HttpContext.Session.Get<List<ShoppingCart>>("Cart") ?? new List<ShoppingCart>();
            var itemToRemove = items.FirstOrDefault(item => item.Products.ProductId == id);

            TempData["CartMessage"] = $"{itemToRemove.Products.ProductName} Removed From Cart";

            if (itemToRemove != null) 
            {
                if (itemToRemove.Quantity > 1)
                {
                    itemToRemove.Quantity--;
                }
                else
                {
                    items.Remove(itemToRemove);
                }
            }

            HttpContext.Session.Set("Cart", items);

            return RedirectToAction("ViewCart");
        }

        //order item button, that sends the info to both the order table for the admin, as well as the previousorder table for the user
        public IActionResult OrderItems()
        {
            var items = HttpContext.Session.Get<List<ShoppingCart>>("Cart") ?? new List<ShoppingCart>();
            var username = User.Identity.Name;

            foreach( var item in items)
            {
                //save each item
                _context.Orders.Add(new Orders
                {
                    UserId = username,
                    ProductId = item.Products.ProductId,
                    Quantity = item.Quantity,
                    OrderPurchaseDate = DateTime.Now,
                    Total = item.Products.ProductPrice * item.Quantity,
                    //set to pending, and is updated whether its approved or denied
                    Status = "Pending"
                });
                //saves this to a previous orders so that it can be read by the previous orders page
                _context.PreviousOrders.Add(new PreviousOrders
                {
                    UserId = username,
                    ProductId = item.Products.ProductId,
                    ProductName = item.Products.ProductName,
                    Quantity = item.Quantity,
                    OrderPurchaseDate = DateTime.Now,
                    Total = item.Products.ProductPrice * item.Quantity,
                    Status= "Pending"
                });
            }
            //save changes
            _context.SaveChanges();
            HttpContext.Session.Set("Cart", new List<ShoppingCart>());

            return RedirectToAction("Index", "Home");
        }

    }
}
