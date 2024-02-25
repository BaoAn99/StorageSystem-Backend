using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StorageSystem.Application.Models.Supplier.Outs
{
    public class GetSupplierForView
    {
        public List<SupplierList> Suppliers = new List<SupplierList>();

        public int Total { get; set; }
    }

    public class SupplierList
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Address { get; set; }

        public string Phone { get; set; }

        public string Email { get; set; }

        public DateTimeOffset DateCreated { get; set; }

        //public decimal Price { set; get; }
    }
}
