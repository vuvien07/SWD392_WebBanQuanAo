﻿using A_LIÊM_SHOP.Models;
using A_LIÊM_SHOP.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace A_LIÊM_SHOP.Repositories
{
    public interface IProductRepository
    {
        public Task<List<Product>> GetProductsAsync(int? brand, int? category, decimal? minPrice, decimal? maxPrice, string? searchQuery, string sortOrder);
        public Product GetProductById(int id);

        public Product GetProductByIdIncludeCategory(int id);
        public void reduceQuantity(List<Item> cart);

        public Task<List<Product>> top8NewestProduct();
    }
}
