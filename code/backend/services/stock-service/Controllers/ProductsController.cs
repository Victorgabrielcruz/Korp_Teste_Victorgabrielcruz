using Microsoft.AspNetCore.Mvc;
using stock_service.Services.Interfaces;
using stock_service.DTOs;

namespace stock_service.Controllers
{
    [ApiController]
    [Route("api/products")]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _service;

        public ProductController(IProductService service)
        {
            _service = service;
        }

        [HttpPost]
        public IActionResult Create([FromBody] CreateProductDTO dto)
        {
            var product = _service.Create(dto);
            return CreatedAtAction(nameof(GetById), new { id = product.Id }, product);
        }

        [HttpGet]
        public IActionResult GetAll() => Ok(_service.GetAll());

        [HttpGet("{id:long}")]
        public IActionResult GetById(long id) => Ok(_service.GetById(id));

        [HttpGet("code/{code}")]
        public IActionResult GetByCode(string code) => Ok(_service.GetByCode(code));

        [HttpPut("{id:long}")]
        public IActionResult Update(long id, [FromBody] UpdateProductDTO dto)
            => Ok(_service.UpdateProduct(id, dto));

        [HttpPatch("{id:long}/stock")]
        public IActionResult UpdateStock(long id, [FromQuery] int quantity)
        {
            _service.UpdateStock(id, quantity);
            return NoContent();
        }

        [HttpDelete("{id:long}")]
        public IActionResult Delete(long id)
        {
            _service.Delete(id);
            return NoContent();
        }

        [HttpGet("code/{code}/validate")]
        public IActionResult ValidateStock(string code, [FromQuery] int quantity)
        {
            var hasStock = _service.HasAvailableStock(code, quantity);
            return Ok(new { available = hasStock });
        }
    }
}