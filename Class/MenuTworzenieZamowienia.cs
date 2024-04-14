namespace Products_Management_ConsoleApp.Class
{
    internal class MenuTworzenieZamowienia
    {
        static string[] pozycjeMenu = { "Dodaj produkt do zamowienia", "Wyswietl wszystkie produkty",
                                            "Pokaz szczegoly zamowienia", "Zloz zamowienie"};
        static int aktywnaPozycjaMenu = 0;

        public static void StartMenu()
        {
            Console.Title = "Tworzenie Zamowienia Menu";
            Console.CursorVisible = false;

            List<Produkt> produkty = new List<Produkt>();
            List<Zamowienie> zamowienia = new List<Zamowienie>();
            Klient klient = new Klient();
            string sposobDostawy = "";
            string sposobPlatnosci = "";

            Zamowienie nowe_zamowienie = new Zamowienie(
                id: zamowienia.Count + 1,
                produkty: null,
                daneKupujacego: klient,
                sposobDostawy: sposobDostawy,
                sposobPlatnosci: sposobPlatnosci,
                kwotaCalkowita: 0
            );

            while (true)
            {
                Menu.PokazMenu(pozycjeMenu, aktywnaPozycjaMenu);
                aktywnaPozycjaMenu = Menu.WybieranieOpcji(pozycjeMenu, aktywnaPozycjaMenu);
                UruchomOpcje(produkty, nowe_zamowienie, aktywnaPozycjaMenu);
            }

        }

        static void UruchomOpcje(List<Produkt> produkty, Zamowienie nowe_zamowienie, int aktywnaPozycjaMenu)
        {
            switch (aktywnaPozycjaMenu)
            {
                case 0:
                    Console.Clear();
                    TworzenieZamowienia.DodajProduktDoZamowienia(produkty, nowe_zamowienie);
                    Console.ReadKey();
                    break;
                case 1:
                    Console.Clear();
                    TworzenieZamowienia.WyswietlWszystkieProdukty(produkty);
                    Console.ReadKey();
                    break;
                case 2:
                    Console.Clear();
                    TworzenieZamowienia.PokazSzczegolyZamowienia(nowe_zamowienie);
                    Console.ReadKey();
                    break;
                case 3:
                    Console.Clear();
                    TworzenieZamowienia.ZlozZamowienie(nowe_zamowienie);
                    Console.ReadKey();
                    break;
                default:
                    Console.WriteLine("Nieznana opcja menu");
                    Console.ReadKey();
                    break;
            }
        }
    }
}
