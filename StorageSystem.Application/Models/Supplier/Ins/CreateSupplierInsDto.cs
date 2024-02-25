using Microsoft.EntityFrameworkCore.Metadata.Internal;
using StorageSystem.Application.Models.Supplier.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using StorageSystem.Domain.Entities;

namespace StorageSystem.Application.Models.Supplier.Ins
{
    public class CreateSupplierInsDto : CreateOrUpdateSupplierDto
    {
        public string Name { get; set; }

        public string Address { get; set; }

        public string Phone { get; set; }

        public string Email { get; set; }

        public List<ProductListItemInsDto> Products { get; set; }
    }

    public class ProductListItemInsDto
    {
        public Guid ProductId { get; set; }
        public decimal Price { set; get; }
    }

    public class SupplierDtoToSupplierListConverter : ITypeConverter<CreateSupplierInsDto, IEnumerable<Domain.Entities.Supplier>>
    {
        public IEnumerable<Domain.Entities.Supplier> Convert(CreateSupplierInsDto source, IEnumerable<Domain.Entities.Supplier> destination, ResolutionContext context)
        {
            foreach (var model in source.Products.Select
                (e => context.Mapper.Map<Domain.Entities.Supplier>(e)))
            {
                context.Mapper.Map(source, model);
                yield return model;
            }
        }
    }
}
