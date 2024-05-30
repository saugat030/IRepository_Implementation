using System;
using System.Collections.Generic;
using System.Linq;

// Define a generic repository interface
public interface IRepository<T>
        {
            T GetById(int id);
            IEnumerable<T> GetAll();
            void Add(T entity);
            void Update(T entity);
            void Delete(int id);
        }

        // Example entity class
        public class Product
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public decimal Price { get; set; }
        }

        // Implement the generic repository interface for Product entity
        public class ProductRepository : IRepository<Product>
        {
            private List<Product> products;

            public ProductRepository()
            {
                products = new List<Product>();
            }

            public void Add(Product product)
            {
                products.Add(product);
            }

            public void Delete(int id)
            {
                Product productToDelete = GetById(id);
                if (productToDelete != null)
                {
                    products.Remove(productToDelete);
                }
            }

            public IEnumerable<Product> GetAll()
            {
                return products;
            }

            public Product GetById(int id)
            {
                return products.Find(p => p.Id == id);
            }

            public void Update(Product product)
            {
                int index = products.FindIndex(p => p.Id == product.Id);
                if (index != -1)
                {
                    products[index] = product;
                }
            }
        }

class Program
{
    static void Main(string[] args)
    {
        // Example usage of IRepository<Product>
        IRepository<Product> productRepository = new ProductRepository();

        productRepository.Add(new Product { Id = 1, Name = "Laptop", Price = 999.99m });
        productRepository.Add(new Product { Id = 2, Name = "Smartphone", Price = 599.99m });

        Console.WriteLine("All Products:");
        foreach (var product in productRepository.GetAll())
        {
            Console.WriteLine($"ID: {product.Id}, Name: {product.Name}, Price: {product.Price:C}");
        }

        Product laptop = productRepository.GetById(1);
        if (laptop != null)
        {
            laptop.Price = 899.99m;
            productRepository.Update(laptop);
        }

        Console.WriteLine("\nUpdated Product:");
        Product updatedLaptop = productRepository.GetById(1);
        if (updatedLaptop != null)
        {
            Console.WriteLine($"ID: {updatedLaptop.Id}, Name: {updatedLaptop.Name}, Price: {updatedLaptop.Price:C}");
        }

        productRepository.Delete(2);

        Console.WriteLine("\nRemaining Products:");
        foreach (var product in productRepository.GetAll())
        {
            Console.WriteLine($"ID: {product.Id}, Name: {product.Name}, Price: {product.Price:C}");
        }
        Console.ReadLine(); 
    }

}

