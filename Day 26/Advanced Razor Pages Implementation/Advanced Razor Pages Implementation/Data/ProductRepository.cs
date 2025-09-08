using Advanced_Razor_Pages_Implementation.Models;
using System.Collections.Generic;
using System.Linq;

namespace Advanced_Razor_Pages_Implementation.Data
{
    public class ProductRepository
    {
        private static List<Product> _products = new List<Product>();

        public static List<Product> GetAll() => _products;

        public static Product GetById(int id) => _products.FirstOrDefault(p => p.ProductID == id);

        public static void Add(Product product)
        {
            product.ProductID = _products.Count + 1;
            _products.Add(product);
        }
    }
}
