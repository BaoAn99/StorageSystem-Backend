using Microsoft.EntityFrameworkCore;
using StorageSystem.Application.Contracts.DataAccess;
using StorageSystem.Application.Models.Bases;
using StorageSystem.DataAccess.UOW.Base;
using StorageSystem.Domain.Entities;
using StorageSystem.Persistence.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StorageSystem.DataAccess.UOW;

public class SupplierDataAccess : GenericDataAccess<Supplier>, ISupplierDataAccess
{
    public SupplierDataAccess(IApplicationDbContext context) : base(context)
    {
    }

    public async Task CreateSupplierAsync(Supplier supplier, CancellationToken cancellationToken = default)
    {
        await InsertAsync(supplier, cancellationToken);
    }

    public async Task CreateSupplierRangeAsync(List<Supplier> suppliers, CancellationToken cancellationToken = default)
    {
        await _context.Suppliers.AddRangeAsync(suppliers, cancellationToken);
    }

    public void DeleteSupplier(Supplier supplier)
    {
        supplier.IsDeleted = true;
        _context.Suppliers.Update(supplier);
    }

    public void DeleteSupplierRange(List<Supplier> suppliers)
    {
        _context.Suppliers.RemoveRange(suppliers);
    }

    public async Task<Supplier> FindSupplierById(Guid Id)
    {
        return await _context.Suppliers.FirstOrDefaultAsync(c => c.Id == Id);
    }

    public async Task<IEnumerable<Supplier>> GetAllSuppliers(CancellationToken cancellationToken = default)
    {
        return await _context.Suppliers.ToListAsync(cancellationToken);
    }

    public async Task<IEnumerable<Supplier>> GetAllSuppliers(FilterBase filter, bool trackingReference, CancellationToken cancellationToken = default)
    {
        var data = await _context.Suppliers
            .Where(p => (string.IsNullOrEmpty(filter.Keyword) || p.Name.ToLower().Contains(filter.Keyword.ToLower()))
            && p.IsDeleted == false
            )
            .OrderBy(p => p.Name)
            //.GroupBy(p => p.Name)
            //.Select(grp => grp.ToList())
            .Skip((filter.PageNumber - 1) * filter.PageSize)
            .Take(filter.PageSize)
            .ToListAsync(cancellationToken);
        return (IEnumerable<Supplier>)data;
    }

    public int GetTotalSuppliers(string keyword = null)
    {
        return _context.Suppliers
            .Where(p => (string.IsNullOrEmpty(keyword) || p.Name.ToLower().Contains(keyword.ToLower())) && p.IsDeleted == false)
            .Count();
    }

    public void UpdateSupplier(Supplier supplier)
    {
        _context.Suppliers.Update(supplier);
    }

    public void UpdateSupplierRange(List<Supplier> suppliers)
    {
        _context.Suppliers.UpdateRange(suppliers);
    }
}
