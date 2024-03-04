using StorageSystem.Application.Models.Order.Base;
using StorageSystem.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StorageSystem.Application.Models.Order.Ins
{
    public class CreateOrderInsDto : CreateOrUpdateOrderDto
    {
        public string? CustomerName { get; set; }

        public string? Phone { get; set; }

        public string? Address { get; set; }

        public Guid OwnerId { get; set; }

        public decimal? DiscountTotal { get; set; }

        public List<CreateOrderItemInsDto> Orders { get; set; }
    }

    public class CreateOrderItemInsDto
    {
        public Guid ProductId { get; set; }

        public decimal Price { get; set; }

        public int Quantity { get; set; }

        public decimal? DiscountItem { get; set; }

        //public StatusOrder StatusOrder { get; set; }
    }
}
