﻿using Marisa.Domain.Entities;

namespace Marisa.Domain.Repositories
{
    public interface IProductRepository : IGenericRepository<Product>
    {
        public Task<Product?> GetProductByIdToDelete(Guid? productId);
        public Task<Product?> GetProductIfExist(Guid? productId);
        public Task<List<Product>?> GetAllProductType(string type);
        public Task<Product?> GetProductById(Guid? productId);
    }
}
