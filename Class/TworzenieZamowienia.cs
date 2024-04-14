using System.Text.Json;

namespace Products_Management_ConsoleApp.Class
{
    internal class TworzenieZamowienia
    {
        public static List<Zamowienie> zamowienia;
        public static string pathProdukty = "C:\\Users\\Admin\\Source\\Repos\\nic00la1\\Products_Management_ConsoleApp\\ProductsDummyData\\produkty.json";
        public static string pathZamowienia = "C:\\Users\\Admin\\Source\\Repos\\nic00la1\\Products_Management_ConsoleApp\\ProductsDummyData\\zamowienia.json";

        public static void UtworzZamowienie()
        {
            try
            {
                if (!File.Exists(pathProdukty)) // sprawdzenie czy plik istnieje
                {
                    File.Create(pathProdukty).Close();
                }

                if (!File.Exists(pathZamowienia))
                {
                    File.Create(pathZamowienia).Close();
                }

                string jsonZ = File.ReadAllText(pathZamowienia);
                zamowienia = JsonSerializer.Deserialize<List<Zamowienie>>(jsonZ);

                Console.SetCursorPosition(12, 4);

                Console.WriteLine("Podaj dane klienta, zeby utworzyc zamowienie");

                Console.WriteLine("Imie: ");
                string imie = Console.ReadLine();

                Console.WriteLine("Nazwisko: ");
                string nazwisko = Console.ReadLine();

                Console.WriteLine("Adres: ");
                string adres = Console.ReadLine();

                Console.WriteLine("Sposob dostawy: ");
                Console.WriteLine("1. kurier (20zl),\n2. odbior osobisty (0zl)");
                string sposobDostawy = Console.ReadLine();

                Console.WriteLine("Sposob platnosci: ");
                Console.WriteLine("1. karta (2zl),\n2. gotowka (0zl)");
                string sposobPlatnosci = Console.ReadLine();

                // Jesli dane klienta sa puste to nie utworzy zamowienia
                if (string.IsNullOrEmpty(imie) || string.IsNullOrEmpty(nazwisko) || string.IsNullOrEmpty(adres))
                {
                    Jesli_Blad();
                    Console.WriteLine("Nie podano wszystkich danych klienta!");
                    Console.ReadKey();
                    return;
                }

                List<Produkt> produkty;
                string jsonP = File.ReadAllText(pathProdukty);
                produkty = JsonSerializer.Deserialize<List<Produkt>>(jsonP);

                Klient klient = new Klient
                {
                    Imie = imie,
                    Nazwisko = nazwisko,
                    Adres = adres

                };

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
                    MenuTworzenieZamowienia.StartMenu();
                }

            }
            catch (Exception ex)
            {
                Jesli_Blad();
                Console.WriteLine(ex.Message);
                Console.ReadKey();
            }
        }

        public static void WyswietlWszystkieZamowienia()
        {
            try
            {
                // Sprawdz czy zamowienia istnieja
                if (!File.Exists(pathZamowienia) || (File.ReadAllText(pathZamowienia) == "[]"))
                {
                    Jesli_Blad();
                    Console.WriteLine("Brak zamowien do wyswietlenia");
                    Console.ReadKey();
                    return;
                }
                Console.WriteLine(">>> Wszystkie zamowienia <<<\n");

                string jsonZ = File.ReadAllText(pathZamowienia);
                zamowienia = JsonSerializer.Deserialize<List<Zamowienie>>(jsonZ);

                foreach (Zamowienie zamowienie in zamowienia)
                {
                    Console.WriteLine("====================================");
                    Console.WriteLine($" --- ID zamowienia: {zamowienie.Id} ---\n");
                    Console.WriteLine($"Klient: {zamowienie.DaneKupujacego.Imie} {zamowienie.DaneKupujacego.Nazwisko}");
                    Console.WriteLine($"Adres: {zamowienie.DaneKupujacego.Adres}");
                    Console.WriteLine($"Sposob dostawy: {zamowienie.SposobDostawy}");
                    Console.WriteLine($"Sposob platnosci: {zamowienie.SposobPlatnosci}");
                    double kwotaCalkowita = zamowienie.KwotaCalkowita;

                    // Dodajemy koszt dostawy jeśli wybrano kuriera
                    if (zamowienie.SposobDostawy == "1")
                    {
                        kwotaCalkowita += 20;
                        Console.WriteLine("Koszt dostawy kurierem: 20zł");
                    }
                    else if (zamowienie.SposobDostawy == "2")
                    {
                        kwotaCalkowita += 0;
                    }

                    // Dodajemy koszt płatności kartą jeśli wybrano tę opcję
                    if (zamowienie.SposobPlatnosci == "1")
                    {
                        kwotaCalkowita += 2;
                        Console.WriteLine("Koszt płatności kartą: 2zł");
                    }
                    else if (zamowienie.SposobDostawy == "2")
                    {
                        kwotaCalkowita += 0;
                    }

                    Console.WriteLine($"Kwota calkowita: {kwotaCalkowita}zł\n");
                    Console.WriteLine("Produkty w zamowieniu:");
                    foreach (Produkt produkt in zamowienie.Produkty)
                    {
                        Console.WriteLine($"{produkt.Nazwa} - {produkt.Cena}zł");
                    }

                    Console.WriteLine("====================================");
                }
                Console.ReadKey();
            }
            catch (Exception ex)
            {
                Jesli_Blad();
                Console.WriteLine(ex.Message);
                Console.ReadKey();
            }
        }

