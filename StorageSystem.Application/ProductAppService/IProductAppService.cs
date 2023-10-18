﻿using StorageSystem.Application.ProductAppService.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StorageSystem.Application.ProductAppService
{
    public interface IProductAppService
    {
        Task<List<GetProductForView>> GetAll();
        Task<GetProductForEditOutput> GetProductForEdit(int id);
        Task CreateProduct(CreateProductDto input);
        Task UpdateProduct(int id, UpdateProductDto input);
        Task Delete(int id);
    }
}
