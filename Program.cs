using System;
using System.Collections.Generic;
using System.Linq;

class Program
{
    static List<Product> products = new List<Product>();

    static void Main(string[] args)
    {
        while (true)
        {
            Console.WriteLine("\nMenu:");
            Console.WriteLine("1. Multi-threading");
            Console.WriteLine("2. LINQ");
            Console.WriteLine("3. Events");
            Console.WriteLine("4. Exit");
            Console.Write("Enter your choice: ");
            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    RunMultiThreading();
                    break;
                case "2":
                    RunLINQ();
                    break;
                case "3":
                    RunEvents();
                    break;
                case "4":
                    Console.WriteLine("Exiting program...");
                    return;
                default:
                    Console.WriteLine("Invalid choice. Please enter a valid option.");
                    break;
            }
        }
    }

    static void RunMultiThreading()
    {
        Console.WriteLine("\nMulti-threading:");
        NumberProcessor numberProcessor = new NumberProcessor();
        List<int> numbers = new List<int> { 1, 2, 3, 4, 5 };
        foreach (int number in numbers)
        {
            System.Threading.Thread thread = new System.Threading.Thread(() => numberProcessor.ProcessNumber(number));
            thread.Start();
        }
        System.Threading.Thread.Sleep(6000); 
    }

    static void RunLINQ()
    {

        products.Add(new Product { Name = "Laptop", Price = 1200 });
        products.Add(new Product { Name = "Smartphone", Price = 800 });
        products.Add(new Product { Name = "Headphones", Price = 100 });
        products.Add(new Product { Name = "Tablet", Price = 500 });

        while (true)
        {
            Console.WriteLine("\nLINQ Menu:");
            Console.WriteLine("1. Add Product");
            Console.WriteLine("2. Show Products");
            Console.WriteLine("3. Find products with price greater than or equal to a specified value");
            Console.WriteLine("4. Back to main menu");
            Console.Write("Enter your choice: ");
            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    AddProduct();
                    break;
                case "2":
                    ShowProducts();
                    break;
                case "3":
                    FindProductsByPrice();
                    break;
                case "4":
                    return;
                default:
                    Console.WriteLine("Please enter a valid option.");
                    break;
            }
        }
    }

    static void AddProduct()
    {
        Console.Write("Enter product name: ");
        string name = Console.ReadLine();

        Console.Write("Enter product price: ");
        double price;
        while (!double.TryParse(Console.ReadLine(), out price) || price <= 0)
        {
            Console.WriteLine(" Please enter a valid positive number.");
        }

        products.Add(new Product { Name = name, Price = price });
        Console.WriteLine("Product added successfully!");
    }

    static void ShowProducts()
    {
        if (products.Count == 0)
        {
            Console.WriteLine("No products found.");
            return;
        }

        Console.WriteLine("\nList of products:");
        foreach (var product in products)
        {
            Console.WriteLine($"Name: {product.Name}, Price: {product.Price:C}");
        }
    }

    static void FindProductsByPrice()
    {
        Console.Write("Enter the minimum price: ");
        double minPrice;
        while (!double.TryParse(Console.ReadLine(), out minPrice) || minPrice < 0)
        {
            Console.WriteLine(" Please enter a valid positive number.");
            Console.Write("Enter the minimum price: ");
        }

        var filteredProducts = products.Where(p => p.Price >= minPrice).ToList();
        if (filteredProducts.Any())
        {
            Console.WriteLine($"\nProducts with price greater than or equal to ${minPrice}:");
            foreach (var product in filteredProducts)
            {
                product.Name = product.Name.ToUpper();
                Console.WriteLine($"Name: {product.Name}, Price: {product.Price:C}");
            }
        }
        else
        {
            Console.WriteLine("No products found with the specified price.");
        }
    }

    static void RunEvents()
    {
        Console.WriteLine("\nEvents:");
        Downloader downloader = new Downloader();
        downloader.ProgressChanged += (progress) => Console.WriteLine($"Download progress: {progress}%");
        downloader.DownloadFile();
    }
}

class NumberProcessor
{
    public void ProcessNumber(int number)
    {
        Console.WriteLine($"Processing number {number}...");
        System.Threading.Thread.Sleep(new Random().Next(1000, 5000)); 
        Console.WriteLine($"Finished processing number {number}.");
    }
}

class Product
{
    public string Name { get; set; }
    public double Price { get; set; }
}

class Downloader
{
    public event Action<int> ProgressChanged;
    public int Progress { get; private set; }

    public void DownloadFile()
    {
        for (int i = 0; i <= 100; i += 10)
        {
            Progress = i;
            ProgressChanged?.Invoke(i);
            System.Threading.Thread.Sleep(1000); 
        }
        Console.WriteLine("Download complete!");
    }
}
