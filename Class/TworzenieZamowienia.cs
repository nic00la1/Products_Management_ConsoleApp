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
                    // dodanie produktow do zamowienia
                    Console.Clear();
                    Console.WriteLine("Wybierz akcje zamowienia: ");
                    // Tymczasowo bedzie menu plaskie
                    Console.WriteLine("1. Dodaj produkt do zamowienia");
                    Console.WriteLine("2. Wyswietl wszystkie produkty");
                    Console.WriteLine("3. Pokaz Szczegoly Zamowienia");
                    Console.WriteLine("4. Zloz zamowienie");

                    string akcja = Console.ReadLine();

                    switch (akcja)
                    {
                        case "1":
                            DodajProduktDoZamowienia(produkty, nowe_zamowienie);
                            break;

                        case "2":
                            WyswietlWszystkieProdukty(produkty);
                            break;

                        case "3":
                            PokazSzczegolyZamowienia(nowe_zamowienie);
                            break;
                        case "4":
                            ZlozZamowienie(nowe_zamowienie);
                            break;
                        default:
                            Console.WriteLine("Nie ma takiej opcji");
                            break;
                    }

                    Console.ReadKey();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public static void WyswietlWszystkieZamowienia()
        {
            try
            {
                // Sprawdz czy zamowienia istnieja
                if (!File.Exists(pathZamowienia))
                {
                    Console.WriteLine("Brak zamowien");
                    return;
                }
                Console.WriteLine(">>> Wszystkie zamowienia <<<\n");

                string jsonZ = File.ReadAllText(pathZamowienia);
                zamowienia = JsonSerializer.Deserialize<List<Zamowienie>>(jsonZ);

                foreach (Zamowienie zamowienie in zamowienia)
                {
                    Console.WriteLine($"ID zamowienia: {zamowienie.Id}\n");
                    Console.WriteLine($"Klient: {zamowienie.DaneKupujacego.Imie} {zamowienie.DaneKupujacego.Nazwisko}");
                    Console.WriteLine($"Adres: {zamowienie.DaneKupujacego.Adres}");
                    Console.WriteLine($"Sposob dostawy: {zamowienie.SposobDostawy}");
                    Console.WriteLine($"Sposob platnosci: {zamowienie.SposobPlatnosci}");
                    Console.WriteLine($"Kwota calkowita: {zamowienie.KwotaCalkowita}zł\n");
                }
                Console.ReadKey();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private static void DodajProduktDoZamowienia(List<Produkt> produkty, Zamowienie nowe_zamowienie)
        {
            Console.Clear();
            Console.WriteLine(">>> Dodaj produkt do zamowienia <<<\n");
            Console.WriteLine("Podaj nazwe produktu");
            string tymczasowa_nazwa = Console.ReadLine();

            foreach (Produkt produkt in produkty)
            {
                if (tymczasowa_nazwa == produkt.Nazwa) // sprawdzenie czy produkt istnieje
                {
                    nowe_zamowienie.DodajProdukt(produkt);
                    tymczasowa_nazwa = ""; // reset nazwy
                    Console.WriteLine("Dodano produkt do zamowienia");
                }
                else
                {
                    Console.WriteLine("Nie ma takiego produktu");
                    return;
                }
            }
        }

        private static void WyswietlWszystkieProdukty(List<Produkt> produkty)
        {
            Console.Clear();
            Console.WriteLine(">>> Lista produktow: <<<\n");
            foreach (Produkt produkt in produkty)
            {
                Console.WriteLine($"{produkt.Nazwa} - {produkt.Cena}zł");
            }
        }

        private static void PokazSzczegolyZamowienia(Zamowienie nowe_zamowienie)
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
                Console.WriteLine($"Kwota calkowita: {nowe_zamowienie.KwotaCalkowita}zł\n");
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

                if (decyzja == "t")
                {
                    Console.WriteLine("Obliczanie kwoty calkowitej...");
                    nowe_zamowienie.Oblicz_Kwote_Calkowita();

                    double cena = nowe_zamowienie.KwotaCalkowita;
                    Console.WriteLine($"\nKwota calkowita zamowienia: {cena}zł");

                    zamowienia.Add(nowe_zamowienie);
                    string jsonZ = JsonSerializer.Serialize<List<Zamowienie>>(zamowienia);

                    File.WriteAllText(pathZamowienia, jsonZ);
                    Console.WriteLine("Zamowienie zostalo zlozone!\n");

                    Console.WriteLine("Nacisnij dowolny klawisz, aby wrocic do menu");
                    Console.ReadKey();

                    // Przerywa petle i wraca do menu glownego
                    Menu.StartMenu();
                }
                else
                {
                    Console.WriteLine("Zamowienie nie zostalo zlozone");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}