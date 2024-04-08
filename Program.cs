using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;

namespace OrderManagement
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Witaj w programie do Zarządzania Produktami!");

            while (true)
            {
                Console.WriteLine("Wybierz opcję:");
                Console.WriteLine("1. Utwórz nowe zamówienie");
                Console.WriteLine("2. Dodaj produkt do zamówienia");
                Console.WriteLine("3. Przelicz wartość zamówienia");
                Console.WriteLine("4. Wyświetl szczegóły zamówienia");
                Console.WriteLine("5. Wyjdź");

                string option = Console.ReadLine();

                switch (option)
                {
                    case "1":
                        CreateOrder();
                        break;
                    case "2":
                        AddProductToOrder();
                        break;
                    case "3":
                        CalculateOrderValue();
                        break;
                    case "4":
                        DisplayOrderDetails();
                        break;
                    case "5":
                        Console.WriteLine("Thank you for using Order Management System. Goodbye!");
                        return;
                    default:
                        Console.WriteLine("Invalid option. Please try again.");
                        break;
                }
            }
        }

        static void CreateOrder()
        {
            Console.WriteLine("Enter the order ID:");
            string orderId = Console.ReadLine();

            Console.WriteLine("Enter the customer name:");
            string customerName = Console.ReadLine();

            Order order = new Order(orderId, customerName);

            string orderJson = JsonConvert.SerializeObject(order);

            File.WriteAllText("zamowienia.json", orderJson);

            Console.WriteLine("Order created successfully.");
        }

        static void AddProductToOrder()
        {
            Console.WriteLine("Enter the order ID:");
            string orderId = Console.ReadLine();

            Console.WriteLine("Enter the product name:");
            string productName = Console.ReadLine();

            Console.WriteLine("Enter the product price:");
            decimal productPrice = Convert.ToDecimal(Console.ReadLine());

            Product product = new Product(productName, productPrice);

            string productJson = JsonConvert.SerializeObject(product);

            File.AppendAllText("produkty.json", productJson + Environment.NewLine); 

            Console.WriteLine("Product added to the order successfully.");
        }

        static void CalculateOrderValue()
        {
            Console.WriteLine("Enter the order ID:");
            string orderId = Console.ReadLine();

            string orderJson = File.ReadAllText("zamowienia.json");

            Order order = JsonConvert.DeserializeObject<Order>(orderJson);

            decimal orderValue = 0;

            string[] productLines = File.ReadAllLines("produkty.json");

            foreach (string productLine in productLines)
            {
                Product product = JsonConvert.DeserializeObject<Product>(productLine);
                orderValue += product.Price;
            }

            Console.WriteLine($"The total value of the order is: {orderValue}");
        }

        static void DisplayOrderDetails()
        {
            Console.WriteLine("Enter the order ID:");
            string orderId = Console.ReadLine();

            string orderJson = File.ReadAllText("zamowienia.json");

            Order order = JsonConvert.DeserializeObject<Order>(orderJson);

            Console.WriteLine($"Order ID: {order.OrderId}");
            Console.WriteLine($"Customer Name: {order.CustomerName}");

            Console.WriteLine("Products:");

            string[] productLines = File.ReadAllLines("produkty.json");

            foreach (string productLine in productLines)
            {
                Product product = JsonConvert.DeserializeObject<Product>(productLine);
                Console.WriteLine($"- {product.Name} (${product.Price})");
            }
        }
    }

    class Order
    {
        public string OrderId { get; set; }
        public string CustomerName { get; set; }

        public Order(string orderId, string customerName)
        {
            OrderId = orderId;
            CustomerName = customerName;
        }
    }

    class Product
    {
        public string Name { get; set; }
        public decimal Price { get; set; }

        public Product(string name, decimal price)
        {
            Name = name;
            Price = price;
        }
    }
}
