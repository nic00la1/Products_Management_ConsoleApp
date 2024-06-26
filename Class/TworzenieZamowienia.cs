﻿using ConsoleTables;
using System.Data;
using System.Text;
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

                List<string> sposobyDostawy = new List<string> { "1. kurier (20zl)", "2. odbior osobisty (0zl)" };
                string sposobDostawy = WybierzOpcje("Sposob dostawy: ", sposobyDostawy);

                List<string> sposobyPlatnosci = new List<string> { "1. karta (2zl)", "2. gotowka (0zl)" };
                string sposobPlatnosci = WybierzOpcje("Sposob platnosci: ", sposobyPlatnosci);

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
                    MenuTworzenieZamowienia.StartMenu(produkty, nowe_zamowienie); // rozpocznij menu tworzenia zamowienia z listą produktów i obiektem zamówienia
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
                    SposobyPlatnosciIDostawy(zamowienie);
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

                    zamowienie.Oblicz_Kwote_Calkowita();
                    Console.WriteLine("Produkty w zamowieniu:");
                    foreach (ProduktZamowienia produktZamowienia in zamowienie.Produkty)
                    {
                        Console.WriteLine($"{produktZamowienia.Produkt.Nazwa} - {produktZamowienia.Produkt.Cena}zł x {produktZamowienia.Ilosc}");
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
                    Console.WriteLine("Podaj ilosc produktu");
                    string iloscString = Console.ReadLine();
                    int ilosc;

                    if (string.IsNullOrEmpty(iloscString) || !int.TryParse(iloscString, out ilosc) || ilosc <= 0)
                    {
                        Console.WriteLine("Podaj prawidlowa ilosc produktu!");
                        return;
                    }

                    nowe_zamowienie.DodajProdukt(produkt, ilosc);
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
            Console.OutputEncoding = Encoding.UTF8;
            var tabela = TabelaWszystkieProdukty(produkty);
            string[] nazwaKolumn = tabela.Columns.Cast<DataColumn>()
                                    .Select(x => x.ColumnName)
                                    .ToArray();

            DataRow[] rows = tabela.Select();
            Console.WriteLine(">>> Wszystkie produkty <<<\n");

            var tableWyswietl = new ConsoleTable(nazwaKolumn);
            foreach (DataRow row in rows)
            {
                tableWyswietl.AddRow(row.ItemArray);
            }

            tableWyswietl.Write(Format.Minimal);
        }

        public static DataTable TabelaWszystkieProdukty(List<Produkt> produkty)
        {
            var table = new DataTable();
            table.Columns.Add("ID", typeof(int));
            table.Columns.Add("Nazwa", typeof(string));
            table.Columns.Add("Producent", typeof(string));
            table.Columns.Add("Cena", typeof(double));
            table.Columns.Add("Kategoria", typeof(string));
            table.Columns.Add("Ilosc", typeof(int));
            table.Columns.Add("Data dostawy", typeof(DateTime));

            foreach (Produkt produkt in produkty)
            {
                table.Rows.Add(produkt.Id, produkt.Nazwa, produkt.Producent, produkt.Cena, produkt.Kategoria, produkt.Ilosc, produkt.DataDostawy);
            }

            return table;
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
                SposobyPlatnosciIDostawy(nowe_zamowienie);


                // Aktualizacja kwoty całkowitej po dodaniu produktu
                nowe_zamowienie.Oblicz_Kwote_Calkowita();
                Console.WriteLine($"\nProdukty w koszyku:");
                foreach (ProduktZamowienia produktZamowienia in nowe_zamowienie.Produkty)
                {
                    Console.WriteLine($"{produktZamowienia.Produkt.Nazwa} - {produktZamowienia.Produkt.Cena}zł x {produktZamowienia.Ilosc}");
                }
            }
        }

        public static void ZlozZamowienie(Zamowienie nowe_zamowienie)
        {
            try
            {
                // Jesli nie wybrano produktow to nie zlozy zamowienia
                if (nowe_zamowienie.Produkty.Count == 0)
                {
                    Console.WriteLine("Nie wybrano zadnych produktow!");
                    return;
                }

                Console.WriteLine(">>> Zloz zamowienie <<<\n");
                Console.WriteLine("Czy na pewno chcesz zlozyc zamowienie? (t/n)");
                string decyzja = Console.ReadLine();

                if (decyzja == "t")
                {
                    // Dodaj nowe zamówienie do listy wszystkich zamówień
                    zamowienia.Add(nowe_zamowienie);

                    // Zapisz listę wszystkich zamówień do pliku JSON
                    string nowyJson = JsonSerializer.Serialize(zamowienia);
                    File.WriteAllText(pathZamowienia, nowyJson);

                    // Przerywa petle wraca do menu glownego i zapisuje zamowienie
                    Console.WriteLine("Zamowienie zostalo zlozone! :) Dziękujemy za skorzystanie z naszych usług");
                    Console.ReadKey();


                    Menu.StartMenu();
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

        public static string WybierzOpcje(string prompt, List<string> options)
        {
            int selected = 0;

            while (true)
            {
                Console.Clear();
                Console.WriteLine(prompt);

                for (int i = 0; i < options.Count; i++)
                {
                    if (i == selected)
                    {
                        Console.Write(">> ");
                    }
                    else
                    {
                        Console.Write("   ");
                    }

                    Console.WriteLine(options[i]);
                }

                ConsoleKeyInfo keyInfo = Console.ReadKey();

                if (keyInfo.Key == ConsoleKey.UpArrow)
                {
                    selected = (selected - 1 + options.Count) % options.Count;
                }
                else if (keyInfo.Key == ConsoleKey.DownArrow)
                {
                    selected = (selected + 1) % options.Count;
                }
                else if (keyInfo.Key == ConsoleKey.Enter)
                {
                    return options[selected].Split(' ')[1]; // Extracting the value from the selected option
                }
            }
        }

        private static void SposobyPlatnosciIDostawy(Zamowienie nowe_zamowienie)
        {
            // Wyswietlanie ceny dostawy
            if (nowe_zamowienie.SposobDostawy == "kurier")
            {
                Console.WriteLine("Sposob dostawy - kurier (20zł)");
            }
            else
            {
                Console.WriteLine("Sposob dostawy - odbiór osobisty (0zł)");
            }

            // Wyswietlanie ceny platnosci
            if (nowe_zamowienie.SposobPlatnosci == "karta")
            {
                Console.WriteLine("Sposob platnosci - karta (2zł)");
            }
            else
            {
                Console.WriteLine("Sposob płatnosci - gotowka (0zł)");
            }
        }

        private static void Jesli_Blad()
        {
            // Wyswietlenie komunikatu w kolorze czerwonym
            Console.ForegroundColor = ConsoleColor.Red;
        }
    }
}