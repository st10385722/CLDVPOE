using CLDV6211POE.Data;
using CLDV6211POE.Models;
using Elfie.Serialization;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using Microsoft.VisualBasic.FileIO;
using System;
using System.IO;
using System.Linq;

namespace CLDV6211POE.Controllers
{
    //the database controller for the admin, having all of the crud for the admin
    [Authorize(Roles ="Admin")]
    public class ProductsController : Controller
    {
        private readonly ApplicationDbContext context;
        private readonly IWebHostEnvironment environment;

        public ProductsController(ApplicationDbContext context, IWebHostEnvironment environment)
        {
            this.context = context;
            this.environment = environment;
        }

        public IActionResult Index()
        {
            var products = context.Products.ToList();
            return View(products);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(ProductDto productDto)
        {
            if (productDto.ProductImageFile == null)
            {
                ModelState.AddModelError("ProductImageFile", "An image is required!");
            }

            if (!ModelState.IsValid)
            {
                return View(productDto);
            }

            string newFileName = DateTime.Now.ToString("yyyyMMddHHmmssfff");
            newFileName += Path.GetExtension(productDto.ProductImageFile!.FileName);

            string imageFullPath = environment.WebRootPath + "/images/products/" + newFileName;

            using (var stream = System.IO.File.Create(imageFullPath))
            {
                productDto.ProductImageFile.CopyTo(stream);
            }

            Products product = new Products()
            {
                ProductName = productDto.ProductName,
                ProductPrice = productDto.ProductPrice,
                ProductCategory = productDto.ProductCategory,
                ProductAvailability = productDto.ProductAvailability,
                ImageFileName = newFileName
            };

            context.Products.Add(product);
            context.SaveChanges();

            return RedirectToAction(nameof(Index));
        }

        public IActionResult Edit(int id)
        {
            var product = context.Products.Find(id);

            if (product == null)
            {
                return NotFound();
            }

            var productDto = new ProductDto()
            {
                ProductId = product.ProductId,
                ProductName = product.ProductName,
                ProductPrice = product.ProductPrice,
                ProductCategory = product.ProductCategory,
                ProductAvailability = product.ProductAvailability,
            };

            ViewData["ProductId"] = product.ProductId;
            ViewData["ImageFileName"] = product.ImageFileName;

            return View(productDto);
        }

        [HttpPost]
        public IActionResult Edit(int id, ProductDto productDto) 
        {
            var product = context.Products.Find(id);

            if(product == null) 
            {
                return RedirectToAction(nameof(Index));
            }
            if(!ModelState.IsValid) 
            {
                ViewData["ProductId"] = product.ProductId;
                ViewData["ImageFileName"] = product.ImageFileName;
                return View(productDto);
            }

            string newFile = product.ImageFileName;
            if(productDto.ProductImageFile != null) 
            {
                newFile = DateTime.Now.ToString("yyyyMMddHHmmssfff");
                newFile += Path.GetExtension(productDto.ProductImageFile.FileName);

                //string fullPath = environment.WebRootPath + "/images/products/" + newFile;
                string fullPath = Path.Combine(environment.WebRootPath, "images", "products", product.ImageFileName);

                using (var stream = System.IO.File.Create(fullPath)) 
                {
                    productDto.ProductImageFile.CopyTo(stream);
                }

                string toDeleteImage = environment.WebRootPath + "/images/products/" + product.ImageFileName;
                System.IO.File.Delete(toDeleteImage);
            }

            product.ProductName = productDto.ProductName;
            product.ProductPrice = productDto.ProductPrice;
            product.ProductCategory = productDto.ProductCategory;
            product.ProductAvailability = productDto.ProductAvailability;
            product.ImageFileName = newFile;

            context.SaveChanges();
            return RedirectToAction(nameof(Index));

        }

        public IActionResult Delete(int id)
        {
            var product = context.Products.Find(id);
            if(product == null) 
            {
                return RedirectToAction("Index", "Products");
            }
            //string fullPath = environment.WebRootPath + "/images/products/" + product.ImageFileName;
            string fullPath = Path.Combine(environment.WebRootPath, "images", "products", product.ImageFileName);
            try
            {
                System.IO.File.Delete(fullPath);
            }
            catch(UnauthorizedAccessException e)
            {
                ModelState.AddModelError("", "Unable to save image: " + e.Message);
                return RedirectToAction(nameof(Index));
            }

            context.Products.Remove(product);
            context.SaveChanges(true);

            return RedirectToAction(nameof(Index));
        }

        private bool ProductExists(int id)
        {
            return context.Products.Any(e => e.ProductId == id);
        }
    }
}
