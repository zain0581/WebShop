using IMSWeb.Dto;
using IMSWeb.Models;

namespace IMSWeb.Interface
{
    public interface ISupplier
    {
       public Task<List<SupplierDTO>> GetAllSuppliers();
        public Task<List<SupplierDTO>> GetSuppliers();
       public Task<Supplier> GetSupplierById(int id);
       public Task<bool> CreateSupplier(SupplierDTO supplier);
       public Task<bool> UpdateSupplier(int id, Supplier supplier);
       public Task<bool> DeleteSupplier(int id);
    }
}
