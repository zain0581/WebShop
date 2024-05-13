using IMSWeb.Dto;
using IMSWeb.Interface;
using IMSWeb.Models;
using IMSWeb.Repo;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace IMSWeb.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SupplierController : ControllerBase
    {
        private readonly ISupplier _supplierRepository;

        public SupplierController(ISupplier supplierRepository)
        {
            _supplierRepository = supplierRepository;
        }

        [HttpGet("GettingSuppliersOnly")]
        public async Task<IActionResult> GetSuppliers()
        {
            var suppliers = await _supplierRepository.GetSuppliers();
            return Ok(suppliers);
        }

        //[HttpGet]
        //public async Task<ActionResult<IEnumerable<Supplier>>> GetAllSuppliers()
        //{
        //    var suppliers = await _supplierRepository.GetAllSuppliers();
        //    return Ok(suppliers);
        //}

        [HttpGet("{id}")]
        public async Task<ActionResult<Supplier>> GetSupplierById(int id)
        {
            var supplier = await _supplierRepository.GetSupplierById(id);
            if (supplier == null)
            {
                return NotFound();
            }
            return Ok(supplier);
        }

        [HttpPost]
        public async Task<ActionResult<Supplier>> CreateSupplier(SupplierDTO supplier)
        {
            await _supplierRepository.CreateSupplier(supplier);
            return CreatedAtAction(nameof(GetSupplierById), new { id = supplier.Id }, supplier);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateSupplier(int id, SupplierDTO supplier)
        {
            var result = await _supplierRepository.UpdateSupplier(id, supplier);
            if (!result)
            {
                return NotFound();
            }
            return Ok("Supplier Updated successfully");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSupplier(int id)
        {
            var result = await _supplierRepository.DeleteSupplier(id);
            if (!result)
            {
                return NotFound();
            }
            return Ok("Supplier deleted successfully");
        }
    }
}

