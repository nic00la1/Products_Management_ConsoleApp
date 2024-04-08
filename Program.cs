using Products_Management_ConsoleApp.Class;

namespace Products_Management_ConsoleApp
{
    class Program
    {
        public static List<Order> Orders;

        static void Main(string[] args)
        {
            while (true) // Jeśli chcemy, aby program działał w nieskończoność
            {
                // Showing the Menu 
                Console.WriteLine("Witaj w Zarzadzaniu Produktami!");
                Console.WriteLine("1. Stwórz nowe zamówienie");
                Console.WriteLine("2. Dodaj produkty do zamówienia");
                Console.WriteLine("3. Wyjdź");

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

        }

        static void AddProductToOrder()
        {
            Console.Clear();

            Console.WriteLine("Dodawanie produktów do zamówienia...");

        }
    }
}