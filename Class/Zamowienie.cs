namespace Products_Management_ConsoleApp.Class
{
    internal class Zamowienie
    {
        public int Id { get; set; }
        public List<ProduktZamowienia> Produkty { get; set; }
        public Klient DaneKupujacego { get; set; }
        public string SposobDostawy { get; set; }
        public string SposobPlatnosci { get; set; }
        public double KwotaCalkowita { get; set; }

        public Zamowienie(int id, List<ProduktZamowienia> produkty, Klient daneKupujacego, string sposobDostawy, string sposobPlatnosci, double kwotaCalkowita)
        {
            Id = id;
            Produkty = produkty ?? new List<ProduktZamowienia>();
            DaneKupujacego = daneKupujacego;
            SposobDostawy = sposobDostawy;
            SposobPlatnosci = sposobPlatnosci;
            KwotaCalkowita = kwotaCalkowita;
        }

        public void DodajProdukt(Produkt produkt, int ilosc)
        {
            var produktZamowienia = Produkty.FirstOrDefault(p => p.Produkt == produkt);
            if (produktZamowienia != null)
            {
                produktZamowienia.Ilosc += ilosc;
            }
            else
            {
                Produkty.Add(new ProduktZamowienia { Produkt = produkt, Ilosc = ilosc });
            }
        }

        public void Oblicz_Kwote_Calkowita()
        {
            double cena = 0;
            if (SposobDostawy == "kurier")
                cena += 20;

            if (SposobPlatnosci == "karta")
                cena += 2;


            foreach (ProduktZamowienia pz in Produkty)
            {
                cena += pz.Produkt.Cena * pz.Ilosc;
            }
            KwotaCalkowita = Math.Round(cena, 2);
            // Zmiana koloru na zielony i podkreslenie czcionki
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.WriteLine($"Kwota całkowita zamówienia: {KwotaCalkowita}zł");
            Console.ForegroundColor = ConsoleColor.DarkCyan;
        }
    }

    internal class Klient
    {
        public string? Imie { get; set; }
        public string? Nazwisko { get; set; }
        public string? Adres { get; set; }
    }

    internal class ProduktZamowienia
    {
        public Produkt Produkt { get; set; }
        public int Ilosc { get; set; }
    }
}
