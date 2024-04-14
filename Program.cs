using Products_Management_ConsoleApp.Class;

namespace Products_Management_ConsoleApp
{
    class Program
    {
        static string[] pozycjeMenu = ["Utworz nowe zamowienie", "Wyswietl wszystkie zamowienia", "Koniec"];
        static int aktywnaPozycjaMenu = 0; // start with the first menu option

        static void Main(string[] args)
        {
            Menu.StartMenu();
        }

        public static string[] GetMenuItems()
        {
            return pozycjeMenu;
        }

        public static ref int GetActiveMenuItem()
        {
            return ref aktywnaPozycjaMenu;
        }
    }
}

