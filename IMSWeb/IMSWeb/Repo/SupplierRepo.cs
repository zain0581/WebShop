using IMSWeb.Dal;
using IMSWeb.Dto;
using IMSWeb.Interface;
using IMSWeb.Models;
using Microsoft.EntityFrameworkCore;

namespace IMSWeb.Repo
{
    public class SupplierRepo : ISupplier
    {
        private IMSContext _context;
        public SupplierRepo(IMSContext context)
        {
            _context = context;
           
        }
        public async Task<List<SupplierDTO>> GetSuppliers()
        {
            var suppliers = await _context.Suppliers.ToListAsync();

            // Map suppliers to DTOs
            var supplierDTOs = suppliers.Select(supplier => new SupplierDTO
            {
                Id = supplier.Id,
                Name = supplier.Name,
                Email = supplier.Email,
                Phone = supplier.Phone
            }).ToList();

            return supplierDTOs;
        }

        public async Task<List<SupplierDTO>> GetAllSuppliers()
        {
            var suppliers = await _context.Suppliers.Include(s => s.InventoryItems).ToListAsync();

            // Map suppliers to DTOs
            var supplierDTOs = suppliers.Select(supplier => new SupplierDTO
            {
                Id = supplier.Id,
                Name = supplier.Name,
                //Email = supplier.Email,
                Phone = supplier.Phone,
                InventoryItems = supplier.InventoryItems?.Select(item => new InventoryItemDto
                {
                    Id = item.Id,
                    Name = supplier.Name,
                    Description = item.Description,
                    ImageUrl = item.ImageUrl,
                    IsAvailable = item.IsAvailable,
        
        // Map inventory item properties as needed
                }).ToList()
            }).ToList();

            return supplierDTOs;
        }

        public async Task<Supplier> GetSupplierById(int id)
        {
            return await _context.Suppliers.FindAsync(id);
        }

        public async Task<bool> CreateSupplier(Supplier supplier)
        {
            _context.Suppliers.Add(supplier);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> UpdateSupplier(int id, Supplier supplier)
        {
            var existingSupplier = await _context.Suppliers.FindAsync(id);
            if (existingSupplier == null)
                return false;

            existingSupplier.Name = supplier.Name;
            existingSupplier.Email = supplier.Email;
            existingSupplier.Phone = supplier.Phone;

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteSupplier(int id)
        {
            var supplier = await _context.Suppliers.FindAsync(id);
            if (supplier == null)
                return false;

            _context.Suppliers.Remove(supplier);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
