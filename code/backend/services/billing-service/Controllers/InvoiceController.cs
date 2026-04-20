using billing_service.DTOs;
using billing_service.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace billing_service.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class InvoiceController : ControllerBase
    {
        private readonly IInvoiceService _service;

        public InvoiceController(IInvoiceService service)
        {
            _service = service;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var invoices = _service.GetAll();
            return Ok(invoices);
        }

        [HttpPost]
        public IActionResult Create([FromBody] CreateInvoiceDTO dto)
        {
            var result = _service.Create(dto);
            return CreatedAtAction(nameof(GetAll), new { id = result.Id }, result);
        }

        [HttpPut("{id:long}")]
        public IActionResult Update(long id, [FromBody] CreateInvoiceDTO dto)
        {
            var result = _service.Update(id, dto);
            return Ok(result);
        }


        [HttpPost("{id:long}/print")]
        public async Task<IActionResult> Print(long id)
        {
            await _service.PrintAndClose(id);
            return Ok(new { message = "Nota fiscal processada e estoque atualizado com sucesso." });
        }
    }
}