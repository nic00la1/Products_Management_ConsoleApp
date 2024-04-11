using Products_Management_ConsoleApp.Class;
using System.Text.Json;

namespace Products_Management_ConsoleApp
{
    class Program
    {
        public static List<Order> orders;
        static string ordersPath = @"C:\Users\Admin\Source\Repos\nic00la1\Products_Management_ConsoleApp\ProductsDummyData\zamowienia.json";
        static string productsPath = @"C:\Users\Admin\Source\Repos\nic00la1\Products_Management_ConsoleApp\ProductsDummyData\produkty.json";

        static void Main(string[] args)
        {
            try
            {
                if (!File.Exists(productsPath))
                {
                    File.Create(productsPath).Close(); // Utworz produkty.json jesli nie istnieje
                }
                if (!File.Exists(ordersPath))
                {
                    File.Create(ordersPath).Close(); // Utworz zamowienia.json jesli nie istnieje
                }

                string json = File.ReadAllText(ordersPath);
                orders = JsonSerializer.Deserialize<List<Order>>(json);

                Console.WriteLine("Witaj w programie zarządzania produktami!");
                Console.WriteLine("1. Utworz zamowienie");
                Console.WriteLine("2. Wyswietl zamowienia");
                Console.WriteLine("3. Dodaj produkt do zamowienia");
                Console.WriteLine("4. Wyjdz");

                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        CreateOrder();
                        break;
                    case "2":
                        ShowOrders();
                        break;
                    case "3":
                        AddProductToOrder();
                        break;
                    case "4":
                        return;
                    default:
                        Console.WriteLine("Niepoprawny wybor");
                        break;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private static void CreateOrder()
        {
            Console.Clear();
            Console.WriteLine("Utworz zamowienie\n");
            Console.WriteLine("Podaj imie klienta");
            string name = Console.ReadLine();

            Console.WriteLine("Podaj nazwisko klienta");
            string surname = Console.ReadLine();

            Console.WriteLine("Podaj adres klienta");
            string address = Console.ReadLine();

            Console.WriteLine("Wybierz opcje dostawy");
            Console.WriteLine("1. Odbior osobisty");
            Console.WriteLine("2. Paczkomat");
            Console.WriteLine("3. Kurier");
            int deliveryOption = int.Parse(Console.ReadLine());

            Console.WriteLine("Wybierz opcje platnosci");
            Console.WriteLine("1. Karta");
            Console.WriteLine("2. Gotowka");
            int paymentOption = int.Parse(Console.ReadLine());

            Console.ReadKey();
            List<Product> products;

            string json = File.ReadAllText(productsPath);
            products = JsonSerializer.Deserialize<List<Product>>(json);

            Order nowe_zamowienie = new Order(
                orders.Count + 1,
                products,
                name,
                surname,
                address,
                deliveryOption,
                paymentOption,
                0,
                DateOnly.FromDateTime(DateTime.Now)
             );
        }

        private static void ShowOrders()
        {
            Console.Clear();
            Console.WriteLine("Zamowienia\n");

            foreach (var order in orders)
            {
                Console.WriteLine($"Id: {order.Id}");
                Console.WriteLine($"Imie: {order.CustomerName}");
                Console.WriteLine($"Nazwisko: {order.CustomerSurname}");
                Console.WriteLine($"Adres: {order.CustomerAddress}");
                Console.WriteLine($"Opcja dostawy: {order.DeliveryOption}");
                Console.WriteLine($"Opcja platnosci: {order.PaymentOption}");
                Console.WriteLine($"Data zamowienia: {order.OrderDate}");
                Console.WriteLine($"Cena calkowita: {order.TotalPrice}");
                Console.WriteLine();
            }
        }


        private static void AddProductToOrder()
        {
            Console.Clear();
            Console.WriteLine("Dodaj produkt do zamowienia\n");
        }
    }
}

