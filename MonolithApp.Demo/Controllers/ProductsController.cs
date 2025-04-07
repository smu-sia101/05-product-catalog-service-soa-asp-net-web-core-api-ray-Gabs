using Microsoft.AspNetCore.Mvc;
using MonolithApp.Demo.Models;
using ProductsBLL;
using System.Collections.Generic;
using System.Linq;

namespace MonolithApp.Demo.Controllers
{
    public class ProductsController : Controller
    {
        private readonly IProductsService _productsService;

        public ProductsController(IProductsService productsService)
        {
            _productsService = productsService;
        }

        // Index action: Displays all products
        public IActionResult Index()
        {
            ViewBag.ProductId = TempData["ProductId"]?.ToString();

            // Fetch products from the service and map them to the Product view model
            IEnumerable<ProductDTO> productDtos = _productsService.Get();
            List<Product> products = productDtos.Select(p => new Product
            {
                Id = p.Id,
                Name = p.Name,
                Type = p.Type,
                Manufacturer = p.Manufacturer
            }).ToList();

            return View(products);
        }

        // Details action: Fetch a single product by id and display details
        [HttpGet("Products/{id}")]
        public IActionResult Details([FromRoute] string id)
        {
            try
            {
                ProductDTO productDto = _productsService.Get(id);

                if (productDto == null)
                {
                    return NotFound("Product not found.");
                }

                Product product = new Product
                {
                    Id = productDto.Id,
                    Name = productDto.Name,
                    Type = productDto.Type,
                    Manufacturer = productDto.Manufacturer
                };

                return View(product);
            }
            catch (Exception ex)
            {
                // Log exception here if needed
                return BadRequest($"Error retrieving product: {ex.Message}");
            }
        }

        // SubmitProduct action: Handles creating a new product
        [HttpPost]
        public IActionResult SubmitProduct(Product product)
        {
            if (!ModelState.IsValid)
            {
                return View(product);
            }

            // Map the Product view model to ProductDTO and add it via the service
            ProductDTO mappedProductDto = new ProductDTO
            {
                Id = product.Id,
                Name = product.Name,
                Type = product.Type,
                Manufacturer = product.Manufacturer
            };

            _productsService.Add(mappedProductDto);

            // Save the ID of the created product into TempData (MongoDB ObjectId)
            TempData["ProductId"] = mappedProductDto.Id;

            return RedirectToAction("Index");
        }

        // Edit action (GET): Fetch the product and show it for editing
        [HttpGet]
        public IActionResult Edit(string id)
        {
            ProductDTO productDto = _productsService.Get(id);

            if (productDto == null)
            {
                return NotFound("Product not found.");
            }

            Product product = new Product
            {
                Id = productDto.Id,
                Name = productDto.Name,
                Type = productDto.Type,
                Manufacturer = productDto.Manufacturer
            };

            return View(product);
        }

        // Edit action (POST): Handle product update
        [HttpPost]
        public IActionResult Edit(Product product)
        {
            if (!ModelState.IsValid)
            {
                return View(product);
            }

            // Map the updated Product view model to ProductDTO
            ProductDTO updatedProductDto = new ProductDTO
            {
                Id = product.Id,
                Name = product.Name,
                Type = product.Type,
                Manufacturer = product.Manufacturer
            };

            _productsService.Update(updatedProductDto);

            return RedirectToAction("Index");
        }

        // Delete action: Deletes a product by ID
        [HttpPost]
        public IActionResult Delete(string id)
        {
            _productsService.Delete(id);
            return RedirectToAction("Index");
        }
    }
}
