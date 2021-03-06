﻿using Grupp9WebbShop.Data.Models;
using System.Collections.Generic;

namespace Grupp9WebbShop.Data
{
    public interface IShopDataService
    {
        Product GetProductById(int id);
        IEnumerable<ProductCategory> GetProductCategories();
        IEnumerable<Product> GetProducts();
        IEnumerable<Product> GetHighlightedProducts();
        int GetProductStock(int id);
        void SetProductStock(int id, int quant);
        bool DecreaseProductStock(int id, int quant);
        IEnumerable<Product> GetProductsByCategory(int categoryId);
    }
}