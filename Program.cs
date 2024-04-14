using Products_Management_ConsoleApp.Class;

namespace Products_Management_ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            string[] pozycjeMenu = { "Utworz nowe zamowienie", "Wyswietl wszystkie zamowienia", "Koniec" };
            int aktywnaPozycjaMenu = 0; // start with the first menu option

            Menu.StartMenu(pozycjeMenu, ref aktywnaPozycjaMenu);
        }
    }
}

