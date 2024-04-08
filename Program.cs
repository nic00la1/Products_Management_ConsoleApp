using Products_Management_ConsoleApp.Class;
using System.Text.Json;

namespace Products_Management_ConsoleApp
{
    class Program
    {
        public static List<Order> Orders;
        public static Order order;
        public static List<Product> Products;
        public static string pathDummyData = "C:\\Users\\Admin\\Source\\Repos\\nic00la1\\Products_Management_ConsoleApp\\ProductsDummyData\\produkty.json";
        public static string jsonProducts = File.ReadAllText(pathDummyData); // Odczytujemy plik JSON z danymi produktów
        public static List<Product> products = JsonSerializer.Deserialize<List<Product>>(jsonProducts); // Deserializujemy dane produktów z JSONa do listy produktów

        static void Main(string[] args)
        {
            while (true) // Jeśli chcemy, aby program działał w nieskończoność
            {
                // Showing the Menu 
                Console.WriteLine("Witaj w Zarzadzaniu Produktami!");
                Console.WriteLine("1. Stwórz nowe zamówienie");
                Console.WriteLine("2. Wyswietl Zamowienia");
                Console.WriteLine("3. Wyjdź");

                string option = Console.ReadLine();

                switch (option)
                {
                    case "1":
                        CreateOrder();
                        break;
                    case "2":
                        DisplayOrders();
                        break;
                    case "3":
                        Console.WriteLine("Dziękujemy za skorzystanie z programu. Do zobaczenia!");
                        return;
                    default:
                        Console.Clear();
                        Console.WriteLine("Nieprawidłowa opcja. Spróbuj ponownie.");
                        break;
                }
            }
        }

        static void CreateOrder()
        {
            Console.Clear();

            Console.WriteLine("Tworzenie nowego zamówienia...");
            Console.WriteLine("Podaj swoje imię:");
            string name = Console.ReadLine();

            Console.WriteLine("Podaj swoje nazwisko:");
            string surname = Console.ReadLine();

            Console.WriteLine("Podaj swój adres:");
            string address = Console.ReadLine();

            Console.WriteLine("Wybierz sposób dostawy: (pisz z malej litery)");
            Console.WriteLine("1. Kurier (cena - 20zl)");
            Console.WriteLine("2. Paczkomat (cena - 10zl)");
            Console.WriteLine("3. Odbiór osobisty (cena - 0zl)");

            string deliveryOption = Console.ReadLine();

            Console.WriteLine("Wybierz sposób płatności: (pisz z malej litery)");
            Console.WriteLine("1. Karta (cena - 2zl)");
            Console.WriteLine("2. Gotówka (cena - 0zl)");

            string paymentOption = Console.ReadLine();


            order = new Order(
                Orders.Count + 1,
                products,
                name,
                surname,
                address,
                deliveryOption,
                paymentOption,
                0,
                DateOnly.FromDateTime(DateTime.Now)
            );

            Console.WriteLine("Zamówienie zostało utworzone. Możesz teraz dodać produkty do zamówienia.");

            while (true) // Petla ktora pozwala dodawac produkty do zamowienia
            {
                Console.Clear();
                Console.WriteLine("Co chcesz zrobić? Wybierz opcję:");
                Console.WriteLine("1. Dodaj produkt po ID");
                Console.WriteLine("2. Wyswietl wszystkie produkty");
                Console.WriteLine("3. Pokaz szczegoly zamowienia");
                Console.WriteLine("4. Złóż zamówienie");

                int option = Convert.ToInt16(Console.ReadLine());

                switch (option)
                {
                    case 1:
                        AddProductByID();
                        break;
                    case 2:
                        DisplayAllProducts();
                        break;
                }
            }
        }

        static void AddProductByID()
        {
            Console.WriteLine("Podaj ID produktu, który chcesz dodać do zamówienia:");
            int productId = Convert.ToInt16(Console.ReadLine());
            Product product = products.FirstOrDefault(p => p.Id == productId); // Szukamy produktu o podanym ID

            if (product != null) // Jeśli produkt o podanym ID istnieje, dodajemy go do zamówienia
            {
                order.AddProduct(product);
                Console.WriteLine("Produkt został dodany do zamówienia.");
            }
            else
            {
                Console.WriteLine("Nie znaleziono produktu o podanym ID.");
            }
        }

        static void DisplayAllProducts()
        {
            Console.Clear();

            Console.WriteLine("Wszystkie produkty: ");
            foreach (var product in products)
            {
                Console.WriteLine($"ID: {product.Id}, Nazwa: {product.Name}, Cena: {product.Price}");
            }
        }

        static void DisplayOrders()
        {
            Console.Clear();

            Console.WriteLine("Wszystkie zamowienia: ");
        }
    }
}