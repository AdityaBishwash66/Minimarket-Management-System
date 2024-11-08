﻿using CoreBusiness;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UseCases.DataStorePluginInterfaces;

namespace Plugins.DataStore.InMemory
{
	public class ProductInMemoryRepository : IProductRepository
	{
		private List<Product> products;

		public ProductInMemoryRepository()
		{
			products = new List<Product>
			{
				new Product { ProductId=1, CategoryId=1, Name="Iced Tea", Quantity=100, Price=1.99 },
				new Product { ProductId=2, CategoryId=1, Name="Canada Dry", Quantity=200, Price=1.99 },
				new Product { ProductId=3, CategoryId=2, Name="whole wheat bread", Quantity=300, Price=1.50 },
				new Product { ProductId=4, CategoryId=2, Name="white bread", Quantity=300, Price=1.60 }
			};
		}

		public void AddProduct(Product product)
		{
			//if (products.Any(x => x.Name.Equals(product.Name, StringComparison.OrdinalIgnoreCase)))
			if (products.Any(x => x.Name.ToLower() == product.Name.ToLower()))
				return;

			if (products != null && products.Count > 0)
			{
				var maxId = products.Max(x => x.CategoryId);
				product.ProductId = maxId.Value + 1;
			}
			else
				product.ProductId = 1;
			
			products.Add(product);
		}

		public void Delete(int productId)
		{
			products?.Remove(GetProductById(productId));
		}

		public Product GetProductById(int productId)
		{
			return products?.FirstOrDefault(x => x.ProductId == productId);
		}

		public IEnumerable<Product> GetProducts()
		{
			return products;
		}

		public IEnumerable<Product> GetProductsByCategory(int categoryId)
		{
			return products?.Where(x => x.CategoryId == categoryId);
		}

		public void Update(Product product)
		{
			var productToUpdate = GetProductById(product.ProductId);
			if (productToUpdate != null)
			{
				productToUpdate.Name = product.Name;
				productToUpdate.Price = product.Price;
				productToUpdate.Quantity = product.Quantity;
				productToUpdate.ProductId = product.ProductId;
				productToUpdate.CategoryId = product.CategoryId;
			}
		}
	}
}
