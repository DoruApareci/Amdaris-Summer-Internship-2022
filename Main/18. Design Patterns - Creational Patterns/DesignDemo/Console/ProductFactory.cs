using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Console
{
    public interface IProductOptions
    {
        IProductOptions WithDescription(string description);
        string GetDescription();
    }

    public class ProductFactory
    {
        public Product CreateNewProduct(string name, long price, IList<long> categoryIds,
           Action<IProductOptions> optionalParams = null)
        {
            var options = new ProductOptions();
            if (optionalParams != null)
                optionalParams(options);
            string description = options.GetDescription();
            if (string.IsNullOrWhiteSpace(description))
                description = "No Description available";

            var product = new Product(name, description, price, 0, categoryIds);

            OnProductCreation(product);

            return product;
        }
        public Product CreateExportedProduct(string name, long price, long ranking, IList<long> categoryIds, Action<IProductOptions> optionalParams = null)
        {
            var options = new ProductOptions();
            if (optionalParams != null)
                optionalParams(options);
            string description = options.GetDescription();
            if (string.IsNullOrWhiteSpace(description))
                description = "No Description available";

            var product = new Product(name, description, price, ranking, categoryIds);

            OnProductCreation(product);

            return product;
        }

        private void OnProductCreation(Product product)
        {
            System.Console.WriteLine($"Hey, product {product.Name} is available in stores.");
        }

        public class ProductOptions : IProductOptions
        {
            private string _description;

            public IProductOptions WithDescription(string description)
            {
                _description = description;
                return this;
            }

            public string GetDescription()
            {
                return _description;
            }
        }
    }
}
