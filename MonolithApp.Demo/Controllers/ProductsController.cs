using Microsoft.AspNetCore.Mvc;
using MonolithApp.Demo.Extensions;
using MonolithApp.Demo.Models;
using ProductsBLL;
using Model = MonolithApp.Demo.Models;

namespace MonolithApp.Demo.Controllers
{
    public class ProductsController : Controller
    {
        protected readonly IProductsService _productsService;
        public ProductsController(IProductsService productsService)
        {
            _productsService = productsService;
        }

        public IActionResult Index()
        {
            ViewBag.ProductId = TempData["ProductId"]?.ToString();

            IEnumerable<ProductDTO> products = _productsService.Get();

            List<Model.Product> mapped = products.MapProductsDALToProductsModel();

            return Ok(mapped);
        }

        [HttpGet("Products/{id}")]
        public IActionResult Details([FromRoute] int id)
        {
            try
            {
                ProductDTO product = _productsService.Get(id);
                Model.Product mapped = product.MapDtoToWebModel();

                return View(mapped);
            }
            catch (Exception)
            {
                return View();
            }
        }

        [HttpPost]
        public IActionResult SubmitProduct(Model.Product product)
        {
            ProductDTO mapped = product.MapWebModelToDto();

            _productsService.Add(mapped);

            TempData["ProductId"] = mapped.Id;

            return RedirectToAction("Index");
        }
        [HttpGet]
        public IActionResult Edit(int id)
        {
            var productDto = _productsService.Get(id);
            var product = new Product
            {
                Id = productDto.Id,
                Name = productDto.Name,
                Type = productDto.Type,
                Manufacturer = productDto.Manufacturer
            };
            return View(product);
        }

        [HttpPost]
        public IActionResult Edit(Product model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var productDto = new ProductDTO
            {
                Id = model.Id,
                Name = model.Name,
                Type = model.Type,
                Manufacturer = model.Manufacturer
            };

            _productsService.Update(productDto);
            return RedirectToAction("Index");
        }
        [HttpPost]
        public IActionResult Delete(int id)
        {
            _productsService.Delete(id);
            return RedirectToAction("Index");
        }
    }
}
