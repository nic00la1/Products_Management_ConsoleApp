namespace Products_Management_ConsoleApp.Class
{
    internal class Menu
    {
        static string[] pozycjeMenu = { "Utworz zamowienie", "Wyswietl Zamowienia",
                                        "Koniec"};
        static int aktywnaPozycjaMenu = 0;

        public static void StartMenu()
        {
            Console.Title = "Menu";
            Console.CursorVisible = false;

            while (true)
            {
                PokazMenu();
                WybieranieOpcji();
                UruchomOpcje();
            }
        }

        static void PokazMenu()
        {
            Console.BackgroundColor = ConsoleColor.Gray;
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.WriteLine(">>> Menu <<<\n");

            for (int i = 0; i < pozycjeMenu.Length; i++)
            {
                if (i == aktywnaPozycjaMenu)
                {
                    Console.BackgroundColor = ConsoleColor.DarkCyan;
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.WriteLine("{0, -35}", pozycjeMenu[i]);
                    Console.BackgroundColor = ConsoleColor.Gray;
                    Console.ForegroundColor = ConsoleColor.DarkCyan;
                }
                else
                {
                    Console.WriteLine(pozycjeMenu[i]);

                }
            }
        }

        static void WybieranieOpcji()
        {
            do
            {
                ConsoleKeyInfo klawisz = Console.ReadKey();
                if (klawisz.Key == ConsoleKey.UpArrow) // strzalka w gore
                {
                    aktywnaPozycjaMenu = (aktywnaPozycjaMenu > 0) ? aktywnaPozycjaMenu - 1 : pozycjeMenu.Length - 1;
                    PokazMenu();
                }
                else if (klawisz.Key == ConsoleKey.DownArrow) // strzalka w dol
                {
                    aktywnaPozycjaMenu = (aktywnaPozycjaMenu + 1) % pozycjeMenu.Length;
                    PokazMenu();
                }
                else if (klawisz.Key == ConsoleKey.Escape) // Koniec
                {
                    aktywnaPozycjaMenu = pozycjeMenu.Length - 1;
                    break;
                }
                else if (klawisz.Key == ConsoleKey.Enter)
                    break;

            } while (true);
        }

        static void UruchomOpcje()
        {
            switch (aktywnaPozycjaMenu)
            {
                case 0:
                    Console.Clear();
                    TworzenieZamowienia.UtworzZamowienie();
                    break;
                case 1:
                    Console.Clear();
                    TworzenieZamowienia.WyswietlWszystkieZamowienia();
                    break;
                case 2:
                    Environment.Exit(0);
                    break;
            }
        }

        static void OpcjaWBudowie()
        {
            Console.SetCursorPosition(12, 4);
            Console.WriteLine("Opcja w budowie");
            Console.ReadKey();
        }
    }
}