        public static void DodajProduktDoZamowienia(List<Produkt> produkty, Zamowienie nowe_zamowienie)
        {
            Console.Clear();
            Console.WriteLine(">>> Dodaj produkt do zamowienia <<<\n");
            Console.WriteLine("Podaj nazwe produktu");
            string tymczasowa_nazwa = Console.ReadLine();

            bool produktZnaleziony = false; // flaga wskazująca, czy produkt został znaleziony

            foreach (Produkt produkt in produkty)
            {
                if (tymczasowa_nazwa == produkt.Nazwa) // sprawdzenie czy produkt istnieje
                {
                    nowe_zamowienie.DodajProdukt(produkt);
                    tymczasowa_nazwa = ""; // reset nazwy
                    produktZnaleziony = true; // ustawienie flagi na true
                    Console.WriteLine("Dodano produkt do zamowienia");
                    break; // przerwanie pętli po dodaniu produktu
                }
            }

            if (!produktZnaleziony) // jeśli produkt nie został znaleziony
            {
                Console.WriteLine("Nie ma takiego produktu!");
            }

        }

        public static void WyswietlWszystkieProdukty(List<Produkt> produkty)
        {
            Console.Clear();
            Console.WriteLine(">>> Lista produktow: <<<\n");
            foreach (Produkt produkt in produkty)
            {
                Console.WriteLine($"{produkt.Nazwa} - {produkt.Cena}zł");
            }
        }

        public static void PokazSzczegolyZamowienia(Zamowienie nowe_zamowienie)
        {
            // Jesli nie ma produktow w zamowieniu
            if (nowe_zamowienie.Produkty.Count == 0)
            {
                Console.WriteLine("Brak produktow w zamowieniu!");
                return;
            }
            else
            {
                Console.Clear();
                Console.WriteLine(">>> Szczegoly zamowienia <<<");
                Console.WriteLine($"Zamowienie z dnia {DateTime.Now} ---> ID: {nowe_zamowienie.Id}\n");
                Console.WriteLine($"Klient: {nowe_zamowienie.DaneKupujacego.Imie} {nowe_zamowienie.DaneKupujacego.Nazwisko}");
                Console.WriteLine($"Adres: {nowe_zamowienie.DaneKupujacego.Adres}");
                Console.WriteLine($"Sposob dostawy: {nowe_zamowienie.SposobDostawy}");
                Console.WriteLine($"Sposob platnosci: {nowe_zamowienie.SposobPlatnosci}");
                // Aktualizacja kwoty całkowitej po dodaniu produktu
                nowe_zamowienie.Oblicz_Kwote_Calkowita();
                Console.WriteLine($"Produkty w koszyku:");
                foreach (Produkt produkt in nowe_zamowienie.Produkty)
                {
                    Console.WriteLine($"{produkt.Nazwa} - {produkt.Cena}zł");
                }
            }
        }

        public static void ZlozZamowienie(Zamowienie nowe_zamowienie)
        {
            try
            {
                Console.WriteLine(">>> Zloz zamowienie <<<\n");
                Console.WriteLine("Czy na pewno chcesz zlozyc zamowienie? (t/n)");
                string decyzja = Console.ReadLine();

                // Jesli nie wybrano produktow to nie zlozy zamowienia
                if (nowe_zamowienie.Produkty.Count == 0)
                {
                    Console.WriteLine("Nie wybrano zadnych produktow!");
                    return;
                }

                string[] pozycjeMenu = { "Option 1", "Option 2", "Option 3" };
                int aktywnaPozycjaMenu = 0;

                if (decyzja == "t")
                {
                    // Przerywa petle i wraca do menu glownego
                    Menu.StartMenu(pozycjeMenu, ref aktywnaPozycjaMenu);
                }
                else
                {
                    Console.WriteLine("Zamowienie nie zostalo zlozone");
                }
            }
            catch (Exception ex)
            {
                Jesli_Blad();
                Console.WriteLine(ex.Message);
                Console.ReadKey();
            }
        }

        private static void Jesli_Blad()
        {
            // Wyswietlenie komunikatu w kolorze czerwonym
            Console.ForegroundColor = ConsoleColor.Red;
        }
    }
}