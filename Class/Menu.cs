namespace Products_Management_ConsoleApp.Class
{
    internal class Menu
    {
        public static void StartMenu()
        {
            Console.Title = "Menu";
            Console.CursorVisible = false;

            string[] pozycjeMenu = Program.GetMenuItems();
            ref int aktywnaPozycjaMenu = ref Program.GetActiveMenuItem();

            while (true)
            {
                PokazMenu(pozycjeMenu, aktywnaPozycjaMenu);
                aktywnaPozycjaMenu = WybieranieOpcji(pozycjeMenu, aktywnaPozycjaMenu);
                UruchomOpcje(pozycjeMenu, aktywnaPozycjaMenu);
            }
        }

        public static void PokazMenu(string[] pozycjeMenu, int aktywnaPozycjaMenu)
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

        public static int WybieranieOpcji(string[] pozycjeMenu, int aktywnaPozycjaMenu)
        {
            do
            {
                ConsoleKeyInfo klawisz = Console.ReadKey();
                if (klawisz.Key == ConsoleKey.UpArrow) // strzalka w gore
                {
                    aktywnaPozycjaMenu = (aktywnaPozycjaMenu > 0) ? aktywnaPozycjaMenu - 1 : pozycjeMenu.Length - 1;
                    PokazMenu(pozycjeMenu, aktywnaPozycjaMenu);
                }
                else if (klawisz.Key == ConsoleKey.DownArrow) // strzalka w dol
                {
                    aktywnaPozycjaMenu = (aktywnaPozycjaMenu + 1) % pozycjeMenu.Length;
                    PokazMenu(pozycjeMenu, aktywnaPozycjaMenu);
                }
                else if (klawisz.Key == ConsoleKey.Escape) // Koniec
                {
                    aktywnaPozycjaMenu = pozycjeMenu.Length - 1;
                    break;
                }
                else if (klawisz.Key == ConsoleKey.Enter)
                    break;

            } while (true);

            return aktywnaPozycjaMenu;
        }

        static void UruchomOpcje(string[] pozycjeMenu, int aktywnaPozycjaMenu)
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
    }
}
