﻿using IMSWeb.Interface;
using IMSWeb.Models;
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

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Supplier>>> GetAllSuppliers()
        {
            var suppliers = await _supplierRepository.GetAllSuppliers();
            return Ok(suppliers);
        }

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
        public async Task<ActionResult<Supplier>> CreateSupplier(Supplier supplier)
        {
            await _supplierRepository.CreateSupplier(supplier);
            return CreatedAtAction(nameof(GetSupplierById), new { id = supplier.Id }, supplier);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateSupplier(int id, Supplier supplier)
        {
            var result = await _supplierRepository.UpdateSupplier(id, supplier);
            if (!result)
            {
                return NotFound();
            }
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSupplier(int id)
        {
            var result = await _supplierRepository.DeleteSupplier(id);
            if (!result)
            {
                return NotFound();
            }
            return NoContent();
        }
    }
}

