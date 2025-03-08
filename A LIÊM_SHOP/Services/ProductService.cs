﻿using A_LIÊM_SHOP.Models;
using A_LIÊM_SHOP.Repositories;
using A_LIÊM_SHOP.ViewModels;
using Microsoft.Data.SqlClient;
using System.Drawing.Drawing2D;

namespace A_LIÊM_SHOP.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;

        public ProductService(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public Product GetProductById(int productId)
        {
            return _productRepository.GetProductById(productId);
        }

		public Product GetProductByIdIncludeCategory(int id)
		{
			return _productRepository.GetProductByIdIncludeCategory(id);
		}

		public async Task<List<Product>> GetProductsAsync(int? brand, int? category, decimal? minPrice, decimal? maxPrice, string? searchQuery, string sortOrder)
        {
            return await _productRepository.GetProductsAsync(brand, category, minPrice, maxPrice, searchQuery, sortOrder);
        }

        public void reduceQuantity(List<Item> cart)
        {
            _productRepository.reduceQuantity(cart);
        }

        public async Task<List<Product>> top8NewestProduct()
        {
            return await _productRepository.top8NewestProduct();
        }
    }
}
